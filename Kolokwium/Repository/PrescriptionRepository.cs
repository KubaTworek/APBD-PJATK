using Kolokwium.Model;

namespace Kolokwium.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private const string _connString = "Data Source=db-mssql;Initial Catalog=s25646;Integrated Security=True";

        public Task AddMedicaments(MedicamentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Prescription>> GetAllByLastName(string lastName)
        {
            throw new NotImplementedException();
        }
    }
}
