using Microsoft.EntityFrameworkCore;
using RunTracker.API.Data;
using RunTracker.API.Models;
using RunTracker.API.Data.Repositories;
using RunTracker.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Logging.AddConsole(); 

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB DI
builder.Services.AddDbContext<RunTrackerDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<RunActivity>, RunActivityRepository>();

//Services DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRunActivityService, RunActivityService>();

var app = builder.Build();

// Configure logging in the application
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();  // Add console logging

});

ILogger logger = loggerFactory.CreateLogger<Program>();

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