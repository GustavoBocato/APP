using Microsoft.AspNetCore.Mvc;
using TesteLuxEnergia.Models;
using TesteLuxEnergia.Services;

namespace TesteLuxEnergia.Controllers
{
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
        [HttpGet("GetAllContactsByCompanyName")]
        public ActionResult GetAllContactsByCompanyName(string companyName) 
        {
            IEnumerable<Contact> contacts = _service.GetContactsByCompany(companyName);

            if(contacts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(_service.GetContactsByCompany(companyName));
        }
    }
}
