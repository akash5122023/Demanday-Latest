# Cloudflare latency playbook

When the application is fast on the Windows/IIS host but slow once it is published behind Cloudflare on Linux, the delay is almost always introduced **before** the request reaches Kestrel. Use the guidance below to quantify the backend time and then tune the Cloudflare edge so users in high-latency regions do not wait several seconds for each grid load.

## 1. Measure the real application time

1. Deploy the build that includes the `ServerTimingMiddleware`. Every HTTP response will now contain:
   * `Server-Timing: app;dur=<ms>` – backend execution time in milliseconds.
   * `X-App-Processing-Time: <ms>ms` – duplicate value that is easy to read in browser dev tools.
2. Open the browser developer tools (Network tab) while reproducing the slow grid load. Compare the numbers:
   * If `app;dur` is well under 500 ms but the total waterfall time is multiple seconds, the slowdown is happening on the network (DNS, TLS handshake, or Cloudflare edge queues).
   * If `app;dur` itself is high, profile the repository/SQL queries—the issue is inside the application.
3. Capture the `curl` timing output (`dns`, `connect`, `tls`, `ttfb`) alongside the `Server-Timing` header so you can show the client which leg is expensive.

## 2. Reduce Cloudflare overhead

Apply the following changes for the tenant zone (e.g. `bizpluserp.com`):

| Area | Action | Expected impact |
| --- | --- | --- |
| **Routing** | Enable [Argo Smart Routing](https://developers.cloudflare.com/argo-smart-routing/) for the zone. This shortens the `connect` and `tls` stages by keeping the TCP path inside Cloudflare's backbone. | 40–60% faster connection setup for distant users. |
| **DNS** | Force all client offices to use Cloudflare's recursive DNS (`1.1.1.1` / `1.0.0.1`) or another low-latency resolver. Misconfigured ISPs often add 150–200 ms before the request even starts. | Cuts the `dns` column in the `curl` output from ~0.17 s to <0.05 s. |
| **TLS handshake** | Turn on **HTTP/3 (QUIC)** and **0-RTT** in Cloudflare's dashboard (`Network → HTTP/3`, `SSL/TLS → Edge Certificates → 0-RTT`). QUIC keeps the connection alive between navigations so grids open immediately. | Removes the 400 ms handshake shown in the slow trace. |
| **Caching** | Add a page rule (or Cache Rule) for `/Scripts/*`, `/Content/*`, and `/Modules/*` that sets `Cache Level: Cache Everything`, `Edge Cache TTL: a month`, and `Origin Cache Control: On`. The new bundling/compression already marks these assets as immutable, so Cloudflare can serve them without revalidation. | Keeps static payload at the edge so only JSON API calls hit the origin. |
| **Origin shield** | If the origin is located far from most users, enable [Regional Services or tiered caching](https://developers.cloudflare.com/cache/about/tiered-cache/) so requests stay on the Mumbai or Singapore PoP instead of bouncing to Europe. | Consistent `ttfb` under 700 ms for South Asian ISPs. |

## 3. Watch for regressions

* Use `curl -I https://example.com` periodically—`cf-cache-status: HIT` and a small `ttfb` confirm the edge is serving assets.
* Monitor the application logs for the slow-request warning emitted by `ServerTimingMiddleware`. If you see entries over 1000 ms, investigate those repository methods; otherwise the backend is healthy and any slowness is upstream.
* Consider scheduling a synthetic check (Pingdom, Better Uptime, or a GitHub Actions `curl` job) from Pakistan/India so you are alerted when Cloudflare latency spikes again.

Following the above steps ensures the browser receives cached bundles instantly and keeps the SaaS grids responsive regardless of the viewer's ISP.
