using System.ComponentModel.DataAnnotations;

namespace SMK_ServicePoint.Models
{
    public class SmartCardMembers
    {
        [Key]
        public int Id { get; set; }
        public int SmartCardId { get; set; }
        public string MemberName { get; set; }

    }
}
