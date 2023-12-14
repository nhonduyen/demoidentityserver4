using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace DemoIdentityServer.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int HashMethod { get; set; }
        public string HMASCSHA1_PassWord { get; set; }
        public DateTime? HMASCSHA1_Date { get; set; }

        public string HMASCSHA1_Portal_PassWord { get; set; }
        public DateTime? HMASCSHA1_Portal_Date { get; set; }

        public string HMASCSHA1_Mobile_PassWord { get; set; }
        public DateTime? HMASCSHA1_Mobile_Date { get; set; }
        public DateTime? EmailConfirmDate { get; set; }
        public DateTime? PhoneConfirmDate { get; set; }
        public bool IsActive { get; set; }
    }
}
