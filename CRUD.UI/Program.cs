using CRUD.Core.Domain.IdentityEntities;
using CRUD.Infrastructure;
using CRUD.Infrastructure.Repositories;
using CRUD.Infrastructure.RepositoryContracts;
using CRUD.Infrastructure.ServiceContracts;
using CRUD.Infrastructure.Services;
using CRUD.UI.Middleware.MiddlewareExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
});




//add services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICountryService, CountryService>();

//add repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>   //generates OpenAPI specification
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CRUD API",
        Description = "An ASP.NET Core Web API for managing CRUD operations",
        TermsOfService = new Uri("https://example.com/terms"),
    });
    options.EnableAnnotations();
});


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
    app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    app.UseSwagger();       //Creates endpoint for swagger.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandlingMiddleWare();
}

app.UseRouting();
app.UseCors();
//app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{Id?}");
app.MapControllers();
//Data Source=DESKTOP-6BHPI4U\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True

app.Run();