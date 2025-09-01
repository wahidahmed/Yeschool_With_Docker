using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using School.AdminService.Data;
using School.AdminService.Extensions;
using School.AdminService.Helpers;
using School.AdminService.Repository;
using School.AdminService.Repository.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database Settings
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRawSqlRepository, RawSqlRepository>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfiles>();
});


string logPath = builder.Configuration.GetSection("Logging:LogPath").Value;
var _logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.File(logPath).CreateLogger();
builder.Logging.AddSerilog(_logger);

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

app.Run();
