using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {1} and at max {2} character long")]
        [Compare("ConfirmPassword", ErrorMessage = "Password must be same")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
