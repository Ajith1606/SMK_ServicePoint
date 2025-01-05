using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Customer Name is Required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone]
        [Display(Name ="Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
       
    }
}
