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
    options.AddPolicy("MyPolicy", builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
// 🔁 Wait for DB with retry
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    var maxRetries = 15;
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); // Applies migrations

            // Seed data
            // Seed data
            if (!context.Users.Any())
            {
                context.Roles.Add(new Role { RoleName = "ADMIN" });
                context.Users.Add(new User { Username = "admin", Password = "123", Role = "ADMIN" });
                //context.AppContents.Add(new AppContent { });
                context.SaveChanges();
            }
            logger.LogInformation("Connected to SQL Server and migrated.");
            break;
        }
        catch (SqlException ex) when (i < maxRetries - 1)
        {
            logger.LogWarning($"Connection to SQL Server failed (attempt {i + 1}/{maxRetries}): {ex.Message}");
            await Task.Delay(10000); // Wait 10 seconds
        }
    }
}

//app.UseHttpsRedirection(); stop it only for checking docker temporarily
//app.UseSwagger(); //it should be in development mode only. but now for checking docker temporarily
//app.UseSwaggerUI();//it should be in development mode only. but now for checking docker temporarily
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
