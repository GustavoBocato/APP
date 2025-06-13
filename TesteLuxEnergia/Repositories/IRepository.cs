using TesteLuxEnergia.Models;

namespace TesteLuxEnergia.Repositories
{
    public interface IRepository
    {
        public IEnumerable<Contact> GetAllContacts();
        public IEnumerable<Company> GetAllCompanies();
        public IEnumerable<Contact> GetContactsByCompany(string companyName);
        public IEnumerable<Contact> GetAllClients();
        public int GetContactsCount();
        public int GetCompaniesCount();
        public Contact? GetContactByEmail(string email);
        public Company? GetCompanyByCNPJ(string CNPJ);
        public bool PostContact(Contact contact);
        public bool PostCompany(Company company);
        public void PostCompanies(IEnumerable<Company> companies);
        public void PostContacts(IEnumerable<Contact> contacts);
        public void DeleteCompany(string id);
        public void DeleteContact(string email);
    }
}