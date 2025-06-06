using kAI_webAPI.Data;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.Question;
using kAI_webAPI.Models.User;
using kAI_webAPI.Models.Subjects;
using kAI_webAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Ensure this is included
using Microsoft.Extensions.DependencyInjection; // Ensure this is included
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using kAI_webAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration cf = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Fix for CS8600: Ensure the connection string is not null before using it.
string? connString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseMySql(
        connString,
        new MySqlServerVersion(new Version(8, 0, 40))
    ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "kAI Web API",
        Version = "v1",
        Description = "API for kAI application"
    });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register Usercontext and UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionsRepository, QuestionsRepository>();
builder.Services.AddScoped<ITranscriptRepository, TranscriptRepository>();

builder.Services.Configure<AppSetting>(cf.GetSection("AppSettings"));

var secretKey = cf["AppSettings:SecretKey"];
var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey ?? throw new InvalidOperationException("SecretKey is not configured."));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Xác thực token bằng chữ ký
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
        // Tự cấp phát hành token, không cần xác thực nhà phát hành và người nhận
        ValidateIssuer = false,
        ValidateAudience = false,

        ClockSkew = TimeSpan.Zero // Optional: Set to zero to avoid clock skew issues
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
