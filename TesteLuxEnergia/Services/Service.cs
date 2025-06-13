using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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

        public IEnumerable<Contact> GetAllContacts()
        {
            return _repository.GetAllContacts();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _repository.GetAllCompanies();
        }

        public IEnumerable<Contact> GetContactsByCompany(string companyName)
        {
            return _repository.GetContactsByCompany(companyName);
        }

        public IEnumerable<Contact> GetAllClients()
        {
            return _repository.GetAllClients();
        }

        public int GetContactsCount()
        {
            return _repository.GetContactsCount();
        }

        public int GetCompaniesCount()
        {
            return _repository.GetCompaniesCount();
        }

        public void PostContact(Contact contact)
        {
            if (_repository.GetContactByEmail(contact.Email) != null)
            {
                throw new Exception("O email do contato a ser registrado já foi cadastrado" +
                    " para um outro contato na nossa base de dados.");
            }

            if (!_repository.PostContact(contact))
            {
                throw new Exception("Algum erro ocorreu ao salvar o contato na nossa base de dados.");
            };
        }

        public void PostCompany(Company company)
        {
            if (_repository.GetCompanyByCNPJ(company.CNPJ) != null)
            {
                throw new Exception("O CNPJ da empresa a ser registrada já consta" +
                    " em nossa base de dados.");
            }

            if (!_repository.PostCompany(company))
            {
                throw new Exception("Algum erro ocorreu ao salvar o contato na nossa base de dados.");
            };
        }

        public void DeleteCompany(string id)
        {
            _repository.DeleteCompany(id);
        }

        public void DeleteContact(string email)
        {
            _repository.DeleteContact(email);
        }

        public async void ReadXlsx(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);

            var sheet = package.Workbook.Worksheets["Dados"];

            var companies = new List<Company>();
            var contacts = new List<Contact>();

            for (int row = 3; row <= sheet.Dimension.End.Row; row++)
            {
                if (sheet.Cells[row, 2].Text == "")
                {
                    continue;
                }

                contacts.Add(new Contact
                {
                    Identifier = sheet.Cells[row, 2].Text,
                    Name = sheet.Cells[row, 3].Text,
                    Role = sheet.Cells[row, 4].Text,
                    CompanyName = sheet.Cells[row, 5].Text,
                    Telephone = sheet.Cells[row, 6].Text,
                    Email = sheet.Cells[row, 7].Text
                });
            }

            for (int row = 3; row <= sheet.Dimension.End.Row; row++)
            {
                if (sheet.Cells[row, 9].Text == "")
                {
                    continue;
                }

                companies.Add(new Company
                {
                    Id = sheet.Cells[row, 9].Text,
                    StreetName = sheet.Cells[row, 10].Text,
                    Number = int.Parse(sheet.Cells[row, 11].Text),
                    State = sheet.Cells[row, 12].Text,
                    City = sheet.Cells[row, 13].Text,
                    CEP = int.Parse(sheet.Cells[row, 14].Text),
                    Name = sheet.Cells[row, 15].Text,
                    CNPJ = sheet.Cells[row, 16].Text,
                    Distribuitor = sheet.Cells[row, 17].Text,
                    TaxModality = sheet.Cells[row, 18].Text,
                    PeakConsumption = sheet.Cells[row, 19].Text,
                    OutOfPeakConsumption = sheet.Cells[row, 20].Text,
                    AverageBillRate = sheet.Cells[row, 21].Text,
                    Manager = sheet.Cells[row, 22].Text
                });
            }

            _repository.PostCompanies(companies);
            _repository.PostContacts(contacts);
        }

        public MemoryStream WriteXlsx()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Dados");

            worksheet.Cells[2, 2].Value = "Identificador";
            worksheet.Cells[2, 3].Value = "Nome";
            worksheet.Cells[2, 4].Value = "Cargo";
            worksheet.Cells[2, 5].Value = "Empresa";
            worksheet.Cells[2, 6].Value = "Telefone";
            worksheet.Cells[2, 7].Value = "e-mail";

            worksheet.Cells[2, 9].Value = "Empresa";
            worksheet.Cells[2, 10].Value = "Endereço - Rua";
            worksheet.Cells[2, 11].Value = "Endereço - Numero";
            worksheet.Cells[2, 12].Value = "Endereço - Estado";
            worksheet.Cells[2, 13].Value = "Endereço - Cidade";
            worksheet.Cells[2, 14].Value = "Endereço - CEP";
            worksheet.Cells[2, 15].Value = "Razão Social";
            worksheet.Cells[2, 16].Value = "CNPJ";
            worksheet.Cells[2, 17].Value = "Distribuidora";
            worksheet.Cells[2, 18].Value = "Modalidade Tarifária";
            worksheet.Cells[2, 19].Value = "Consumo Ponta (kWh)";
            worksheet.Cells[2, 20].Value = "Consumo Fora Ponta (kWh)";
            worksheet.Cells[2, 21].Value = "Valor Médio da Fatura (R$)";
            worksheet.Cells[2, 22].Value = "Gestor Responsável (LUX)";

            var contacts = _repository.GetAllContacts().ToList();
            var companies = _repository.GetAllCompanies().ToList();

            for (int i = 0; i < contacts.Count; i++)
            {
                var c = contacts[i];
                worksheet.Cells[i + 3, 2].Value = c.Identifier;
                worksheet.Cells[i + 3, 3].Value = c.Name;
                worksheet.Cells[i + 3, 4].Value = c.Role;
                worksheet.Cells[i + 3, 5].Value = c.CompanyName;
                worksheet.Cells[i + 3, 6].Value = c.Telephone;
                worksheet.Cells[i + 3, 7].Value = c.Email;

            }

            for (int i = 0; i < companies.Count; i++)
            {
                var c = companies[i];
                worksheet.Cells[i + 3, 9].Value = c.Id;
                worksheet.Cells[i + 3, 10].Value = c.StreetName;
                worksheet.Cells[i + 3, 11].Value = c.Number;
                worksheet.Cells[i + 3, 12].Value = c.State;
                worksheet.Cells[i + 3, 13].Value = c.City;
                worksheet.Cells[i + 3, 14].Value = c.CEP;
                worksheet.Cells[i + 3, 15].Value = c.Name;
                worksheet.Cells[i + 3, 16].Value = c.CNPJ;
                worksheet.Cells[i + 3, 17].Value = c.Distribuitor;
                worksheet.Cells[i + 3, 18].Value = c.TaxModality;
                worksheet.Cells[i + 3, 19].Value = c.PeakConsumption;
                worksheet.Cells[i + 3, 20].Value = c.OutOfPeakConsumption;
                worksheet.Cells[i + 3, 21].Value = c.AverageBillRate;
                worksheet.Cells[i + 3, 22].Value = c.Manager;
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
