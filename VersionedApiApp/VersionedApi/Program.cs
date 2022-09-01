using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    // configure Swagger page
    var title = "My Versioned API";
    var description = "This is a Web API to experiment with versioning";
    var terms = new Uri("https://localhost:7110"); // could be used to link to terms of API
    
    var license = new OpenApiLicense()
    {
        Name = "This is my license info"
    };
    var contact = new OpenApiContact()
    {
        Name = "John Doe",
        Email = "help@johndoe.com",
        Url = new Uri("https://johndoe.com")
    };

    opts.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = $"{title} v1",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

    opts.SwaggerDoc("v2", new OpenApiInfo()
    {
        Version = "v2",
        Title = $"{title} v2",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

});

builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new(1, 0); // major & ninor version
    opts.ReportApiVersions = true; // shows version for accessed endpoint
});

builder.Services.AddVersionedApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV"; // indicates major and minor patches versions, like v1.2
    opts.SubstituteApiVersionInUrl = true; // tells Swagger to show dropdown for versions in corner
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");opts.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");        
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
