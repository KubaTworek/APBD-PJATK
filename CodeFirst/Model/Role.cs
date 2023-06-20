using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Model
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
