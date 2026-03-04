using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Reports
{
    public class ReportsRepository
    {
        private readonly SqlDataAccess _db;

        public ReportsRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<DataTable> GetCurrentPayrollAsync()
        {
            return await _db.ExecuteQueryAsync("sp_GetCurrentPayroll", CommandType.StoredProcedure);
        }

        public async Task<DataTable> GetSalaryChangesAsync()
        {
            return await _db.ExecuteQueryAsync("sp_GetSalaryChanges", CommandType.StoredProcedure);
        }

        public async Task<DataTable> GetOrganizationStructureAsync()
        {
            return await _db.ExecuteQueryAsync("sp_GetOrganizationStructure", CommandType.StoredProcedure);
        }
    }
}
