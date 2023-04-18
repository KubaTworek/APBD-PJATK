using Kolokwium.Model;

namespace Kolokwium.Repository
{
    public interface IPrescriptionRepository
    {
        Task AddMedicaments(List<MedicamentRequest> request, int prescriptionId);
        Task<Medicament> FindMedicamentById(int idMedicament);
        Task<PrescriptionMedicament> FindMedicamentInPrescription(int idMedicament, int prescriptionId);
        Task<IList<Prescription>> GetAllByLastName(string lastName);
    }
}
