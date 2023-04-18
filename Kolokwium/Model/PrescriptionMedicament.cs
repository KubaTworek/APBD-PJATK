namespace Kolokwium.Model
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }

        public PrescriptionMedicament(int idMedicament, int idPrescription, int dose, string details)
        {
            IdMedicament = idMedicament;
            IdPrescription = idPrescription;
            Dose = dose;
            Details = details;
        }
    }
}
