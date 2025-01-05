using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
