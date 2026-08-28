using Learn.Core.DataAccess;
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
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddScoped<IStudentsRepository,StudentRepository>();
builder.Services.AddScoped<IStudentsService,StudentsService>();
builder.Services.AddDbContext<StudentsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))); // Ρύθμιση DbContext

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Database seeding through Migrations
using (var scope = app.Services.CreateAsyncScope())
{
    var studentsContext = scope.ServiceProvider.GetRequiredService<StudentsContext>();
    await studentsContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//added for typescript access
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
