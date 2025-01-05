using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.Models
{
    public class PanCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pan Number is Required")]
        [MaxLength(10)]
        [Display(Name = "Pan Number")]
        public string PanNo { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Father Name is Required")]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Date Of Birth is Required")]
        [Display(Name = "Date of Birth")]
        public DateOnly DateofBirth { get; set; }

        [Display(Name = "QR Image Url")]
        public string QRImageUrl { get; set; }

        [Display(Name = "Photo Image")]
        public string PhotoImageUrl { get; set; }

        [Display(Name = "Sign Image")]
        public string SignImageUrl { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        [Display(Name = "Select Type NSDL or UTI")]
        public string PanNSDLUTI { get; set; }

    }
}
