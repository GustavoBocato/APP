using TesteLuxEnergia.Models;

namespace TesteLuxEnergia.Services
{
    public interface IService
    {
        public IEnumerable<Contact> GetContactsByCompany(string companyName);
    }
}
