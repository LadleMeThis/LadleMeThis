using System.Text;
using LadleMeThis.Context;
using LadleMeThis.Data.Seeder;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Repositories.RecipeRatingRepository;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Repositories.TagRepository;
using LadleMeThis.Services.CategoryService;
using LadleMeThis.Services.FoodImageService;
using LadleMeThis.Services.IngredientService;
using LadleMeThis.Services.RecipeDetailService;
using LadleMeThis.Services.RecipeRatingService;
using LadleMeThis.Services.RecipeService;
using LadleMeThis.Services.TagService;
using LadleMeThis.Services.TokenService;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddDb(builder);
AddIdentity(builder);
AddAuthentication(builder);
AddServices(builder);
AddCookiePolicy(builder);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	
	var db = services.GetRequiredService<LadleMeThisContext>();
	
	if (db.Database.IsRelational())
	{
		db.Database.Migrate();

		await SeedDatabaseAsync(services, builder, db);
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCookiePolicy();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices(WebApplicationBuilder webApplicationBuilder)
{
	webApplicationBuilder.Services.AddScoped<ITagRepository, TagRepository>();
	webApplicationBuilder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
	webApplicationBuilder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
	webApplicationBuilder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
	webApplicationBuilder.Services.AddScoped<IRecipeRatingRepository, RecipeRatingRepository>();
	webApplicationBuilder.Services.AddScoped<IIngredientService, IngredientService>();
	webApplicationBuilder.Services.AddScoped<ITagService, TagService>();
	webApplicationBuilder.Services.AddScoped<ICategoryService, CategoryService>();
	webApplicationBuilder.Services.AddScoped<IRecipeDetailService, RecipeDetailService>();
	webApplicationBuilder.Services.AddScoped<IRecipeService, RecipeService>();
	webApplicationBuilder.Services.AddScoped<IRecipeRatingService, RecipeRatingService>();
	webApplicationBuilder.Services.AddScoped<IUserService, UserService>();
	webApplicationBuilder.Services.AddScoped<ITokenService, TokenService>();
}

void AddDb(WebApplicationBuilder builder1)
{
	builder1.Services.AddDbContext<LadleMeThisContext>(options =>
	{
		options.UseSqlServer(
			builder1.Configuration["DbConnectionString"]);
	});
}

void AddIdentity(WebApplicationBuilder webApplicationBuilder1)
{
	webApplicationBuilder1.Services
		.AddIdentityCore<IdentityUser>(options =>
		{
			options.SignIn.RequireConfirmedAccount = false;
			options.User.RequireUniqueEmail = true;
			options.Password.RequireDigit = false;
			options.Password.RequiredLength = 6;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireLowercase = false;
		})
		.AddRoles<IdentityRole>()
		.AddEntityFrameworkStores<LadleMeThisContext>();
}

void AddAuthentication(WebApplicationBuilder builder2)
{
	builder2.Services
		.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.Events = new JwtBearerEvents
			{
				OnMessageReceived = context =>
				{
					var token = context.Request.Cookies["AuthToken"];
					if (!string.IsNullOrEmpty(token))
						context.Token = token;

					return Task.CompletedTask;
				}
			};

			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ClockSkew = TimeSpan.Zero,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = builder2.Configuration["ValidIssuer"],
				ValidAudience = builder2.Configuration["ValidAudience"],
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(builder2.Configuration["IssuerSigningKey"])
				),
			};
		});
}

void AddCookiePolicy(WebApplicationBuilder builder3)
{
	builder3.Services.Configure<CookiePolicyOptions>(options =>
	{
		options.HttpOnly = HttpOnlyPolicy.Always;
		options.Secure = CookieSecurePolicy.Always;
		options.MinimumSameSitePolicy = SameSiteMode.None;
	});
}

async Task SeedDatabaseAsync(IServiceProvider serviceProvider, WebApplicationBuilder webApplicationBuilder2,
	LadleMeThisContext ladleMeThisContext)
{
	var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
	var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var imageService = new FoodImageService(webApplicationBuilder2.Configuration);

	var seeder = new DataSeeder(userManager, roleManager, ladleMeThisContext, imageService);
	await seeder.SeedAsync();
}

public partial class Program { }