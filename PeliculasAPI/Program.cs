using PeliculasAPI;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PeliculasAPI.Servicios;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PeliculasAPI.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PeliculasApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


builder.Services.AddScoped<PeliculaExisteAttribute>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IActoresServices, ActoresServices>();
builder.Services.AddScoped<ICuentasServices, CuentasServices>();
builder.Services.AddScoped<ICustomBaseControllerServices, CustomBaseControllerServices>();
builder.Services.AddScoped<IGenerosServices, GenerosServices>();
builder.Services.AddScoped<IPeliculasServices, PeliculasServices>();
builder.Services.AddScoped<IReviewServices, ReviewServices>();
builder.Services.AddScoped<ISalasDeCineServicios, SalasDeCineServicios>();




builder.Services.AddSingleton(provider =>

    new MapperConfiguration(config =>
    {
        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
        config.AddProfile(new AutoMapperProfiles(geometryFactory));
    }).CreateMapper()
);

string jwtKey = Environment.GetEnvironmentVariable("PELICULAS_API_JWT_KEY");
if (jwtKey == null) throw new Exception("PELICULAS_API_JWT_KEY environment variable not set");
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero
            });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
          sqlServerOptions => sqlServerOptions.UseNetTopologySuite()
          ));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(
       System.Text.Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
           ClockSkew = TimeSpan.Zero
       }
   );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var isTest = AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.ToLowerInvariant().Contains("mvc.testing"));
    if (isTest) dbContext.SeedTestData().Wait();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();

public partial class Program {}