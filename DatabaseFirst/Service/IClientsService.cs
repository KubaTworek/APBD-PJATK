namespace DatabaseFirst.Service
{
    public interface IClientsService
    {
        Task<bool> DeleteClient(int clientId);
    }
}
