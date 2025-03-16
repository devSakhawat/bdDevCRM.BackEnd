using Microsoft.EntityFrameworkCore.Design;

namespace bdDevCRM.Api.ContextFactory;

//// first empliment IDesignTimeDbContextFactory to get CreateDbContext with return type RepositoryContext
//public class RepositoryContextFactory : IDesignTimeDbContextFactory<CRMDBContext>
//{
//  public CRMContext CreateDbContext(string[] args)
//  {
//    // Build the configuration to read the connection string from the appsettings.json file
//    var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();


//    // Configure DbContextOptions with the connection string
//    var builder = new DbContextOptionsBuilder<CRMDBContext>().UseSqlServer(configuration.GetConnectionString("DbLocation"));

//    // reutrn repositorycontext with configured option
//    return new CRMDBContext(builder.Options);
//  }
//}