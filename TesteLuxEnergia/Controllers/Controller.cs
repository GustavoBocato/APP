using Microsoft.AspNetCore.Mvc;
using TesteLuxEnergia.Models;
using TesteLuxEnergia.Services;

namespace TesteLuxEnergia.Controllers
{
    /// <summary>
    /// A minha solução para o desafio foi uma API em C#, que pode ser acessada pelo swagger.
    /// Obviamente que essa solução é apenas uma prova de conceito, com mais tempo eu poderia
    /// desenvolver um front apropriado e usar um banco de dados real e não apenas um mockado em
    /// memória. Porém todo o MVP do projeto está desenvolvido aqui. E a aplicação consegue 
    /// atingir persistencia de dados por meio do endpoint de exportar a base em Excel.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;
        private readonly IService _service;

        public Controller(ILogger<Controller> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Retorna todos os contatos que trabalham na empresa informada.
        /// </summary>
        /// <param name="nomeEmpresa">Nome da empresa</param>
        /// <returns>Lista de contatos</returns>
        /// <response code="200">Contatos encontrados</response>
        /// <response code="404">Empresa não encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("GetContactsByCompanyName")]
        public ActionResult GetContactsByCompanyName(string companyName)
        {
            IEnumerable<Contact> contacts = _service.GetContactsByCompany(companyName);

            if (contacts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        /// <summary>
        /// Retorna todos os contatos de clientes.
        /// </summary>
        /// <returns>Lista de contatos</returns>
        /// <response code="200">Contatos encontrados</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("GetAllContactsUnderOurManager")]
        public ActionResult GetAllClients()
        {
            return Ok(_service.GetAllClients());
        }

        /// <summary>
        /// Retorna a contagem dos contatos e empresas.
        /// </summary>
        /// <returns>Json com dois inteiros</returns>
        /// <response code="200">Contagem feita com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("GetContactsAndCompaniesCounts")]
        public ActionResult GetContactsAndCompaniesCounts()
        {
            var counts = new
            {
                CompaniesCounts = _service.GetCompaniesCount(),
                ContactCount = _service.GetContactsCount()
            };

            return Ok(counts);
        }

        /// <summary>
        /// Retorna todos os contatos.
        /// </summary>
        /// <returns>Lista de contatos</returns>
        /// <response code="200">Contatos encontrados</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("GetAllContacts")]
        public ActionResult GetAllContacts()
        {
            return Ok(_service.GetAllContacts());
        }

        /// <summary>
        /// Retorna todos as empresas.
        /// </summary>
        /// <returns>Lista de empresas</returns>
        /// <response code="200">Empresas encontradas</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("GetAllCompanies")]
        public ActionResult GetAllCompanies()
        {
            return Ok(_service.GetAllCompanies());
        }

        /// <summary>
        /// Registra contato na base de dados.
        /// </summary>
        /// <response code="200">Contato registrado com sucesso</response>
        /// <response code="400">Email do contato a ser registrado já existe na base</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("PostContact")]
        public ActionResult PostContact(Contact contact)
        {
            try
            {
                _service.PostContact(contact);
                return Ok("Contato registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registra empresa na base de dados.
        /// </summary>
        /// <response code="200">Empresa registrada com sucesso</response>
        /// <response code="400">CNPJ da empresa a ser registrado já existe na base</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("PostCompany")]
        public ActionResult PostCompany(Company company)
        {
            try
            {
                _service.PostCompany(company);
                return Ok("A empresa foi registrada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCompany")]
        public ActionResult DeleteCompany(string id)
        {
            _service.DeleteCompany(id);
            return Ok("A empresa foi deletada com sucesso.");
        }

        [HttpDelete("DeleteContact")]
        public ActionResult DeleteContact(string email)
        {
            _service.DeleteContact(email);
            return Ok("O contato foi deletado com sucesso.");
        }

        /// <summary>
        /// Lê arquivos xlsx e salva os contatos e empresas na base de dados.
        /// </summary>
        /// <response code="200">Arquivo lido com sucesso</response>
        /// <response code="400">Arquivo vazio</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("O arquivo está vazio.");

            _service.ReadXlsx(file);

            return Ok("O arquivo foi lido com sucesso.");
        }

        /// <summary>
        /// Exporta todo a base de dados para excel.
        /// </summary>
        /// <response code="200">Excel exportado com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var stream = _service.WriteXlsx();

            return File(
                stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Dados.xlsx"
            );
        }
    }
}
