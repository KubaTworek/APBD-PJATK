using System.ComponentModel.DataAnnotations;

namespace DatabaseFirst.Model
{
    public class ClientRequest
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "PESEL is required.")]
        [StringLength(11, ErrorMessage = "PESEL must be 11 characters long.", MinimumLength = 11)]
        public string Pesel { get; set; }

        public string? PaymentDate { get; set; }


        public ClientRequest(string firstName, string lastName, string email, string telephone, string pesel, string paymentDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Telephone = telephone;
            Pesel = pesel;
            PaymentDate = paymentDate;
        }

        public override string ToString()
        {
            return $"First Name: {FirstName}, Last Name: {LastName}, Email: {Email}, Telephone: {Telephone}, " +
                   $"PESEL: {Pesel}, Payment Date: {PaymentDate}";
        }
    }
}
