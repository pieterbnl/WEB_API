CREATE PROCEDURE [dbo].[spTodos_Create]
	@Task nvarchar(50),
	@AssignedTo int
AS
BEGIN
	INSERT INTO dbo.Todos (Task, AssignedTo)
	VALUES (@Task, @AssignedTo);

	-- return full record with select statement
	SELECT Id, Task, AssignedTo, IsComplete
	FROM dbo.Todos
	WHERE Id = SCOPE_IDENTITY(); 
	-- SCOPE_IDENTITY returns the last created record id.. in a given scope / transaction 
	-- which is relevant in stored procedures as they create there own transaction
	-- @@IDENTITY returns identity of last created record id.. in the database	
END