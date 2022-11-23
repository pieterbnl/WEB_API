using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoLibrary.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{

    // GET: api/todos
    [HttpGet]
    public ActionResult<IEnumerable<TodoModel>> Get()
    {        
        throw new NotImplementedException();            
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public ActionResult<TodoModel> Get(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/todos
    [HttpPost]
    public IActionResult Post([FromBody] TodoModel value)
    {
        throw new NotImplementedException();
    }

    // PUT api/todos/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TodoModel value)
    {
        throw new NotImplementedException();
    }

    // PUT api/todos/5/complete
    [HttpPut("{id}/Complete")]
    public IActionResult Complete(int id, [FromBody] TodoModel value)
    {
        throw new NotImplementedException();
    }

    // DELETE api/todos/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }
}