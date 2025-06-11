using TesteLuxEnergia.Models;

namespace TesteLuxEnergia.Repositories
{
    public interface IRepository
    {
        public IEnumerable<Contact> GetContactsByCompany(string companyName);
    }
}