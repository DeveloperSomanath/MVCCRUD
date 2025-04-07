ALTER PROCEDURE [dbo].[Sp_GetAllEmployees]
    @PageNumber INT = 2,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Employees
    ORDER BY PersonId 
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END