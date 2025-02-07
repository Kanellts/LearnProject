using Learn.Core;
using Learn.Core.DataAccess.Models;
using Learn.Core.DataAccess;
using Learn.Core.Logger;
using Learn.Core.Migrations;
using Learn.Core.Repository;
using Learn.Core.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // Ορίζει το path όπου βρίσκεται το appsettings.json
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Φορτώνει το αρχείο
            .Build();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IStudentsService,StudentsService>();
builder.Services.AddSingleton<IStudentsRepository,StudentRepository>();
builder.Services.AddSingleton<IStudentLogger,StudentLogger>();
builder.Services.AddDbContext<StudentsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))); // Ρύθμιση DbContext

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
