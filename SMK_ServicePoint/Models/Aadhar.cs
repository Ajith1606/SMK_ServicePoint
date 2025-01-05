using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.Models
{
    public class Aadhar
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "பெயர்")]
        public string TamilName { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date Of Birth is Required")]
        [Display(Name = "Date of Bith")]
        public DateOnly DateofBirth { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "முகவரி")]
        public string TamilAddress { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [MaxLength(4)]
        [Required(ErrorMessage = "Aadhar no1 is Required")]
        [Display(Name = "Aadhar no1")]
        public string Aadharno1 { get; set; }

        [MaxLength(4)]
        [Required(ErrorMessage = "Aadhar no2 is Required")]
        [Display(Name = "Aadhar no2")]
        public string Aadharno2 { get; set; }

        [MaxLength(4)]
        [Required(ErrorMessage = "Aadharno3 is Required")]
        [Display(Name = "Aadhar no3")]
        public string Aadharno3 { get; set; }
        public string Aadharno 
        {
            get
            {        
                return string.Join(" ", new[] { Aadharno1, Aadharno2, Aadharno3 }.Where(Aadhar => !string.IsNullOrWhiteSpace(Aadhar)));
            }
        }

        [Required(ErrorMessage = "Aadhar noIssued Date is Required")]
        [Display(Name = "Aadhar no Issued Date")]
        public DateOnly AadharnoIssuedDate { get; set; }

        [Display(Name = "Photo Image")]
        public string PhotoImageUrl { get; set; }

        [Display(Name = "Qr Image Url")]
        public string QRImageUrl { get; set; }
      
    }
}
