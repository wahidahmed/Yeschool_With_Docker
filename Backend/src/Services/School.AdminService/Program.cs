using Microsoft.EntityFrameworkCore;
using School.AdminService.Data;
using School.AdminService.Repository;
using School.AdminService.Repository.Interfaces;

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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
