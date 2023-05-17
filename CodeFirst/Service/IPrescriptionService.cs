using CodeFirst.DTO;

namespace CodeFirst.Service
{
    public interface IPrescriptionService
    {
        Task<IList<PrescriptionResponse>> FindAllPrescriptions();
        Task<PrescriptionResponse> FindPrescriptionById(int idPrescription);
    }
}
