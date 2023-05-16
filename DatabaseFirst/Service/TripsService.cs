using DatabaseFirst.DAL;
using DatabaseFirst.Middleware;
using DatabaseFirst.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq;

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
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null)
            {
                throw new NotFoundException($"Trip with ID {idTrip} does not exist.");
            }

            var client = new Client
            {
                FirstName = clientRequest.FirstName,
                LastName = clientRequest.LastName,
                Email = clientRequest.Email,
                Telephone = clientRequest.Telephone,
                Pesel = clientRequest.Pesel
            };

            var clientTrip = new ClientTrip
            {
                IdTrip = trip.IdTrip,
                IdClient = client.IdClient,
                RegisteredAt = DateTime.Now,
                PaymentDate = DateTime.Parse(clientRequest.PaymentDate)
            };


            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == clientRequest.Pesel);
            if (existingClient == null)
            {
                _context.Clients.Add(client);
            }
            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();

            var clientResponse = new ClientResponse
            (
                client.FirstName,
                client.LastName
            );

            return clientResponse;
        }

        public async Task<IList<TripResponse>> FindAllTrips()
        {
            var trips = await _context.Trips
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
    }
}
