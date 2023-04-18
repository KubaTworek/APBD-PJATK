using Kolokwium.Model;
using Kolokwium.Repository;

namespace Kolokwium.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<int> AddMedicaments(MedicamentsRequest request, int prescriptionId)
        {
            var medicaments = new List<MedicamentRequest>();

            foreach (MedicamentRequest medicament in request.Medicaments)
            {
                Medicament medicamentTemp = await _prescriptionRepository.FindMedicamentById(medicament.IdMedicament) ?? throw new Exception();
                PrescriptionMedicament medicamentInPrescription = await _prescriptionRepository.FindMedicamentInPrescription(medicament.IdMedicament, prescriptionId);
                if (medicamentInPrescription != null)
                {
                    throw new Exception();
                }
                medicaments.Add(medicament);
            }

            await _prescriptionRepository.AddMedicaments(medicaments, prescriptionId);
            return medicaments.Count;
        }

        public async Task<IList<Prescription>> GetAllByLastName(string lastName)
        {
            return await _prescriptionRepository.GetAllByLastName(lastName);
        }
    }
}
