using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Task2Identity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {  get; set; }

    }
}
