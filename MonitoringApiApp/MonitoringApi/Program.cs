using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MonitoringApi.HealthChecks;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Site health check")
    .AddCheck<RandomHealthCheck>("Db health check");

builder.Services.AddWatchDogServices();

builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health"); // api monitoring itself via /health - note: can also point to multiple url's
    opts.SetEvaluationTimeInSeconds(5); // carrying out a health check every 5 seconds
    opts.SetMinimumSecondsBetweenFailureNotifications(10); // only send notifications every 10 seconds upon failure - note: typically, 5 minutes or so is a good value
}).AddInMemoryStorage(); // store logging in memory, for demonstration purposes

var app = builder.Build();

app.UseWatchDogExceptionLogger(); // logs any unhandled exceptions

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{ 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse // tells how to format data that its gotten
});

app.MapHealthChecksUI();

app.UseWatchDog(opts =>
{
    // Get username & password from configuration file
    opts.WatchPageUsername = app.Configuration.GetValue<string>("WatchDog:UserName");
    opts.WatchPagePassword = app.Configuration.GetValue<string>("WatchDog:Password");
    
    // Prevent monitoring of /health endpoint
    opts.Blacklist = "health";
});

app.Run();