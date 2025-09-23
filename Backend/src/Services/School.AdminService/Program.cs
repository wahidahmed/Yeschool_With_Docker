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
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure
        (
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )
    ));

#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRawSqlRepository, RawSqlRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IFeesService, FeesService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfiles>();
});

var _authkey = builder.Configuration.GetValue<string>("JwtSettings:SecurityKey");

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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_authkey))
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DynamicPermission", policy =>
        policy.Requirements.Add(new PermissionRequirement())
    );

    // 🔑 Set DynamicPermission as the default policy for [Authorize]
    options.DefaultPolicy = options.GetPolicy("DynamicPermission");
});

// ✅ Register handler
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();



string logPath = builder.Configuration.GetSection("Logging:LogPath").Value;
var _logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.File(logPath).CreateLogger();
builder.Logging.AddSerilog(_logger);

// 🛰️ Database with retry
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
    ));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler(app.Environment);
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var retries = 0;
    const int maxRetries = 15;
    while (retries < maxRetries)
    {
        try
        {
            context.Database.Migrate();
            logger.LogInformation("✅ Database migrated successfully.");
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "❌ Migration failed (attempt {Attempt}/{Max}), retrying in 10s...", retries + 1, maxRetries);
            await Task.Delay(10_000);
            retries++;
        }
    }

    if (retries == maxRetries)
        throw new Exception("Failed to connect to database after multiple attempts.");
}

app.Run();
