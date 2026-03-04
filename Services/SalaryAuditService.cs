using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Services
{
    public class SalaryAuditService
    {
        private readonly SqlDataAccess _db;

        public SalaryAuditService(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task LogSalaryChangeAsync(int employeeId, decimal oldSalary, decimal newSalary, string changedBy)
        {
            await _db.ExecuteNonQueryAsync("sp_LogSalaryChange", CommandType.StoredProcedure,
                new SqlParameter("@EmployeeId", employeeId),
                new SqlParameter("@OldSalary", oldSalary),
                new SqlParameter("@NewSalary", newSalary),
                new SqlParameter("@ChangedAt", DateTime.UtcNow),
                new SqlParameter("@ChangedBy", changedBy));
        }
    }
}
