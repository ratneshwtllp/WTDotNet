using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ERP.DataAccess
{
    public class DbContextFactory : IDbContextFactory<DBContext>
    {
        public DBContext Create(DbContextFactoryOptions options)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");
            var connectionStringConfig = configBuilder.Build();

            var builder = new DbContextOptionsBuilder<DBContext>();
            builder.UseSqlServer(connectionStringConfig.GetConnectionString("ERPConnection"));
            return new DBContext(builder.Options);
        }
    }
}
