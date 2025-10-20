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



app.Run();
