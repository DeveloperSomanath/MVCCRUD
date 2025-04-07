using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class EmployeeModel
    {
        [Key]
        public int PersonId { get; set; }
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gender Is Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "City Is Required")]
        public string City { get; set; }
        public string? Images { get; set; }  

    }
}
