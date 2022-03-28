using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ASPNETCoreDemoDBContext services. (Dependency Injection for database)
//builder.Services.AddDbContext<MEetAndYouDBContext>(
//    options => options.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["MEetAndYouDatabase"].ConnectionString));
//builder.Services.AddDbContext<MEetAndYouDBContext>(options =>
//     options.UseSqlServer(
//        Configuration.GetConnectionString("MEetAndYouDatabase")));

builder.Services.AddDbContext<MEetAndYouDBContext>();

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

