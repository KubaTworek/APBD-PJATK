using AnimalAPI.Model;
using System.Data.SqlClient;
using System.Globalization;

namespace AnimalAPI.DAO
{
    public interface IAnimalDAO
    {
        Task<IList<Animal>> GetAll(string orderBy);
        Task<bool> Create(Animal animal);
        Task<bool> Update(Animal animal, long id);
        Task<bool> Delete(long id);
    }

    public class AnimalDAO : IAnimalDAO
    {
        private const string _connString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";

        public async Task<IList<Animal>> GetAll(string orderBy)
        {
            List<Animal> animals = new();

            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;

            string sql = "SELECT * FROM Animal";

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var columnNames = new List<string> { "Name", "Description", "Category", "Area" };
                if (!columnNames.Contains(orderBy))
                {
                    orderBy = "Name";
                }
                sql += $" ORDER BY [{orderBy}]";
                sqlCommand.Parameters.AddWithValue("@orderBy", orderBy);
            }

            sqlCommand.CommandText = sql;

            await sqlConnection.OpenAsync();

            using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (await sqlDataReader.ReadAsync())
            {
                Animal animal = new(
                    int.Parse(sqlDataReader["IdAnimal"].ToString()),
                    sqlDataReader["Name"].ToString(),
                    sqlDataReader["Description"].ToString(),
                    sqlDataReader["Category"].ToString(),
                    sqlDataReader["Area"].ToString()
                );

                animals.Add(animal);
            }

            await sqlConnection.CloseAsync();

            return animals;
        }

        public async Task<bool> Create(Animal animal)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new("INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
            sqlCommand.Parameters.AddWithValue("@Description", animal.Description);
            sqlCommand.Parameters.AddWithValue("@Category", animal.Category);
            sqlCommand.Parameters.AddWithValue("@Area", animal.Area);

            await sqlConnection.OpenAsync();

            int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            await sqlConnection.CloseAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> Update(Animal animal, long id)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new("UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@IdAnimal", id);
            sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
            sqlCommand.Parameters.AddWithValue("@Description", animal.Description);
            sqlCommand.Parameters.AddWithValue("@Category", animal.Category);
            sqlCommand.Parameters.AddWithValue("@Area", animal.Area);

            await sqlConnection.OpenAsync();

            int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            await sqlConnection.CloseAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> Delete(long id)
        {
            string sql = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

            await using SqlConnection sqlConnection = new(_connString);
            await using SqlCommand sqlCommand = new(sql, sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@IdAnimal", id));

            await sqlConnection.OpenAsync();
            int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }
}
