var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// CORS 
builder.Services.AddCors();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(a =>
{
    a.AllowAnyOrigin();
    a.AllowAnyMethod();
    a.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
