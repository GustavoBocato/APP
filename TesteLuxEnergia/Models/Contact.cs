using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteLuxEnergia.Models
{
    public class Contact
    {
        [Required]
        public string Identifier {  get; set; }
        [Required]
        public string Name { get; set; }
        public string Role { get; set; }
        public string CompanyName { get; set; }
        public string Telephone { get; set; }
        [Key]
        [EmailAddress]
        public string Email { get; set; }
    }
}
