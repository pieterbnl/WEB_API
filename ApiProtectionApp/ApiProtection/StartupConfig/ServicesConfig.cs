using AspNetCoreRateLimit;

namespace ApiProtection.StartupConfig;

public static class ServicesConfig
{
    // extension method - allowing to call AddRateLimitServices on any IServiceCollection
    public static void AddRateLimitServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<IpRateLimitOptions>(
        builder.Configuration.GetSection("IpRateLimiting")); // call IpRateLimiting in appsettings.json
        builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>(); // using memory cache to store policy
        builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>(); // using memory cache to store rate limit counter
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>(); // implementation
        builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); // way to process those calls
        builder.Services.AddInMemoryRateLimiting();
    }
}