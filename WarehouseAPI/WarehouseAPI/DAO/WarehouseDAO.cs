using System.Data;
using System.Data.SqlClient;
using WarehouseAPI.Model;

namespace WarehouseAPI.DAO
{
    public interface IWarehouseDAO
    {
        Task<int> CountProductById(long idProduct);
        Task<int> CountProductWarehouseByOrderId(int idOrder);
        Task<int> CountWarehouseById(long idWarehouse);
        Task<int> Create(ProductWarehouse productWarehouse);
        Task<int> CreateProcedure(ProductRequest request);
        Task<List<Order>> GetOrdersByProductId(long idProduct);
        Task<Product> GetProductByOrder(Order order);
        Task UpdateFullifiledTimeOrder(Order order);
    }

    public class WarehouseDAO : IWarehouseDAO
    {
        private const string _connString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";

        public async Task<int> CountProductById(long idProduct)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new("SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@IdProduct", idProduct);

            await sqlConnection.OpenAsync();

            int count = (int)await sqlCommand.ExecuteScalarAsync();

            await sqlConnection.CloseAsync();

            return count;
        }

        public async Task<int> CountProductWarehouseByOrderId(int idOrder)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new("SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @idOrder", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idOrder", idOrder);

            await sqlConnection.OpenAsync();

            int count = (int)await sqlCommand.ExecuteScalarAsync();

            await sqlConnection.CloseAsync();

            return count;
        }

        public async Task<int> CountWarehouseById(long idWarehouse)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new("SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @idWarehouse", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idWarehouse", idWarehouse);

            await sqlConnection.OpenAsync();

            int count = (int)await sqlCommand.ExecuteScalarAsync();

            await sqlConnection.CloseAsync();

            return count;
        }

        public async Task<int> Create(ProductWarehouse productWarehouse)
        {
            using SqlConnection connection = new(_connString);
            using SqlCommand command = new("INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) OUTPUT INSERTED.IdProductWarehouse VALUES (@idWarehouse, @idProduct, @idOrder, @amount, @price, @createdAt)", connection);

            command.Parameters.AddWithValue("@idWarehouse", productWarehouse.IdWarehouse);
            command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@idOrder", productWarehouse.IdOrder);
            command.Parameters.AddWithValue("@amount", productWarehouse.Amount);
            command.Parameters.AddWithValue("@price", productWarehouse.Price);
            command.Parameters.AddWithValue("@createdAt", productWarehouse.CreatedAt);

            await connection.OpenAsync();

            int insertedId = (int)await command.ExecuteScalarAsync();
            await connection.CloseAsync();

            return insertedId;
        }

        public async Task<int> CreateProcedure(ProductRequest request)
        {
            var procedure = "AddProductToWarehouse";

            using var connection = new SqlConnection(_connString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            using var command = new SqlCommand(procedure, connection, transaction)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
            command.Parameters.AddWithValue("@Amount", request.Amount);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

            try
            {
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return 1; //should be id of product but procedure doesnt give it back
            }
            catch (SqlException exception)
            {
                await transaction.RollbackAsync();
                throw new Exception($"An error occurred while executing the {procedure} procedure: {exception.Message}", exception);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<List<Order>> GetOrdersByProductId(long idProduct)
        {
            List<Order> orders = new();
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;

            string sql = "SELECT * FROM \"Order\" WHERE IdProduct = @IdProduct";
            sqlCommand.Parameters.AddWithValue("@IdProduct", idProduct);

            sqlCommand.CommandText = sql;

            await sqlConnection.OpenAsync();

            using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (await sqlDataReader.ReadAsync())
            {
                Order order = new(
                    int.Parse(sqlDataReader["IdOrder"].ToString()),
                    int.Parse(sqlDataReader["IdProduct"].ToString()),
                    int.Parse(sqlDataReader["Amount"].ToString()),
                    DateTime.Parse(sqlDataReader["CreatedAt"].ToString())
                );

                orders.Add(order);
            }

            await sqlConnection.CloseAsync();

            return orders;
        }

        public async Task<Product> GetProductByOrder(Order order)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;

            string sql = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
            sqlCommand.Parameters.AddWithValue("@IdProduct", order.IdProduct);

            sqlCommand.CommandText = sql;

            await sqlConnection.OpenAsync();

            using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (await sqlDataReader.ReadAsync())
            {
                return new Product(
                    long.Parse(sqlDataReader["IdProduct"].ToString()),
                    sqlDataReader["Name"].ToString(),
                    sqlDataReader["Description"].ToString(),
                    double.Parse(sqlDataReader["Price"].ToString())
                );
            }

            await sqlConnection.CloseAsync();

            return null;
        }

        public async Task UpdateFullifiledTimeOrder(Order order)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new("UPDATE \"Order\" SET FulfilledAt = @fulfilledAt WHERE IdOrder = @IdOrder", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@IdOrder", order.IdOrder);
            sqlCommand.Parameters.AddWithValue("@fulfilledAt", DateTime.Now);

            await sqlConnection.OpenAsync();

            int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            await sqlConnection.CloseAsync();
        }
    }
}
