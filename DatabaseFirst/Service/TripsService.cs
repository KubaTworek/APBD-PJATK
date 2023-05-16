using DatabaseFirst.DAL;
using DatabaseFirst.Model;

namespace DatabaseFirst.Service
{
    public class TripsService : ITripsService
    {
        private readonly S25646Context _context;

        public TripsService(S25646Context context)
        {
            _context = context;
        }

        public Task<ClientResponse> AddClientToTrip(ClientRequest clientRequest, int idTrip)
        {
            throw new NotImplementedException();
        }

        public Task<TripResponse> FindAllTrips()
        {
            throw new NotImplementedException();
        }
    }
}
