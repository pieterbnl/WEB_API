CREATE PROCEDURE [dbo].[spTodos_GetAllAssigned]	
	@AssignedTo int
AS
BEGIN
	SELECT *
	FROM dbo.Todos
	WHERE AssignedTo = @AssignedTo;
END