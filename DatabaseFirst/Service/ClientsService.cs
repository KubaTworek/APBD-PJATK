using DatabaseFirst.DAL;
using DatabaseFirst.Middleware;
using Microsoft.EntityFrameworkCore;

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
            var client = await _context.Clients.FindAsync(clientId)
                ?? throw new NotFoundException($"Client with ID {clientId} does not exist.");

            var isClientTrips = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == clientId);
            if (isClientTrips)
            {
                throw new BadRequestException($"Client with ID {clientId} has active trips.");
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
