using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class DepartmentHead
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        public List<Worker> Workers { get; set; }
        public Director Director { get; set; }
    }
}
