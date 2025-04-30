using System.Data.Common;
using LadleMeThis.Context;
using LadleMethisIntegrationTests.Seeder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LadleMeThisIntegrationTests;

public class LadleMeThisFactory : WebApplicationFactory<Program>
{
	private readonly string _dbName = Guid.NewGuid().ToString();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			var descriptor = services.SingleOrDefault(
				d => d.ServiceType == typeof(IDbContextOptionsConfiguration<LadleMeThisContext>));

			if (descriptor != null)
				services.Remove(descriptor);
			
			
			var dbConnectionDescriptor = services.SingleOrDefault(
				d => d.ServiceType ==
				     typeof(DbConnection));
			
			if (dbConnectionDescriptor != null)
				services.Remove(dbConnectionDescriptor);
			

			services.AddDbContext<LadleMeThisContext>(options =>
			{
				options.UseInMemoryDatabase(_dbName);
			});

			var sp = services.BuildServiceProvider();
			using var scope = sp.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();
			
			var identityManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
			var seeder = new TestDataSeeder(userManager, identityManager, db);

			var seedTask = seeder.SeedAsync();
			seedTask.Wait();
		});
	}

}