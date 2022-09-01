using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(opts =>
{
    // Adding custom claim policies
    opts.AddPolicy("MustHaveEmployeeId", policy =>
    {
        policy.RequireClaim("employeeId");
    });

    // The following ensures that, if no authorization policy is applied,
    // at least the user is required to be authenticated: the bare minimum to use the API.
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new()
        {
            // Do you want to validate the issuer? audience? signingkey?.. yes
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,

            // What does a valid issuer look like? 
            // This will take the token, look up the issuer (as above its indicated this is to be validated)
            // and compare it to builder.Configuration.GetValue<string>("Authentication:Issuer") 
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            
            // Same for audience and signingkey
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration.GetValue<string>("Authentication:SecretKey")))
        };
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();