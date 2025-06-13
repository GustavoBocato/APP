using APP.Models;

namespace APP.Services
{
    public interface IService
    {
        public IEnumerable<Contact> GetAllContacts();
        public IEnumerable<Company> GetAllCompanies();
        public IEnumerable<Contact> GetContactsByCompany(string companyName);
        public IEnumerable<Contact> GetAllClients();
        public int GetContactsCount();
        public int GetCompaniesCount();
        public void PostContact(Contact contact);
        public void PostCompany(Company company);
        public void ReadXlsx(IFormFile file);
        public void DeleteCompany(string id);
        public void DeleteContact(string email);
        public MemoryStream WriteXlsx();
    }
}
