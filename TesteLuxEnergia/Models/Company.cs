using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class Company
    {
        [Key]
        public string Id { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int CEP { get; set; }
        public string Name { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public string Distribuitor { get; set; }
        public string TaxModality { get; set; }
        public string PeakConsumption { get; set; }
        public string OutOfPeakConsumption { get; set; }
        public string AverageBillRate { get; set; }
        public string Manager { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Company outro)
                return false;

            return Id == outro.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}