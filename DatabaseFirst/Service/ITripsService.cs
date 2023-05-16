using DatabaseFirst.Model;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst.Service
{
    public interface ITripsService
    {
        Task<IList<TripResponse>> FindAllTrips();
        Task<ClientResponse> AddClientToTrip(ClientRequest clientRequest, int idTrip);
    }
}
