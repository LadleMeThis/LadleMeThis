using LadleMeThis.Context;
using LadleMeThis.Repositories.SavedRecipeRepository;
using LadleMeThis.Repositories.UserRepository;
using LadleMeThis.Services.SavedRecipeService;
using LadleMeThis.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LadleMeThisContext>(options =>
{
    options.UseNpgsql(
        "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=LadleMeThis;");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISavedRecipeRepository, SavedRecipeRepository>();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISavedRecipeService, SavedRecipeService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();