using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HPRSApp.Models
{
    public class PatientViewModel
    {
        [Key]
        [Required]
        public int PatientId { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z ]+$",ErrorMessage ="Patient name can only contain spaces and letters.")]
        [DisplayName("Patient Name")]
        public string Name { get; set; }
        [Required]
        [Range(1,120,ErrorMessage ="Patient age must be between valid range.")]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [RegularExpression("^[A-za-z]+$", ErrorMessage = "Patient Diagnosis Name can only contain letters.")]
        public string Diagnosis { get; set; }
        [Required]
        [DisplayName("Addmission Date")]
        public DateTime AddmissionDate { get; set; }
        [Required]
        [DisplayName("Discharged")]
        public bool isDischarged { get; set; } = false;
    }
}
