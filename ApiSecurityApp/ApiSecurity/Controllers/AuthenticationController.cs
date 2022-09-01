using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    // HARD CODED LOGIN DETAILS SOLUTION - FOR QUICK TESTING ONLY
    public record AuthenticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName, string Title, string EmployeeId);

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    // api/Authentication/token
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        // validate credentials
        var user = ValidateCredentials(data);

        if (user is null)
        {
            return Unauthorized();
        }

        // generate token
        var token = GenerateToken(user);

        return Ok(token);
    }

    private string GenerateToken(UserData user)
    {
        // Encoding converts the given secret string into an ASCII byte array
        // which is then passed into to SymmetricSecurityKey, to create a new instance
        // Note that : is used to navigate one level down under Authentication (in secrets.json) to grap SecretKey
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                _config.GetValue<string>("Authentication:SecretKey")));

        // Signature for token
        // If the signature doesn't match anymore, it means the token has been tampered with
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // Add claims: datapoints about the user, to be verified
        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserId.ToString())); // verify the subject is the user
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName)); // verify the username is the user
        claims.Add(new("title", user.Title));
//        claims.Add(new("employeeId", user.EmployeeId));

        // Build token
        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow, // when token becomes valid (as of right now)
            DateTime.UtcNow.AddMinutes(1), // when token expires
            signingCredentials: signingCredentials); // signing of token, to ensure it cannot be modified without becoming invalid

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
        
    
    // Dummy lookup
    private UserData ValidateCredentials(AuthenticationData data)
    {
        // NOT PRODUCTION CODE - FOR DEMO ONLY
        if (CompareValues(data.UserName, "jimmy") && 
            CompareValues(data.Password, "test123"))
        {
            return new UserData(1, data.UserName, "CEO", "E001");
        }

        if (CompareValues(data.UserName, "johny") &&
           CompareValues(data.Password, "test123"))
        {
            return new UserData(2, data.UserName, "Sales Engineer", "E013");
        }

        return null;
    }

    private bool CompareValues(string? actual, string expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected)) //, StringComparison.InvariantCultureIgnoreCase
            {
                return true;
            }
        }
        
        return false;
    }
}