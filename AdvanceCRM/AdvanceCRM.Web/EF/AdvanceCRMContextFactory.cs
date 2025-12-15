using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AdvanceCRM.Web.EF.Models
{
    public class AdvanceCRMContextFactory : IDesignTimeDbContextFactory<AdvanceCRMContext>
    {
        public AdvanceCRMContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdvanceCRMContext>();
            optionsBuilder.UseSqlServer("Server=103.61.224.79,1433;Database=AdvanceCRM_Default_v1;User ID=sa;Password=Advancecrm@123;TrustServerCertificate=true;");
            return new AdvanceCRMContext(optionsBuilder.Options);
        }
    }
}
