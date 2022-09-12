using ApiProtection.StartupConfig;
using AspNetCoreRateLimit; // using statement unnecessary, for demo purposes only

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching(); // add caching service - will inform client that browser can cache data as well

builder.Services.AddMemoryCache(); // add memory cache service to cache client call information, to keep track of rate limiting
builder.AddRateLimitServices(); // add rate limiting services

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching(); // use caching service

app.UseAuthorization();

app.MapControllers();

app.UseIpRateLimiting(); // enable rate limiting service

app.Run();