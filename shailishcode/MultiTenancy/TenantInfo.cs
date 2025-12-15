namespace AdvanceCRM.MultiTenancy
{
    /// <summary>
    /// Represents a tenant resolved from the current request context.
    /// </summary>
    public class TenantInfo
    {
        public int TenantId { get; set; }

        public string? Subdomain { get; set; }

        public string? DbName { get; set; }

        /// <summary>
        /// Cached connection string pointing to the tenant database.
        /// </summary>
        public string? ConnectionString { get; set; }
    }
}
