using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportCenterApi;
using SportCenterApi.Services.Interfaces;
using SportCenterApi.Services;
using Microsoft.AspNetCore.Routing;
using SportCenterApi.Filters;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SportCenterApi.Mapping;
using AutoMapper;
using SportCenterApi.Entities;
using SportCenterApi.Seeders;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Formatting.Indented;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<HideEndpointsDocumentFilter>();
});



builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<DbSportCenterContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<DbSportCenterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IBmiService, BmiService>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedAppUser.SeedAsync(services, 100);
    await SeedRoles.SeedAsync(services);
}


app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SportCenter Api");
});

app.MapControllers();

app.Run();
