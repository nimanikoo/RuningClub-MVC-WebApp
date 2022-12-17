using System.ComponentModel.DataAnnotations;

namespace RuningClub_WebApp.Dtos
{
    public class UserRegisterDto
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirmed Password")]
        [Required(ErrorMessage ="Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Dosen't match")]
        public string ConfirmPassword { get; set; }
    }
}
