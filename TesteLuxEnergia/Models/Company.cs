using TesteLuxEnergia.Models.Enums;

namespace TesteLuxEnergia.Models
{
    public class Company
    {
        public string Id { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Distribuitor { get; set; }
        public TaxModality TaxModality { get; set; }
        public double PeakConsumption { get; set; }
        public double OutOfPeakConsumption { get; set; }
        public double AverageBillRate { get; set; }

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