namespace DatabaseFirst.Model
{
    public class TripResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string MaxPeople { get; set; }
        public IEnumerable<object> Countries { get; set; }
        public IEnumerable<ClientResponse> Clients { get; set; }

        public TripResponse(string name, string description, string dateFrom, string dateTo, string maxPeople,
                            IEnumerable<object> countries, IEnumerable<ClientResponse> clients)
        {
            Name = name;
            Description = description;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MaxPeople = maxPeople;
            Countries = countries;
            Clients = clients;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Description: {Description}, DateFrom: {DateFrom}, DateTo: {DateTo}, " +
                   $"MaxPeople: {MaxPeople}, Countries: {string.Join(", ", Countries)}, " +
                   $"Clients: {string.Join(", ", Clients)}";
        }
    }
}
