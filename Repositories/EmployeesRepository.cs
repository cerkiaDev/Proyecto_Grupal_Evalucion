using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Repositories
{
    // Full CRUD for Employees using ADO.NET and stored procedures
    public class EmployeesRepository
    {
        private readonly SqlDataAccess _db;

        public EmployeesRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            // Example uses stored procedure with pagination
            var table = await _db.ExecuteQueryAsync("sp_GetEmployeesPaged", CommandType.StoredProcedure,
                new SqlParameter("@Page", page), new SqlParameter("@PageSize", pageSize));
            var list = new List<Employee>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Employee
                {
                    Id = (int)row["Id"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                });
            }
            return list;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var table = await _db.ExecuteQueryAsync("sp_GetEmployeeById", CommandType.StoredProcedure, new SqlParameter("@Id", id));
            if (table.Rows.Count == 0) return null;
            var row = table.Rows[0];
            return new Employee
            {
                Id = (int)row["Id"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
            };
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            return await _db.ExecuteNonQueryAsync("sp_CreateEmployee", CommandType.StoredProcedure,
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName));
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            return await _db.ExecuteNonQueryAsync("sp_UpdateEmployee", CommandType.StoredProcedure,
                new SqlParameter("@Id", employee.Id),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName));
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _db.ExecuteNonQueryAsync("sp_DeleteEmployee", CommandType.StoredProcedure,
                new SqlParameter("@Id", id));
        }
    }
}
