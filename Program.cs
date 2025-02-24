//using ExceptionLogsTask.Data;
//using ExceptionLogsTask.Logging;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;

//var builder = WebApplication.CreateBuilder(args);

//// Get the connection string for the database
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//// Configure logging to database
//builder.Logging.ClearProviders();
//builder.Logging.AddProvider(new DatabaseLoggerProvider(connectionString));

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Register custom DatabaseLogger as a service for dependency injection
//builder.Services.AddSingleton<DatabaseLogger>(new DatabaseLogger(connectionString));

//var app = builder.Build();

//// Configure Swagger UI
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//// Configure HTTP request pipeline.
//app.UseHttpsRedirection();
//app.MapControllers();

//app.Run();





//using ExceptionLogsTask.Data;
//using ExceptionLogsTask.Logging;
//using Microsoft.Extensions.Logging;
//using Serilog;

//var builder = WebApplication.CreateBuilder(args);

//// Get the connection string for the database
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//// Step 1: Configure Serilog (File Logging)
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Day)  // Logs to "logs/myapp.log"
//    .CreateLogger();

//// Step 2: Clear default providers and add custom providers (Database + File)
//// builder.Logging.ClearProviders();

//// Add custom DatabaseLogger provider
//builder.Logging.AddProvider(new DatabaseLoggerProvider(connectionString));

//// Add File Logger (Serilog)
//builder.Logging.AddSerilog();

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Register custom DatabaseLogger as a service for dependency injection
//builder.Services.AddSingleton<DatabaseLogger>(new DatabaseLogger(connectionString));

//var app = builder.Build();

//// Configure Swagger UI in development
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//// HTTP request pipeline configuration
//app.UseHttpsRedirection();
//app.MapControllers();

//// Run the app
//app.Run();




using ExceptionLogsTask.Data;
using ExceptionLogsTask.Logging;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;  // Use the Compact Formatter

var builder = WebApplication.CreateBuilder(args);

// Set up Serilog with File Sink using Compact format
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(new CompactJsonFormatter(), "logs/myapp.log", rollingInterval: RollingInterval.Day)  // Log to "logs/myapp.log" in Compact format
    .CreateLogger();

// Configure logging to use both DatabaseLoggerProvider and Serilog (file logging)
builder.Logging.ClearProviders();  // Clear default providers
builder.Logging.AddProvider(new DatabaseLoggerProvider(builder.Configuration.GetConnectionString("DefaultConnection")));  // Custom DB logger provider
builder.Logging.AddSerilog();  // Add Serilog for file logging

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom DatabaseLogger as a service for dependency injection
builder.Services.AddSingleton<DatabaseLogger>(new DatabaseLogger(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTP request pipeline configuration
app.UseHttpsRedirection();
app.MapControllers();

// Run the app
app.Run();


