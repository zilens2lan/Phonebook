using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Director
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        public List<DepartmentHead> DepartmentsHeads { get; set; }
    }
}
