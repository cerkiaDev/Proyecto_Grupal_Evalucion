using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Repositories
{
    // Manage department managers with exclusivity validation
    public class DeptManagerRepository
    {
        private readonly SqlDataAccess _db;

        public DeptManagerRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<bool> HasManagerAsync(int deptId, DateTime fromDate, DateTime? toDate)
        {
            var table = await _db.ExecuteQueryAsync("sp_CheckDeptManagerOverlap", CommandType.StoredProcedure,
                new SqlParameter("@DeptId", deptId),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value));
            if (table.Rows.Count == 0) return false;
            var count = Convert.ToInt32(table.Rows[0][0]);
            return count > 0;
        }

        public async Task<int> AssignManagerAsync(DeptManager mgr)
        {
            if (await HasManagerAsync(mgr.DeptId, mgr.FromDate, mgr.ToDate))
                throw new InvalidOperationException("Department already has a manager for the specified period.");

            return await _db.ExecuteNonQueryAsync("sp_AssignDeptManager", CommandType.StoredProcedure,
                new SqlParameter("@EmployeeId", mgr.EmployeeId),
                new SqlParameter("@DeptId", mgr.DeptId),
                new SqlParameter("@FromDate", mgr.FromDate),
                new SqlParameter("@ToDate", mgr.ToDate ?? (object)DBNull.Value));
        }
    }
}
