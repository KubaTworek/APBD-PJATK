using Kolokwium.Model;
using System.Data.SqlClient;
using System.Transactions;

namespace Kolokwium.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private const string _connString = "Data Source=db-mssql;Initial Catalog=s25646;Integrated Security=True";

        public async Task AddMedicaments(List<MedicamentRequest> request, int prescriptionId)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in request)
                            {
                                using SqlCommand command = new SqlCommand("INSERT INTO Prescription_Medicament (IdMedicament, IdPrescription, Dose, Details) VALUES (@IdMedicament, @IdPrescription, @Dose, @Details)", connection, transaction);

                                command.Parameters.AddWithValue("@IdMedicament", item.IdMedicament);
                                command.Parameters.AddWithValue("@IdPrescription", prescriptionId);
                                command.Parameters.AddWithValue("@Dose", item.Dose);
                                command.Parameters.AddWithValue("@Details", item.Details);
                                await command.ExecuteNonQueryAsync();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<Medicament> FindMedicamentById(int idMedicament)
        {

            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;

            string sql = "SELECT * FROM [dbo].[Medicament] WHERE IdMedicament = @IdMedicament";

            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.AddWithValue("@IdMedicament", idMedicament);

            await sqlConnection.OpenAsync();

            using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            Medicament medicament = null;
            while (await sqlDataReader.ReadAsync())
            {
                medicament = new(
                    int.Parse(sqlDataReader["IdMedicament"].ToString()),
                    sqlDataReader["Name"].ToString(),
                    sqlDataReader["Description"].ToString(),
                    sqlDataReader["Type"].ToString()
                );
            }

            await sqlConnection.CloseAsync();

            return medicament;
        }

        public async Task<PrescriptionMedicament> FindMedicamentInPrescription(int idMedicament, int prescriptionId)
        {
            using SqlConnection sqlConnection = new(_connString);
            using SqlCommand sqlCommand = new();
            sqlCommand.Connection = sqlConnection;

            string sql = "SELECT * FROM [dbo].[Prescription_Medicament] WHERE IdMedicament = @IdMedicament AND IdPrescription = @IdPrescripton";

            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.AddWithValue("@IdMedicament", idMedicament);
            sqlCommand.Parameters.AddWithValue("@IdPrescripton", prescriptionId);

            await sqlConnection.OpenAsync();

            using SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            PrescriptionMedicament prescription_medicament = null;
            while (await sqlDataReader.ReadAsync())
            {
                prescription_medicament = new(
                    int.Parse(sqlDataReader["IdMedicament"].ToString()),
                    int.Parse(sqlDataReader["IdPrescription"].ToString()),
                    int.Parse(sqlDataReader["Dose"].ToString()),
                    sqlDataReader["Details"].ToString()
                );
            }

            await sqlConnection.CloseAsync();

            return prescription_medicament;
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
