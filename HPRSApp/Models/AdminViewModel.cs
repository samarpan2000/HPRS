using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HPRSApp.Models
{
    public class AdminViewModel
    {
        [Key]
        [Required]
        public int AdminId { get; set; }
        [Required]
        [DisplayName("Name")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Name can only contain spaces and letters.")]
        public string AdminName { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
