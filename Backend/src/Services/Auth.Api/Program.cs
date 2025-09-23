using Auth.Api.Data;
using Auth.Api.Data.Entties;
using Auth.Api.Data.RawSql;
using Auth.Api.Helper;
using Auth.Api.Modal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    //options.AddPolicy("MyPolicy", builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
    //    .AllowAnyMethod()
    //    .AllowAnyHeader()
    //    .AllowCredentials());

    options.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder
            .WithOrigins(
                "http://localhost:4200",    // Host machine Angular
                "https://localhost:4200",
                "http://frontend:80"        // Docker internal
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Required for cookies/auth headers
    });
});

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🛰️ Database with retry
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
    ));

//builder.Services.AddTransient<IUserService, UserService>();
#region Database Settings
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("DbConnection"),
    sqlOptions=>sqlOptions.EnableRetryOnFailure
        (
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )
    )
);
#endregion

builder.Services.AddScoped<IRawSql_Helper,RawSql_Helper>();


var _authkey = builder.Configuration.GetValue<string>("JwtSettings:SecurityKey");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

var _jwtsetting = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(_jwtsetting);

builder.Services.AddControllersWithViews(options => options.Filters.Add(typeof(DynamicAuthorizationFilter)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var maxRetries = 20;
    var delay = TimeSpan.FromSeconds(10);

    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Attempting to connect to SQL Server...");
            context.Database.OpenConnection(); // Test connection
            context.Database.CloseConnection();
            break; // Connection succeeded
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, $"Connection failed on attempt {i + 1}/{maxRetries}. Retrying in 10s...");
            await Task.Delay(delay);
        }
    }

    // Now migrate
    try
    {
        context.Database.Migrate();
        logger.LogInformation("✅ Database migrated.");

        // Seed data
        if (!context.Users.Any(u => u.Username == "admin"))
        {
           
            context.Roles.Add(new Role { RoleName = "ADMIN" });
            context.Users.Add(new User
            {
                Username = "admin",
                Password = "yfTzNw11SmvlXcJ4M4zog4RuKCf7rtL3QM8Tz2zAMGVMyFjC",// password 123
                Role = "ADMIN"
            });
            context.SaveChanges();
            logger.LogInformation("🔐 Admin user seeded.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to migrate or seed database.");
        throw;
    }
}


app.Run();
