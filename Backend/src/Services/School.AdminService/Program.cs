using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.AdminService.Data;
using School.AdminService.Extensions;
using School.AdminService.Helpers;
using School.AdminService.Repository;
using School.AdminService.Repository.Interfaces;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database Settings
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
    ));
#endregion

// 💉 DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRawSqlRepository, RawSqlRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IFeesService, FeesService>();
builder.Services.AddScoped<IIdGeneratorService, IdGeneratorService>();

// 🔄 AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

// 🔐 JWT Authentication
var _authkey = builder.Configuration.GetValue<string>("JwtSettings:SecurityKey")
              ?? throw new InvalidOperationException("JWT key not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey))
        };
    });


// 🔑 Authorization with Dynamic Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DynamicPermission", policy =>
        policy.Requirements.Add(new PermissionRequirement()));

    // Set as default policy for [Authorize]
    options.DefaultPolicy = options.GetPolicy("DynamicPermission");
});

// ✅ Register Authorization Handler
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();


// 📝 Logging
string logPath = builder.Configuration.GetSection("Logging:LogPath").Value
                 ?? "logs/admin-service-log-.txt";

var _logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog(_logger);

// 🚦 Build App
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler(app.Environment);
app.UseAuthentication(); // ← YOU ARE MISSING THIS!
app.UseAuthorization();
// 🔀 Map Endpoints
app.MapControllers(); // ← This maps all controller routes

app.Run();
