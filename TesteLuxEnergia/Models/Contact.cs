namespace TesteLuxEnergia.Models
{
    public class Contact
    {
        public string Identifier {  get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public Company Company { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
