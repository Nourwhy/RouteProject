using System.ComponentModel.DataAnnotations;

namespace RouteProject.PL.Dtos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required ! !")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
