using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173",
        builder => builder
            .WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()                      
            .AllowAnyMethod()                     
    );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Ensure the database is migrated
    context.Database.Migrate();

    // Add test customers if the table is empty
    if (!context.Klanten.Any())
    {
        context.Klanten.AddRange(
            new KlantDTO { Naam = "Jan Jansen", Email = "jan.jansen@example.com" },
            new KlantDTO { Naam = "Marie de Vries", Email = "marie.devries@example.com" },
            new KlantDTO { Naam = "Piet Klaassen", Email = "piet.klaassen@example.com" }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy to allow requests from localhost:5173
app.UseCors("AllowLocalhost5173");

app.UseAuthorization();

app.MapControllers();

app.Run();
