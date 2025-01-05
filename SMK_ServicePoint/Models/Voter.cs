using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.Models
{
    public class Voter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EPIC Number is Required")]
        [MaxLength(13)]
        [Display(Name = "Epic Number")]
        public string EpicNo { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "பெயர்")]
        public string TamilName { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "தந்தை பெயர்")]
        public string TamilFatherNameorHusbandName { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Father Name")]
        public string FatherNameorHusbandName { get; set; }

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

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = " வாக்காளர் பதிவு அலுவலர்")]     
        public string TamilElectoralRegistrationOfficier { get; set; }

        [Required(ErrorMessage = "Electoral Registration Officier is Required")]
        [Display(Name = " Electoral Registration Officier")]
        public string ElectoralRegistrationOfficier { get; set; }

        [Required(ErrorMessage = "Download Date is Required")]
        [Display(Name = " Download Date")]
        public DateOnly DownloadDate { get; set; }

        [Display(Name = "QR Image")]
        public string QRImageUrl { get; set; }

        [Display(Name = "Photo Image")]
        public string PhotoImageUrl { get; set; }

        [Display(Name = "Sign Image")]
        public string SignImageUrl { get; set; }

    }
}
