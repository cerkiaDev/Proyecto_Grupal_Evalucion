using Microsoft.Extensions.Configuration;
using Proyecto_Grupal.Data;
using Proyecto_Grupal.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Proyecto_Grupal.Repositories
{
    // CRUD for Titles/Cargos using ADO.NET and stored procedures
    public class TitlesRepository
    {
        private readonly SqlDataAccess _db;

        public TitlesRepository(IConfiguration configuration)
        {
            _db = new SqlDataAccess(configuration);
        }

        public async Task<IEnumerable<Title>> GetAllAsync()
        {
            var table = await _db.ExecuteQueryAsync("sp_GetAllTitles", CommandType.StoredProcedure);
            var list = new List<Title>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Title
                {
                    Id = (int)row["Id"],
                    Name = row["Name"].ToString()
                });
            }
            return list;
        }

        public async Task<Title> GetByIdAsync(int id)
        {
            var table = await _db.ExecuteQueryAsync("sp_GetTitleById", CommandType.StoredProcedure, new SqlParameter("@Id", id));
            if (table.Rows.Count == 0) return null;
            var row = table.Rows[0];
            return new Title
            {
                Id = (int)row["Id"],
                Name = row["Name"].ToString()
            };
        }

        public async Task<int> CreateAsync(Title title)
        {
            return await _db.ExecuteNonQueryAsync("sp_CreateTitle", CommandType.StoredProcedure,
                new SqlParameter("@Name", title.Name));
        }

        public async Task<int> UpdateAsync(Title title)
        {
            return await _db.ExecuteNonQueryAsync("sp_UpdateTitle", CommandType.StoredProcedure,
                new SqlParameter("@Id", title.Id),
                new SqlParameter("@Name", title.Name));
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _db.ExecuteNonQueryAsync("sp_DeleteTitle", CommandType.StoredProcedure,
                new SqlParameter("@Id", id));
        }
    }
}
