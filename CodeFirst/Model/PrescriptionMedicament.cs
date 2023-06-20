using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Model
{
    public class PrescriptionMedicament
    {
        [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }

        [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }

        [Required]
        public int Dose { get; set; }

        [Required]
        [MaxLength(100)]
        public string Details { get; set; }

        public virtual Medicament Medicament { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
