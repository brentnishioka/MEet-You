using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Microsoft.Extensions.Configuration;                   //Ask to see if it is approved
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ASPNETCoreDemoDBContext services. (Dependency Injection for database)
var connection =
    System.Configuration.ConfigurationManager.
    ConnectionStrings["MEetAndYouDatabase"].ConnectionString;

builder.Services.AddDbContext<MEetAndYouDBContext>(options =>
     options.UseSqlServer(connection));

builder.Services.AddControllers();

//Dependency injection for Controllers
builder.Services.AddSingleton<AuthnManager>();
builder.Services.AddSingleton<CopyManager>();
builder.Services.AddSingleton<ICalendarManager, CalendarManager>();
builder.Services.AddSingleton<IRatingManager, RatingManager>();
builder.Services.AddSingleton<IRatingService, RatingService>();
builder.Services.AddSingleton<IRatingDAO, RatingDAO>();
builder.Services.AddSingleton<LoggingManager>();
builder.Services.AddSingleton<ILoggingService, LoggingService>();
builder.Services.AddSingleton<ILogDAO, LogDAO>();
builder.Services.AddSingleton<Log>();
builder.Services.AddSingleton<CopyItineraryDAO>();
builder.Services.AddScoped<UserEventRating>();
builder.Services.AddScoped<ItineraryNote>();

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

// Produciton settings
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

