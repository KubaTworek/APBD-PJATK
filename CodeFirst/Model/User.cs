using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Model
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        [ForeignKey("Role")]
        public int IdRole { get; set; }
        public virtual Role Role { get; set; }
    }
}
