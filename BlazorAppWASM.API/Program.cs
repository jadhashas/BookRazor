using Microsoft.EntityFrameworkCore;
using BlazorAppWASM.Services;
using BlazorAppWASM.DAL.Data;
using BlazorAppWASM.Services.Interfaces;
using BlazorAppWASM.Services.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Configuration du DbContext
builder.Services.AddDbContext<DbContexte>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILivreService, LivreService>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
