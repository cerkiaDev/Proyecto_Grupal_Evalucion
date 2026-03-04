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
    // Assignment of employees to departments with overlap validation
    public class DeptEmpRepository
    {
        private readonly SqlDataAccess _db;

        public DeptEmpRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<bool> HasOverlapAsync(int employeeId, DateTime fromDate, DateTime? toDate)
        {
            var table = await _db.ExecuteQueryAsync("sp_CheckDeptEmpOverlap", CommandType.StoredProcedure,
                new SqlParameter("@EmployeeId", employeeId),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value));
            // expect stored procedure returns count
            if (table.Rows.Count == 0) return false;
            var count = Convert.ToInt32(table.Rows[0][0]);
            return count > 0;
        }

        public async Task<int> AssignAsync(DeptEmp assignment)
        {
            // validate overlap first (could be also enforced in DB)
            if (await HasOverlapAsync(assignment.EmployeeId, assignment.FromDate, assignment.ToDate))
                throw new InvalidOperationException("Assignment overlaps with existing one.");

            return await _db.ExecuteNonQueryAsync("sp_AssignEmployeeToDept", CommandType.StoredProcedure,
                new SqlParameter("@EmployeeId", assignment.EmployeeId),
                new SqlParameter("@DeptId", assignment.DeptId),
                new SqlParameter("@FromDate", assignment.FromDate),
                new SqlParameter("@ToDate", assignment.ToDate ?? (object)DBNull.Value));
        }
    }
}
