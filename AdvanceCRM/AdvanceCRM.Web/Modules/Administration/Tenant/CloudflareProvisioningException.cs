using System;
using System.Net;

namespace AdvanceCRM.Administration
{
    public class CloudflareProvisioningException : Exception
    {
        public CloudflareProvisioningException(
            string subdomain,
            HttpStatusCode? statusCode,
            string responseContent,
            string message,

            string diagnosticHint = null,

            Exception innerException = null)
            : base(message ?? "Cloudflare provisioning failed.", innerException)
        {
            Subdomain = subdomain;
            StatusCode = statusCode;
            ResponseContent = responseContent;

            DiagnosticHint = diagnosticHint;

        }

        public string Subdomain { get; }

        public HttpStatusCode? StatusCode { get; }

        public string ResponseContent { get; }


        public string DiagnosticHint { get; }

    }
}
