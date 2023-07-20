using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class DepartmentHead
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        public Director Director { get; set; }
    }
}
