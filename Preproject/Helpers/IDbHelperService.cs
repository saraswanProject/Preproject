

using Dapper;
using System.Data;

namespace Preproject.Helpers
{
    public interface IDbHelperService : IDisposable
    {
        Task<IEnumerable<T>> ExecuteQuery<T>(string sql, DynamicParameters param = null, bool isproc = false);
        Task<dynamic> ExecuteQueryDynamic(string sql, DynamicParameters param = null, bool isproc = false);
        void Execute(string sql, DynamicParameters param = null, bool isproc = false);
        DataTable ConvertToDataTable<T>(IList<T> data);
        public DataTable ExecuteDATATable(string sql);
        public DataTable ExecuteQueryTableSync(string sql, DynamicParameters param = null, bool isproc = false);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql,  DynamicParameters param = null, bool isproc = false);

        T QuerySingleOrDefault<T>( string sql, DynamicParameters param = null,bool isproc = false);
    }
}