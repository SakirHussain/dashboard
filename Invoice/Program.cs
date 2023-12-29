using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Invoice.Data;
using Invoice.Repositories;
using Invoice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add the ApplicationDbContext to the services with DbContextOptionsFactory.
builder.Services.AddDbContext<ApplicationDbContext>();

// Set up and configure connection strings for different databases.
Dictionary<string, string> connStrs = new Dictionary<string, string>();
connStrs.Add("EInvoice", builder.Configuration["ConnectionStrings:EInvoice"]);
connStrs.Add("EwayBillOfficer", builder.Configuration["ConnectionStrings:EwayBillOfficer"]);
connStrs.Add("DefaultConnect", builder.Configuration["ConnectionStrings:DefaultConnect"]);
DbContextOptionsFactory.SetConnectionString(connStrs);

// Add HttpClient to services.
builder.Services.AddHttpClient();

// Register repositories and interfaces in the dependency injection container.
builder.Services.AddScoped(typeof(DatabaseOperationsInterface), typeof(DatabaseOperations));
builder.Services.AddScoped(typeof(HeaderVerificationInterface), typeof(HeaderVerification));

// Add controllers to services.
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI for development environment.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable authorization.
app.UseAuthorization();

// Map controllers.
app.MapControllers();

app.Run();
