using Microsoft.AspNetCore.Identity;

namespace SMK_ServicePoint.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
