using DatabaseFirst.DAL;
using DatabaseFirst.Middleware;
using DatabaseFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.Service
{
    public class TripsService : ITripsService
    {
        private readonly S25646Context _context;

        public TripsService(S25646Context context)
        {
            _context = context;
        }

        public async Task<ClientResponse> AddClientToTrip(ClientRequest clientRequest, int idTrip)
        {
            var trip = await GetExistingTrip(idTrip);
            var existingClient = await FindExistingClient(clientRequest.Pesel);

            if (existingClient != null)
            {
                await CheckClientAssignedToTrip(existingClient.IdClient, idTrip);
            }
            else
            {
                existingClient = await CreateNewClient(clientRequest);
            }

            await AssignClientToTrip(trip.IdTrip, existingClient.IdClient, clientRequest.PaymentDate);

            var clientResponse = CreateClientResponse(existingClient);

            return clientResponse;
        }

        public async Task<IList<TripResponse>> FindAllTrips()
        {
            var trips = await _context.Trips
                .Include(t => t.Countries)
                .GroupJoin(_context.ClientTrips, trip => trip.IdTrip, clientTrip => clientTrip.IdTrip, (trip, clientTrips) => new { Trip = trip, ClientTrips = clientTrips })
                .SelectMany(x => x.ClientTrips.DefaultIfEmpty(), (x, client) => new { x.Trip, Client = client.ClientNavigation })
                .Select(x => new
                {
                    Trip = x.Trip,
                    Client = x.Client,
                    CountryNames = x.Trip.Countries.Select(ct => ct.Name)
                })
                .ToListAsync();

            var tripResponses = trips.GroupBy(r => r.Trip)
                .OrderByDescending(group => group.Key.DateFrom)
                .Select(group => new TripResponse
                (
                    group.Key.Name,
                    group.Key.Description,
                    group.Key.DateFrom.ToString(),
                    group.Key.DateTo.ToString(),
                    group.Key.MaxPeople.ToString(),
                    group.SelectMany(r => r.CountryNames).Distinct(),
                    group.Where(r => r.Client != null)
                        .Select(r => new ClientResponse
                        (
                            r.Client.FirstName,
                            r.Client.LastName
                        ))
                )).ToList();

            return tripResponses;
        }

        private IList<TripResponse> MapTripResponses(List<(Trip Trip, Client? Client, IEnumerable<string> CountryNames)> trips)
        {
            return trips.GroupBy(r => r.Trip)
                .OrderByDescending(group => group.Key.DateFrom)
                .Select(group => new TripResponse
                (
                    group.Key.Name,
                    group.Key.Description,
                    group.Key.DateFrom.ToString(),
                    group.Key.DateTo.ToString(),
                    group.Key.MaxPeople.ToString(),
                    group.SelectMany(r => r.CountryNames).Distinct(),
                    group.Where(r => r.Client != null)
                        .Select(r => new ClientResponse
                        (
                            r.Client.FirstName,
                            r.Client.LastName
                        ))
                )).ToList();
        }

        private async Task<Trip> GetExistingTrip(int idTrip)
        {
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                throw new NotFoundException($"Trip with ID {idTrip} does not exist.");
            }

            return trip;
        }

        private async Task<Client> FindExistingClient(string pesel)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);
        }

        private async Task CheckClientAssignedToTrip(int clientId, int tripId)
        {
            var isClientAssigned = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == clientId && ct.IdTrip == tripId);
            if (isClientAssigned)
            {
                throw new BadRequestException($"Client with ID {clientId} has already been assigned to that trip.");
            }
        }

        private async Task<Client> CreateNewClient(ClientRequest clientRequest)
        {
            var client = new Client
            {
                IdClient = await _context.Clients.MaxAsync(c => c.IdClient) + 1,
                FirstName = clientRequest.FirstName,
                LastName = clientRequest.LastName,
                Email = clientRequest.Email,
                Telephone = clientRequest.Telephone,
                Pesel = clientRequest.Pesel
            };

            var clientCreated = _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return clientCreated.Entity;
        }

        private async Task AssignClientToTrip(int tripId, int clientId, string paymentDate)
        {
            var clientTrip = new ClientTrip
            {
                IdTrip = tripId,
                IdClient = clientId,
                RegisteredAt = DateTime.Now,
                PaymentDate = DateTime.Parse(paymentDate)
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();
        }

        private ClientResponse CreateClientResponse(Client client)
        {
            return new ClientResponse
            (
                client.FirstName,
                client.LastName
            );
        }
    }
}
