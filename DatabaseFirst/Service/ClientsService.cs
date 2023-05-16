using DatabaseFirst.DAL;
using DatabaseFirst.Middleware;

namespace DatabaseFirst.Service
{
    public class ClientsService : IClientsService
    {
        private readonly S25646Context _context;

        public ClientsService(S25646Context context)
        {
            _context = context;
        }

        public async Task<bool> DeleteClient(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                throw new NotFoundException($"Client with ID {clientId} does not exist.");
            }

            var clientTrips = _context.ClientTrips.Where(ct => ct.IdClient == clientId);
            _context.ClientTrips.RemoveRange(clientTrips);
            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
