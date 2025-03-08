using System.ComponentModel.DataAnnotations;

namespace RouteProject.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Code is Requied !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Requied !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CreateAt is Requied !")]
        public DateTime CreateAt { get; set; }
    }
}
