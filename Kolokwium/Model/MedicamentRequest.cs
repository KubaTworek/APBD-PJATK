namespace Kolokwium.Model
{
    public class MedicamentRequest
    {
        public int IdMedicament { get; set; }
        public string Dose { get; set; }
        public string Details { get; set; }

        public MedicamentRequest(int idMedicament, string dose, string details)
        {
            IdMedicament = idMedicament;
            Dose = dose;
            Details = details;
        }

        public override string ToString()
        {
            return $"MedicamentRequest ID: {IdMedicament}, Dose: {Dose}, Details: {Details}";
        }
    }
}
