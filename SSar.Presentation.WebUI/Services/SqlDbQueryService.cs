using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SSar.Presentation.WebUI.Services
{
    public class SqlDbQueryService : IQueryService
    {
        private readonly string _connectionString;

        public SqlDbQueryService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<T> Query<T>(string sqlQuery, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<T>(sqlQuery, param);
            }
        }

        public async Task<List<T>> ListQuery<T>(string sqlQuery)
        {
            // TODO: Research using parameterized queries with Dappe

            using (var connection = new SqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<T>(sqlQuery)).ToList();
            }

        }
    }
}
