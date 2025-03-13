using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Full Name")]
        [StringLength(30)]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }

        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Mobile Number")]
        [StringLength(10,MinimumLength =10)]
        public string MobileNumber { get; set; }

    }
}
