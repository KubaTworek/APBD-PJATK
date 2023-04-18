namespace Kolokwium.Model
{
    public class Prescription
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        public Prescription(int idPrescription, DateTime date, DateTime dueDate, int idPatient, int idDoctor)
        {
            IdPrescription = idPrescription;
            Date = date;
            DueDate = dueDate;
            IdPatient = idPatient;
            IdDoctor = idDoctor;
        }

        public override string ToString()
        {
            return $"Prescription ID: {IdPrescription}, Date: {Date}, Due Date: {DueDate}, Patient ID: {IdPatient}, Doctor ID: {IdDoctor}";
        }
    }
}
