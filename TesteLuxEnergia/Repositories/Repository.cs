using TesteLuxEnergia.Data;
using TesteLuxEnergia.Models;

namespace TesteLuxEnergia.Repositories
{
    public class Repository
    {
        private readonly LuxTestDbContext _dbContext;
        public Repository(LuxTestDbContext luxTestDbContext) 
        {
            _dbContext = luxTestDbContext;
        }

        public IEnumerable<Contact> GetContactsByCompany(string companyName) 
        {
            return _dbContext.Contacts.AsQueryable()
                .Where(c => c.Company.Name == companyName)
                .ToList();
        }
    }
}
