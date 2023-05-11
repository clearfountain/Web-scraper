using Microsoft.OpenApi.Models;
using Scraping.Application.Contracts.Infrastructure;
using Scraping.Application.Contracts.Persisitence;
using Scraping.Infrastructure.Reporting;
using Scraping.Infrastructure.Repositories;
using Scraping.Infrastructure.Scraper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Scraper API", Version = "v1" });
});

builder.Services.AddScoped<IScraperService, ScraperService>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IReportingService, ReportingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Scraper API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
