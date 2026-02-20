

﻿using Dapper;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;

using Microsoft.Data.SqlClient;

namespace Preproject.Helpers
{
    public class DbHelperService : IDisposable, IDbHelperService
    {
        private readonly IConfiguration _configuration;
        public DbHelperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private DbConnection GetConn()
        {
            DbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection;
        }
        public async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, DynamicParameters param = null, bool isproc = false)
        {
            DbConnection conn = GetConn();
            return await conn.QueryAsync<T>(sql, param, commandType: (isproc) ? CommandType.StoredProcedure : CommandType.Text);

        }
        public async Task<dynamic> ExecuteQueryDynamic(string sql, DynamicParameters param = null, bool isproc = false)
        {
            DbConnection conn = GetConn();
            return await conn.QueryAsync<dynamic>(sql, param, commandType: (isproc) ? CommandType.StoredProcedure : CommandType.Text);

        }
        public void Execute(string sql, DynamicParameters param = null, bool isproc = false)
        {
            DbConnection conn = GetConn();
            conn.Execute(sql, param, commandType: (isproc) ? CommandType.StoredProcedure : CommandType.Text);
        }
        public void Dispose()
        {
            if (GetConn().State == ConnectionState.Open)
                GetConn().Close();
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            string json = JsonConvert.SerializeObject(data);
            table = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            return table;

        }

        public DataTable ExecuteDATATable(string sql)
        {
            DbConnection conn = GetConn();
            var reader = conn.ExecuteReader(sql);

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }

        public DataTable ExecuteQueryTableSync(string sql, DynamicParameters param = null, bool isproc = false)
        {
            using (DbConnection conn = GetConn())
            {
                conn.Open(); // Open connection

                using (var reader = conn.ExecuteReader(sql, param, commandType: isproc ? CommandType.StoredProcedure : CommandType.Text))
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
        }
        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql,DynamicParameters param = null, bool isproc = false)
        {
            using (DbConnection conn = GetConn())
            {
                return await conn.QuerySingleOrDefaultAsync<T>(
                    sql,
                    param,
                    commandType: isproc ? CommandType.StoredProcedure : CommandType.Text
                );
            }
        }



        public T QuerySingleOrDefault<T>(string sql,DynamicParameters param = null,  bool isproc = false)
        {
            using (DbConnection conn = GetConn())
            {
                return conn.QuerySingleOrDefault<T>(
                    sql,
                    param,
                    commandType: isproc ? CommandType.StoredProcedure : CommandType.Text
                );
            }
        }



    }
}
