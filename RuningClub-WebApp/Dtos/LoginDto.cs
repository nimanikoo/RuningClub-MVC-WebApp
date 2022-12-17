using System.ComponentModel.DataAnnotations;

namespace RuningClub_WebApp.Dtos
{
    public class LoginDto
    {
        [Display(Name ="Email Address")]
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
