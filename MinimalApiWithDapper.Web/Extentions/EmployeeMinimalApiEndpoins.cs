using Dapper;
using Microsoft.Data.SqlClient;
using MinimalApiWithDapper.Web.Data.Entites;

namespace MinimalApiWithDapper.Web.Extentions
{
    public static class EmployeeMinimalApiEndpoins
    {
        public static void EmployeeEndpoints(this WebApplication app) 
        {
         var group =   app.MapGroup("Employee");

            group.MapGet("Employee", async (IConfiguration configuration) =>
            {
                var connextionstring = configuration.GetConnectionString("DefultConnection");
                using var connection = new SqlConnection(connextionstring);

                const string sqlCommand = "SELECT * FROM Employee";

                var Employess = await connection.QueryAsync<Employee>(sqlCommand);

                return Results.Ok(Employess);

            });

            group.MapGet("Employee/{id}", async (int id, IConfiguration configuration) =>
            {
                var connextionstring = configuration.GetConnectionString("DefultConnection");
                using var connection = new SqlConnection(connextionstring);

                const string sqlCommand = """
                                            select *
                                            from Employee e
                                            where e.id = @Empid 
                                           """;

                var Employess = await connection.QuerySingleOrDefaultAsync<Employee>(sqlCommand, new { Empid = id });
                if (Employess is not null)
                    return Results.Ok(Employess);

                return Results.NotFound();

            });


            //update employee

            group.MapPut("Employee", async (Employee employee, IConfiguration configuration) =>
            {
                var connextionstring = configuration.GetConnectionString("DefultConnection");
                using var connection = new SqlConnection(connextionstring);

                const string sqlCommand = """
                                           UPDATE Employee
                                           SET Name = @Name, Salary = @Salary
                                           WHERE id = @Id
                                           """;

                var result = await connection.ExecuteAsync(sqlCommand, employee);

                return Results.Ok(result == 1 ? "Employee Updeted Successfuly" : "Erorr  ");



            });

            group.MapPost("Employee", async (Employee employee, IConfiguration configuration) =>
            {
                var connextionstring = configuration.GetConnectionString("DefultConnection");
                using var connection = new SqlConnection(connextionstring);

                const string sqlCommand = """
                                           INSERT INTO Employee(id,Name,Salary)
                                           VALUES (@Id,@Name, @Salary)
                                           """;

                var result = await connection.ExecuteAsync(sqlCommand, employee);

                return Results.Ok(result == 1 ? "Employee Added Successfuly" : "Erorr  ");



            });


            group.MapDelete("Employee/{id}", async (int id, IConfiguration configuration) =>
            {
                var connextionstring = configuration.GetConnectionString("DefultConnection");
                using var connection = new SqlConnection(connextionstring);

                const string sqlCommand = """
                                           DELETE FROM Employee
                                           WHERE id = @EmpId

                                           
                                           """;

                var result = await connection.ExecuteAsync(sqlCommand, new { Empid = id });

                return Results.Ok(result == 1 ? "Employee Added Successfuly" : "Erorr  ");



            });
        }

    }
}
