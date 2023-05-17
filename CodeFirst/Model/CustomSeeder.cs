namespace CodeFirst.Model
{
    public class CustomSeeder
    {
        private readonly MainDbContext _context;

        public CustomSeeder(MainDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if(!_context.Patients.Any()) 
                {
                    var patients = GetPatients();
                    _context.Patients.AddRange(patients);
                    _context.SaveChanges();
                }
                if (!_context.Doctors.Any())
                {
                    var doctors = GetDoctors();
                    _context.Doctors.AddRange(doctors);
                    _context.SaveChanges();
                }
                if (!_context.Medicaments.Any())
                {
                    var medicaments = GetMedicaments();
                    _context.Medicaments.AddRange(medicaments);
                    _context.SaveChanges();
                }
                if (!_context.Prescriptions.Any())
                {
                    var prescriptions = GetPrescriptions();
                    _context.Prescriptions.AddRange(prescriptions);
                    _context.SaveChanges();
                }
                if (!_context.PrescriptionMedicaments.Any())
                {
                    var prescriptionMedicaments = GetPrescriptionMedicaments();
                    _context.PrescriptionMedicaments.AddRange(prescriptionMedicaments);
                    _context.SaveChanges();
                }
            }
        }

        private List<Patient> GetPatients()
        {
            var patients = new List<Patient>
        {
            new Patient { FirstName = "John", LastName = "Doe", Birthdate = new DateTime(1990, 1, 1) },
            new Patient { FirstName = "Jane", LastName = "Smith", Birthdate = new DateTime(1985, 5, 10) },
        };

            return patients;
        }

        private List<Prescription> GetPrescriptions()
        {
            var prescriptions = new List<Prescription>
        {
            new Prescription { Date = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IdPatient = 1, IdDoctor = 1 },
            new Prescription { Date = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IdPatient = 2, IdDoctor = 2 },
        };

            return prescriptions;
        }

        private List<PrescriptionMedicament> GetPrescriptionMedicaments()
        {
            var prescriptionMedicaments = new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 4, Dose = 2, Details = "Twice a day" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 5, Dose = 1, Details = "Once a day" },
        };

            return prescriptionMedicaments;
        }

        private List<Medicament> GetMedicaments()
        {
            var medicaments = new List<Medicament>
        {
            new Medicament { Name = "Medicament 1", Description = "Description 1", Type = "Type 1" },
            new Medicament { Name = "Medicament 2", Description = "Description 2", Type = "Type 2" },
        };

            return medicaments;
        }

        private List<Doctor> GetDoctors()
        {
            var doctors = new List<Doctor>
        {
            new Doctor { FirstName = "Dr. John", LastName = "Smith", Email = "john.smith@example.com" },
            new Doctor { FirstName = "Dr. Jane", LastName = "Doe", Email = "jane.doe@example.com" },
            };

            return doctors;
        }
    }
}
