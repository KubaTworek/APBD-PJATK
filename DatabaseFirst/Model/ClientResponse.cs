namespace DatabaseFirst.Model
{
    public class ClientResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ClientResponse(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"First Name: {FirstName}, Last Name: {LastName}";
        }
    }
}
