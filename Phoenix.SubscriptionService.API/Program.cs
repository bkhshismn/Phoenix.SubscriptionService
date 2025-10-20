using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Phoenix.SubscriptionService.Application.Interfaces;
using Phoenix.SubscriptionService.Application.Services;
using Phoenix.SubscriptionService.Infrastructure.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===== Add Services =====
builder.Services.AddControllers();

// ===== DbContext (SQLite) =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=subscription.db"));

// ===== Application Services =====
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

// ===== Repositories =====
builder.Services.AddScoped(typeof(Phoenix.SubscriptionService.Domain.Interfaces.IRepository<>), typeof(Phoenix.SubscriptionService.Infrastructure.Data.Repository<>));
builder.Services.AddScoped<Phoenix.SubscriptionService.Domain.Interfaces.ISubscriptionRepository, Phoenix.SubscriptionService.Infrastructure.Data.SubscriptionRepository>();

// ===== JWT Authentication =====
var jwtKey = "THIS_IS_A_SECRET_KEY_FOR_JWT_1234567890";
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== Middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ===== Seed Database (Optional) =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Plans.Any())
    {
        db.Plans.Add(new Phoenix.SubscriptionService.Domain.Entities.Plan
        {
            Name = "Standard",
            Description = "Standard Plan",
            Price = 10,
            DurationInDays = 30,
            Type = Phoenix.SubscriptionService.Domain.Enums.PlanType.Standard
        });
        db.Plans.Add(new Phoenix.SubscriptionService.Domain.Entities.Plan
        {
            Name = "Premium",
            Description = "Premium Plan",
            Price = 30,
            DurationInDays = 30,
            Type = Phoenix.SubscriptionService.Domain.Enums.PlanType.Premium
        });
        db.Plans.Add(new Phoenix.SubscriptionService.Domain.Entities.Plan
        {
            Name = "Custom",
            Description = "Custom Plan",
            Price = 50,
            DurationInDays = 30,
            Type = Phoenix.SubscriptionService.Domain.Enums.PlanType.Custom
        });
        db.SaveChanges();
    }
}

app.Run();
