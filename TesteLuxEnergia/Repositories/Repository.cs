using TesteLuxEnergia.Data;
using TesteLuxEnergia.Models;

namespace TesteLuxEnergia.Repositories
{
    public class Repository : IRepository
    {
        private readonly string client = "Cliente";
        private readonly LuxTestDbContext _dbContext;
        public Repository(LuxTestDbContext luxTestDbContext) 
        {
            _dbContext = luxTestDbContext;
        }

        public IEnumerable<Contact> GetAllContacts() 
        {
            return _dbContext.Contacts.ToList();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _dbContext.Companies.ToList();
        }

        public IEnumerable<Contact> GetContactsByCompany(string companyName) 
        {
            return _dbContext.Contacts.AsQueryable()
                .Where(c => c.CompanyName == companyName)
                .ToList();
        }

        public IEnumerable<Contact> GetAllClients() 
        {
            return _dbContext.Contacts.AsQueryable()
                .Where(c => c.Identifier == client)
                .ToList();
        }

        public int GetContactsCount() 
        {
            return _dbContext.Contacts.AsQueryable().Count();
        }

        public int GetCompaniesCount()
        {
            return _dbContext.Companies.AsQueryable().Count();
        }

        public Contact? GetContactByEmail(string email) 
        {
            return _dbContext.Contacts.AsQueryable()
                .Where(c => c.Email == email)
                .FirstOrDefault();
        }

        public Company? GetCompanyByCNPJ(string CNPJ) 
        {
            return _dbContext.Companies.AsQueryable()
                .Where(c => c.CNPJ == CNPJ)
                .FirstOrDefault();
        }

        public bool PostContact(Contact contact) 
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();

            if(_dbContext.Contacts.AsQueryable().
                Where(c => c.Email == contact.Email).Count() > 0) 
            {
                return true;
            }

            return false;
        }

        public bool PostCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();

            if (_dbContext.Companies.AsQueryable().
                Where(c => c.CNPJ == company.CNPJ).Count() > 0)
            {
                return true;
            }

            return false;
        }

        public void PostCompanies(IEnumerable<Company> companies)
        {
            companies.ToList().ForEach(c => _dbContext.Companies.Add(c));
            _dbContext.SaveChanges();
        }

        public void PostContacts(IEnumerable<Contact> contacts)
        {
            contacts.ToList().ForEach(c => _dbContext.Contacts.Add(c));
            _dbContext.SaveChanges();
        }

        public void DeleteCompany(string id)
        {
            var company = _dbContext.Companies.Where(c => c.Id == id)
                .FirstOrDefault();

            if (company != null)
            {
                _dbContext.Companies.Remove(company);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteContact(string email)
        {
            var contact = _dbContext.Contacts.Where(c => c.Email == email)
                .FirstOrDefault();

            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
            }
        }
    }
}
