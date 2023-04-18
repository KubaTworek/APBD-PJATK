using Kolokwium.Model;

namespace Kolokwium.Service
{
    public interface IPrescriptionService
    {
        Task<int> AddMedicaments(MedicamentsRequest request, int prescriptionId);
        Task<IList<Prescription>> GetAllByLastName(string lastName);
    }
}
