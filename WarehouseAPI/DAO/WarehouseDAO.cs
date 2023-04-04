using WarehouseAPI.Model;
using System.Data.SqlClient;
using System.Globalization;

namespace WarehouseAPI.DAO
{
    public interface IWarehouseDAO
    {
        Task<bool> Create(ProductRequest request);
        Task<bool> Procedure(ProductRequest request);
    }

    public class WarehouseDAO : IWarehouseDAO
    {
        private const string _connString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";

        public async Task<bool> Create(ProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Procedure(ProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
