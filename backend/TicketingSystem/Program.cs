using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data;
using TicketingSystem.Repositories;
using TicketingSystem.Repositories.Interfaces;
using TicketingSystem.Services;
using TicketingSystem.Services.Interfaces;
using TicketingSystem.Helpers;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Controllers
// =======================
builder.Services.AddControllers();

// =======================
// Swagger
// =======================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =======================
// SQL SERVER Connection ✅
// =======================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// =======================
// Dependency Injection
// =======================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>(); // clean reference

// =======================
// JWT CONFIG
// =======================
var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"] ?? throw new Exception("JWT Key missing"))
        )
    };
});

builder.Services.AddAuthorization();

// =======================
// Build App
// =======================
var app = builder.Build();

// =======================
// Global Exception Middleware
// =======================
app.UseMiddleware<ExceptionMiddleware>();

// =======================
// Swagger
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// =======================
// Middleware Pipeline
// =======================
app.UseHttpsRedirection();

app.UseAuthentication();   // 🔐 IMPORTANT
app.UseAuthorization();

app.MapControllers();

// =======================
// AUTO DATABASE CREATE (DEV ONLY)
// =======================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();