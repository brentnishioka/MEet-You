using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Microsoft.Extensions.Configuration;                   //Ask to see if it is approved
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add API key
//var eventsApiKey = builder.Configuration["EventsAPI:ServiceApiKey"];

// Add Cors policy to allow from all origins
builder.Services.AddCors();

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
builder.Services.AddSingleton<CopyItineraryDAO>();
builder.Services.AddSingleton<ISuggestionManager, SuggestionManager>();
builder.Services.AddSingleton<ISuggestionDAO, SuggestionDAO>();
builder.Services.AddSingleton<IAPIService, EventAPIService>();
//builder.Services.AddSingleton<Configuration>();


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

// Add global Cors policies
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

//app.MapGet("/", () => eventsApiKey);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

