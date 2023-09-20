using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// CORS 
builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(a =>
    {
        a.RequireHttpsMetadata = false;
        a.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("bohrbohrbohrbohrbohrbohrbohrbohr"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(a =>
{
    a.AllowAnyOrigin();
    a.AllowAnyMethod();
    a.AllowAnyHeader();
});

app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();

app.Run();
