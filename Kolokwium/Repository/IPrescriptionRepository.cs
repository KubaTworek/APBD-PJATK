using Kolokwium.Model;

namespace Kolokwium.Repository
{
    public interface IPrescriptionRepository
    {
        Task AddMedicaments(MedicamentsRequest request);
        Task<IList<Prescription>> GetAllByLastName(string lastName);
    }
}
