using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.DBContext
{
    public partial class ProductCatalogueContext : DbContext
    {
        public ProductCatalogueContext(string connectionString) : base(GetOptions(connectionString))
        {
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}

