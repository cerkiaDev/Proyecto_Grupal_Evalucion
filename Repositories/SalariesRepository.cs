using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Repositories
{
    // CRUD for Salaries using ADO.NET with vigencias and calculation of current salary
    public class SalariesRepository
    {
        private readonly SqlDataAccess _db;

        public SalariesRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<IEnumerable<Salary>> GetAllAsync(int employeeId)
        {
            var table = await _db.ExecuteQueryAsync("sp_GetSalariesByEmployee", CommandType.StoredProcedure, new SqlParameter("@EmployeeId", employeeId));
            var list = new List<Salary>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Salary
                {
                    Id = (int)row["Id"],
                    EmployeeId = (int)row["EmployeeId"],
                    Amount = Convert.ToDecimal(row["Amount"]),
                    FromDate = Convert.ToDateTime(row["FromDate"]),
                    ToDate = row["ToDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ToDate"])
                });
            }
            return list;
        }

        public async Task<Salary> GetCurrentSalaryAsync(int employeeId)
        {
            var table = await _db.ExecuteQueryAsync("sp_GetCurrentSalaryByEmployee", CommandType.StoredProcedure, new SqlParameter("@EmployeeId", employeeId));
            if (table.Rows.Count == 0) return null;
            var row = table.Rows[0];
            return new Salary
            {
                Id = (int)row["Id"],
                EmployeeId = (int)row["EmployeeId"],
                Amount = Convert.ToDecimal(row["Amount"]),
                FromDate = Convert.ToDateTime(row["FromDate"]),
                ToDate = row["ToDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ToDate"])
            };
        }

        public async Task<int> CreateAsync(Salary salary)
        {
            return await _db.ExecuteNonQueryAsync("sp_CreateSalary", CommandType.StoredProcedure,
                new SqlParameter("@EmployeeId", salary.EmployeeId),
                new SqlParameter("@Amount", salary.Amount),
                new SqlParameter("@FromDate", salary.FromDate),
                new SqlParameter("@ToDate", salary.ToDate ?? (object)DBNull.Value));
        }

        public async Task<int> UpdateAsync(Salary salary)
        {
            return await _db.ExecuteNonQueryAsync("sp_UpdateSalary", CommandType.StoredProcedure,
                new SqlParameter("@Id", salary.Id),
                new SqlParameter("@Amount", salary.Amount),
                new SqlParameter("@FromDate", salary.FromDate),
                new SqlParameter("@ToDate", salary.ToDate ?? (object)DBNull.Value));
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _db.ExecuteNonQueryAsync("sp_DeleteSalary", CommandType.StoredProcedure,
                new SqlParameter("@Id", id));
        }
    }
}
