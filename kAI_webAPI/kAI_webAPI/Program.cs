using Microsoft.EntityFrameworkCore; // Ensure this is included
using Microsoft.Extensions.DependencyInjection; // Ensure this is included
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using kAI_webAPI.Models.User;
using kAI_webAPI.Models.Question;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration cf = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
// qua la oke 
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Sử dụng MySQL provider
builder.Services.AddDbContext<Questioncontext>(options =>
    options.UseMySql(
        connString,
        new MySqlServerVersion(new Version(8, 0, 40)) 
    ));

builder.Services.AddDbContext<Usercontext>(options =>
    options.UseMySql(
        connString,
        new MySqlServerVersion(new Version(8, 0, 40))
    ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Usercontext and UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
