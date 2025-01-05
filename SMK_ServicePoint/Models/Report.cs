using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMK_ServicePoint.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
  
        [Required(ErrorMessage = "Customer Name is Required")]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; } 

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
      

        [Required(ErrorMessage = "Starting date is Required")]
        [Display(Name = "Starting Date")]
        public DateTime Startdate { get; set; }
       
        [Required(ErrorMessage = "Ending date is Required")]
        [Display(Name = "Ending Date")]
        public DateTime Enddate { get; set; }

        [Required(ErrorMessage = "Service Type is Required")]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
    }
}
