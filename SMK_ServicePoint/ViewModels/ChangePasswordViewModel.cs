using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {1} and at max {1} character long")]
        [DataType(DataType.Password)]
        [Compare("NewConfirmPassword", ErrorMessage = "Password must be same")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string NewConfirmPassword { get; set; }
    }
}
