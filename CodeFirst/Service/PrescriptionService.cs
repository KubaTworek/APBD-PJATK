using CodeFirst.DTO;
using CodeFirst.Middleware;
using CodeFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly MainDbContext _context;

        public PrescriptionService(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IList<PrescriptionResponse>> FindAllPrescriptions()
        {
            var prescriptions = await GetAllPrescriptionsWithDetails();

            var prescriptionResponses = new List<PrescriptionResponse>();
            foreach (var prescription in prescriptions)
            {
                var patientResponse = MapPatientToResponse(prescription.Patient);
                var doctorResponse = MapDoctorToResponse(prescription.Doctor);
                var medicaments = await GetMedicamentsForPrescription(prescription.IdPrescription);

                var prescriptionResponse = new PrescriptionResponse
                {
                    IdPrescription = prescription.IdPrescription,
                    Date = prescription.Date,
                    DueDate = prescription.DueDate,
                    Patient = patientResponse,
                    Doctor = doctorResponse,
                    Medicaments = MapMedicamentsToResponse(medicaments)
                };

                prescriptionResponses.Add(prescriptionResponse);
            }

            return prescriptionResponses;
        }

        public async Task<PrescriptionResponse> FindPrescriptionById(int idPrescription)
        {
            var prescription = await GetPrescriptionById(idPrescription);
            var patientResponse = MapPatientToResponse(prescription.Patient);
            var doctorResponse = MapDoctorToResponse(prescription.Doctor);
            var medicaments = await GetMedicamentsForPrescription(idPrescription);

            var prescriptionResponse = new PrescriptionResponse
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                Patient = patientResponse,
                Doctor = doctorResponse,
                Medicaments = MapMedicamentsToResponse(medicaments)
            };

            return prescriptionResponse;
        }

        private async Task<List<Prescription>> GetAllPrescriptionsWithDetails()
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .ToListAsync();
        }

        private async Task<Prescription> GetPrescriptionById(int idPrescription)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(p => p.IdPrescription == idPrescription)
                ?? throw new NotFoundException($"Prescription with ID {idPrescription} does not exist.");

            return prescription;
        }

        private PatientResponse MapPatientToResponse(Patient patient)
        {
            return new PatientResponse
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate
            };
        }

        private DoctorResponse MapDoctorToResponse(Doctor doctor)
        {
            return new DoctorResponse
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
        }

        private async Task<List<Medicament>> GetMedicamentsForPrescription(int idPrescription)
        {
            var medicamentIds = await _context.PrescriptionMedicaments
                .Where(pm => pm.IdPrescription == idPrescription)
                .Select(pm => pm.IdMedicament)
                .ToListAsync();

            var medicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .ToListAsync();

            return medicaments;
        }

        private List<MedicamentResponse> MapMedicamentsToResponse(List<Medicament> medicaments)
        {
            return medicaments.Select(m => new MedicamentResponse
            {
                IdMedicament = m.IdMedicament,
                Name = m.Name,
                Description = m.Description,
                Type = m.Type
            }).ToList();
        }
    }
}
