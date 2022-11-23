using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLibrary.Models;

namespace TodoLibrary.DataAccess;

public class TodoData : ITodoData
{
    private readonly ISqlDataAccess _sql;

    public TodoData(ISqlDataAccess sql)
    {
        _sql = sql;
    }
    public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
    {
        return _sql.LoadData<TodoModel, dynamic>(
            "dbo.spTodos_GetAllAssigned",
            new { AssignedTo = assignedTo }, // creating an anonymous object, passing in AssignedTo property - Dapper takes care
            "Default");
    }

    public async Task<TodoModel?> GetOneAssigned(int assignedTo, int todoId)  // async to be able to await results and apply .FirstOrDefault and just return TodoModel and not a list of it
    {
        var results = await _sql.LoadData<TodoModel, dynamic>( // dynamic required to be able to pass in an anonymous object
            "dbo.spTodos_GetOneAssigned",
            new { AssignedTo = assignedTo, TodoId = todoId }, // anonymous object with parameter AssignedTo
            "Default");

        return results.FirstOrDefault();
    }

    public async Task<TodoModel?> Create(int assignedTo, string task)
    {
        var results = await _sql.LoadData<TodoModel, dynamic>(
            "dbo.spTodos_Create",
            new { AssignedTo = assignedTo, Task = task },
            "Default");

        return results.FirstOrDefault();
    }

    public Task UpdateTask(int assignedTo, int todoId, string task)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTodos_UpdateTask",
            new { AssignedTo = assignedTo, TodoId = todoId, Task = task },
            "Default");
    }

    public Task CompleteTodo(int assignedTo, int todoId)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTodos_Complete",
            new { AssignedTo = assignedTo, TodoId = todoId },
            "Default");
    }

    public Task Delete(int assignedTo, int todoId)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTodos_Delete",
            new { AssignedTo = assignedTo, TodoId = todoId },
            "Default");
    }
}