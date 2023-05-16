namespace DatabaseFirst.Model
{
    public class ClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
        public int IdTrip { get; set; }
        public string TripName { get; set; }
        public string PaymentDate { get; set; }

        public ClientRequest(string firstName, string lastName, string email, string telephone, string pesel,
                             int idTrip, string tripName, string paymentDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Telephone = telephone;
            Pesel = pesel;
            IdTrip = idTrip;
            TripName = tripName;
            PaymentDate = paymentDate;
        }

        public override string ToString()
        {
            return $"First Name: {FirstName}, Last Name: {LastName}, Email: {Email}, Telephone: {Telephone}, " +
                   $"PESEL: {Pesel}, ID Trip: {IdTrip}, Trip Name: {TripName}, Payment Date: {PaymentDate}";
        }
    }
}
