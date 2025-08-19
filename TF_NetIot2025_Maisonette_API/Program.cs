using Microsoft.EntityFrameworkCore;
using TF_NetIot2025_Maisonette_API.Entities.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>( o => 
    o.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=MaisonetteDb;integrated security=true;trust server certificate=true")
);

builder.Services.AddCors(p => p.AddDefaultPolicy(b => b
    .WithOrigins("http://localhost:4200")
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
