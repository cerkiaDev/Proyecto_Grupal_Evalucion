using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Repositories
{
    // CRUD for Departments using ADO.NET
    public class DepartmentsRepository
    {
        private readonly SqlDataAccess _db;

        public DepartmentsRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var table = await _db.ExecuteQueryAsync("sp_GetAllDepartments", CommandType.StoredProcedure);
            var list = new List<Department>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Department
                {
                    Id = (int)row["Id"],
                    Name = row["Name"].ToString()
                });
            }
            return list;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var table = await _db.ExecuteQueryAsync("sp_GetDepartmentById", CommandType.StoredProcedure, new SqlParameter("@Id", id));
            if (table.Rows.Count == 0) return null;
            var row = table.Rows[0];
            return new Department
            {
                Id = (int)row["Id"],
                Name = row["Name"].ToString()
            };
        }

        public async Task<int> CreateAsync(Department department)
        {
            return await _db.ExecuteNonQueryAsync("sp_CreateDepartment", CommandType.StoredProcedure,
                new SqlParameter("@Name", department.Name));
        }

        public async Task<int> UpdateAsync(Department department)
        {
            return await _db.ExecuteNonQueryAsync("sp_UpdateDepartment", CommandType.StoredProcedure,
                new SqlParameter("@Id", department.Id),
                new SqlParameter("@Name", department.Name));
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _db.ExecuteNonQueryAsync("sp_DeleteDepartment", CommandType.StoredProcedure,
                new SqlParameter("@Id", id));
        }
    }
}
