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

        public async Task<int> AddMedicaments(MedicamentsRequest request)
        {
            //for each
            //jesi lek nie istnieje wyrzucic exception
            //jesli lek jest dodany do recepty pomin
            //dodaj do listy


            await _prescriptionRepository.AddMedicaments(request);
            return 1;
        }

        public async Task<IList<Prescription>> GetAllByLastName(string lastName)
        {
            return await _prescriptionRepository.GetAllByLastName(lastName);
        }
    }
}
