# Kubernetes Deployment Notes

## Tenant Proxy Configuration

The `nginx/wildcard.conf` file configures a single Nginx server block for `*.example.com`.
It relies on `/etc/nginx/conf.d/tenants.map` to map each tenant's host header to its
internal application port.

### Provisioning

1. Copy `k8s/nginx/wildcard.conf` to your server (e.g. `/etc/nginx/conf.d/`).
2. For each tenant, run:

   ```bash
   scripts/provision-tenant.sh <subdomain> <port>
   ```

   The script stores the mapping in `/etc/nginx/conf.d/tenants.map` and reloads Nginx
   so that `subdomain.example.com` proxies to the given port.
