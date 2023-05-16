using DatabaseFirst.Model;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst.Service
{
    public interface ITripsService
    {
        Task<TripResponse> FindAllTrips();
        Task<ClientResponse> AddClientToTrip(ClientRequest clientRequest, int idTrip);
    }
}
