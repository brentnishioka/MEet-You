using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities.DBModels;
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
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using System.Web.Http;
using System.Web.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddCors(options => 
{
    options.AddPolicy("MEetAndYouPolicy",
        policy => {
            policy.WithOrigins("https://localhost:3000/");
                      });
});
*/

//Add API key
var eventsApiKey = builder.Configuration["EventsAPI:ServiceApiKey"];

// Add Cors policy to allow from all origins
builder.Services.AddCors();

// Add ASPNETCoreDemoDBContext services. (Dependency Injection for database)
var connection =
    System.Configuration.ConfigurationManager.
    ConnectionStrings["MEetAndYouDatabase"].ConnectionString;

builder.Services.AddDbContext<MEetAndYouDBContext>(options =>
     options.UseSqlServer(connection));

//trying to add cors
//builder.Services.AddCors();
//builder.Services.AddCors(options => {
//    options.AddDefaultPolicy(
//        builder => {
//            builder.WithOrigins("https://*") //change 
//                                .AllowAnyHeader()
//                                .AllowAnyMethod();
//        });
//});
/*builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        builder => {
            builder.AllowAnyOrigin() //change 
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});*/


builder.Services.AddControllers();

//Dependency injection for Controllers
builder.Services.AddSingleton<AuthnManager>();
builder.Services.AddSingleton<CopyManager>();
builder.Services.AddSingleton<IAuthorizationManager, AuthorizationManager>();
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
builder.Services.AddSingleton<ISuggestionManager, SuggestionManager>();
builder.Services.AddSingleton<ISuggestionDAO, SuggestionDAO>();
builder.Services.AddSingleton<IAPIService, EventAPIService>();
//builder.Services.AddSingleton<Configuration>();

builder.Services.AddSingleton<ICalendarDAO, CalendarDAO>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors block 
var app = builder.Build();

//trying to add cors
app.UseCors();
app.UseCors(builder => {
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
//end cors


/*app.options('*', function(req, res){
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Methods', 'GET,PUT,HEAD,POST,PATCH');
    res.header('Access-Control-Allow-Headers',
    'Authorization,Origin,Referer,Content-Type,Accept,User-Agent');
    res.sendStatus(200);
    res.end();
});*/

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

