using System.ComponentModel.DataAnnotations;

namespace RouteProject.PL.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Code is Eequired !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Eequired !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt is Eequired !")]
        public DateTime CreateAt { get; set; }
    }
}
