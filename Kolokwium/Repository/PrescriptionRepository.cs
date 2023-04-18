using Kolokwium.Model;
using System.Data.SqlClient;

namespace Kolokwium.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private const string _connString = "Data Source=db-mssql;Initial Catalog=s25646;Integrated Security=True";

        public Task AddMedicaments(MedicamentsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Prescription>> GetAllByLastName(string lastName)
        {
            List<Prescription> prescriptons = new();

            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;

            string sql = "SELECT * FROM [dbo].[Prescription] ORDER BY Date DESC";

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                sql = "SELECT p.IdPrescription, p.Date, p.DueDate, p.IdPatient, p.IdDoctor FROM Prescription p INNER JOIN Patient pt ON p.IdPatient = pt.IdPatient WHERE pt.LastName = @LastName ORDER BY p.Date;";
            }

            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.AddWithValue("@LastName", lastName);

            await sqlConnection.OpenAsync();

            using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (await sqlDataReader.ReadAsync())
            {
                Prescription prescription = new(
                    int.Parse(sqlDataReader["IdPrescription"].ToString()),
                    DateTime.Parse(sqlDataReader["Date"].ToString()),
                    DateTime.Parse(sqlDataReader["DueDate"].ToString()),
                    int.Parse(sqlDataReader["IdPatient"].ToString()),
                    int.Parse(sqlDataReader["IdDoctor"].ToString())
                );

                prescriptons.Add(prescription);
            }

            await sqlConnection.CloseAsync();

            return prescriptons;
        }
    }
}
