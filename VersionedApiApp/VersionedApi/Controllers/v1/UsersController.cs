﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VersionedApi.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)]    
public class UsersController : ControllerBase
{
    // GET: api/v1/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "V1 value1", "V2 value2" };
    }
}