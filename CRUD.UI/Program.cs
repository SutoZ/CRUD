using CRUD.Core.ServiceContracts;
using CRUD.Core.Services;
using CRUD.Infrastructure;
using CRUD.Infrastructure.Repositories;
using CRUD.Infrastructure.RepositoryContracts;
using CRUD.Infrastructure.ServiceContracts;
using CRUD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CRUD API",
        Description = "An ASP.NET Core Web API for managing CRUD operations",
        TermsOfService = new Uri("https://example.com/terms"),
    });
});

//add services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICountryService, CountryService>();

//add repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
//Add database context to IoC container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
            .WithHeaders("Authorization", "origin", "accept", "content-type")
            .WithMethods("GET", "POST", "PUT", "DELETE");
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseRouting();
app.UseCors();
//app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{Id?}");
app.MapControllers();
//Data Source=DESKTOP-6BHPI4U\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True


app.Run();

