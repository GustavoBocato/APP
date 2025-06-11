using TesteLuxEnergia.Models;
using TesteLuxEnergia.Repositories;

namespace TesteLuxEnergia.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Contact> GetContactsByCompany(string companyName) 
        {
            return _repository.GetContactsByCompany(companyName);
        }
    }
}
