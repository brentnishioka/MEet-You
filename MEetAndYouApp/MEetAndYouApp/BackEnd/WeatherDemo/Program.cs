using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Microsoft.Extensions.Configuration;                   //Ask to see if it is approved

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ASPNETCoreDemoDBContext services. (Dependency Injection for database)
var connection =
    System.Configuration.ConfigurationManager.
    ConnectionStrings["MEetAndYouDatabase"].ConnectionString;

builder.Services.AddDbContext<MEetAndYouDBContext>(options =>
     options.UseSqlServer(connection));


builder.Services.AddControllers();
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

