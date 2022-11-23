using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TodoLibrary.DataAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        // connection to db
        // using statement ensures that it will run dispose method on this object, no matter what    
        using IDbConnection connection = new SqlConnection(connectionString);

        // .Query comes from Dapper, passing in storedProcedure and parameters, indicating return type is going to be StoredProcedure        
        var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);

        // rows will be a IEnumerable<T>, convert to a list        
        return rows.ToList();
    }

    public Task SaveData<T>( // note: not asynchronous as there's no need to wait
        string storedProcedure,
        T parameters,
        string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        return connection.ExecuteAsync( // returning a Task
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}