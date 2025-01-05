using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMK_ServicePoint.Models
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is Required")]
        [Display(Name ="Customer Name")]
        public int CustomerId { get; set; } 

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
      
        [Required(ErrorMessage = "Service is Required")]
        public string Service { get; set; }
   
        public double Amount { get; set; }

        [Required(ErrorMessage = "Tax is Required")]
        public double Tax { get; set; }
        public double Total {  get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is Required")]
        [Display(Name = "Unit Price")]
        public int UnitPrice { get; set; }
    }
}
