namespace Kolokwium.Model
{
    public class MedicamentsRequest
    {
        public List<MedicamentRequest> Medicaments { get; set; }

        public MedicamentsRequest(List<MedicamentRequest> medicaments)
        {
            Medicaments = medicaments;
        }
    }
}
