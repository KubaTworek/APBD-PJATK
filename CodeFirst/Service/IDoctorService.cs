using CodeFirst.DTO;

namespace CodeFirst.Service
{
    public interface IDoctorService
    {
        Task<IList<DoctorResponse>> FindAllDoctors();
        Task<DoctorResponse> FindDoctorById(int idDoctor);
        Task<DoctorResponse> CreateDoctor(DoctorRequest request);
        Task<DoctorResponse> UpdateDoctorById(DoctorRequest request, int idDoctor);
        Task<int> DeleteDoctorById(int idDoctor);
    }
}
