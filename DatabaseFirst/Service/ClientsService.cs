using DatabaseFirst.DAL;

namespace DatabaseFirst.Service
{
    public class ClientsService : IClientsService
    {
        private readonly S25646Context _context;

        public ClientsService(S25646Context context)
        {
            _context = context;
        }

        public Task<bool> DeleteClient(string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
