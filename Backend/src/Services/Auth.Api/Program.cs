using Auth.Api.Data;
using Auth.Api.Helper;
using Auth.Api.Modal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<IUserService, UserService>();
#region Database Settings
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
#endregion

builder.Services.AddCors(op =>
{
    op.AddPolicy("myPolicy", b =>
    {
        b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var _authkey = builder.Configuration.GetValue<string>("JwtSettings:SecurityKey");

//builder.Services.AddAuthentication(item =>
//{
//    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(item =>
//{
//    item.RequireHttpsMetadata = true;
//    item.SaveToken = true;
//    item.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };

//});

//var _jwtsetting = builder.Configuration.GetSection("JwtSettings");
//builder.Services.Configure<JwtSettings>(_jwtsetting);

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
app.UseCors("myPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
