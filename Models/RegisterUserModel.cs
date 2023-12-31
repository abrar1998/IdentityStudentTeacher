using System.ComponentModel.DataAnnotations;

namespace Task2Identity.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public int Common_Id {  get; set; }
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string SelectedRole { get; set; }
    }
}
