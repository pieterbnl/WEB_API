using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    // HARD CODED LOGIN DETAILS SOLUTION - FOR QUICK TESTING ONLY
    public record AuthenticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName);

    // api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        // validate credentials
        
    }
        
    private UserData ValidateCredentials(AuthenticationData data)
    {
        // NOT PRODUCTION CODE - FOR DEMO ONLY
        if (CompareValues(data.UserName, "jimmy") && 
            CompareValues(data.Password, "test123"))
        {
            return new UserData(1, data.UserName);
        }

        if (CompareValues(data.UserName, "johny") &&
           CompareValues(data.Password, "test123"))
        {
            return new UserData(2, data.UserName);
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