using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.Models
{
    public class SmartCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Family Head is Required")]
        [Display(Name = "Family Head")]
        public string FamilyHead { get; set; }

        [Required(ErrorMessage = "Father Or Husband is Required")]
        [Display(Name = "Father Or Husband")]
        public string FatherOrHusband { get; set; }

        [Required(ErrorMessage = "Date Of Birth is Required")]
        [Display(Name = "Date of Bith")]
        public DateOnly DateofBirth { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Card Type is Required")]
        [Display(Name = "Card Type")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "Card Number is Required")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Shop Number is Required")]
        [Display(Name = "Shop Number")]
        public string ShopNumber { get; set; }

        [Required(ErrorMessage = "Year is Required")]
        [Display(Name = "Year")]
        public int year { get; set; }

        [Display(Name = " QR Image Url")]
        public string QRImageUrl { get; set; }

        [Display(Name = "Photo Image")]
        public string PhotoImageUrl { get; set; }
    }
}
