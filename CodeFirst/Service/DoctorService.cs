using CodeFirst.DTO;
using CodeFirst.Middleware;
using CodeFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly MainDbContext _context;

        public DoctorService(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IList<DoctorResponse>> FindAllDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            var doctorResponses = doctors.Select(d => new DoctorResponse
            {
                IdDoctor = d.IdDoctor,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email
            }).ToList();

            return doctorResponses;
        }

        public async Task<DoctorResponse> FindDoctorById(int idDoctor)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == idDoctor)
                ?? throw new NotFoundException($"Doctor with ID {idDoctor} does not exist.");

            var doctorResponse = new DoctorResponse
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };

            return doctorResponse;
        }

        public async Task<DoctorResponse> CreateDoctor(DoctorRequest request)
        {
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            var doctorResponse = new DoctorResponse
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };

            return doctorResponse;
        }

        public async Task<DoctorResponse> UpdateDoctorById(DoctorRequest request, int idDoctor)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == idDoctor)
                ?? throw new NotFoundException($"Doctor with ID {idDoctor} does not exist.");

            existingDoctor.FirstName = request.FirstName;
            existingDoctor.LastName = request.LastName;
            existingDoctor.Email = request.Email;

            await _context.SaveChangesAsync();

            var doctorResponse = new DoctorResponse
            {
                IdDoctor = existingDoctor.IdDoctor,
                FirstName = existingDoctor.FirstName,
                LastName = existingDoctor.LastName,
                Email = existingDoctor.Email
            };

            return doctorResponse;
        }

        public async Task<int> DeleteDoctorById(int idDoctor)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == idDoctor)
                ?? throw new NotFoundException($"Doctor with ID {idDoctor} does not exist.");

            _context.Doctors.Remove(doctor);
            int deletedCount = await _context.SaveChangesAsync();

            return deletedCount;
        }
    }
}
