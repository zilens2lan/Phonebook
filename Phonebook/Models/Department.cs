using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        public int DirectorId { get; set; }
    }
}
