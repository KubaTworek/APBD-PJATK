using Kolokwium.Model;

namespace Kolokwium.Service
{
    public interface IPrescriptionService
    {
        Task<int> AddMedicaments(MedicamentsRequest request);
        Task<IList<Prescription>> GetAllByLastName(string lastName);
    }
}
