using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ConsultaJa.Models;
using Newtonsoft.Json;
using System;
using ConsultaJa.Backend;
using ConsultaJa.Exceptions;
using ConsultaJa.Services;

namespace ConsultaJa.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ReactPolicy")]
    public class ContasController : ControllerBase
    {
        private ConsultaJaModel model = new ConsultaJaModel();
        private ContaService service = new ContaService();

        private readonly ILogger<ContasController> _logger;

        public ContasController(ILogger<ContasController> logger)
        {
            _logger = logger;
        }

        //GET /contas/P5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetConta(string id)
        {
            Conta c = model.getConta(id);
            ContaModel cmodel = new ContaModel();
            cmodel.Type = (c.getID().Substring(0,1).CompareTo("P")==0) ? "Paciente" : "Medico";
            cmodel.Email = c.getEmail();
            cmodel.Nome = c.getNome();
            cmodel.DataNascimento = c.getDataNascimento().ToString().Substring(0,10);
            return Ok(cmodel);
        }

        /* POST /contas
        Criacao de uma nova conta
        */
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegistarConta([FromBody] ContaModel conta)
        {
            string id = "";

            if (conta.Type!=null && conta.Type.Equals("Paciente"))
            {
                DateTime data = DateTime.Parse(conta.DataNascimento);
                List<string> contac = new List<string>();
                contac.Add(conta.Contactos);
                id = this.model.novoPaciente(conta.Email, conta.Password, conta.Nome, data, conta.Morada, conta.Nif, conta.Codigo_postal, contac, conta.Localidade);
            }
            else
            {
                DateTime data = DateTime.Parse(conta.DataNascimento);
                this.model.fazerPedidoInscricao(conta.Email, conta.Password, conta.Nome, data, conta.Nif, conta.Morada, conta.Codigo_postal, conta.Localidade, conta.Contactos);
            }
            return Ok(id);
        }

        // PUT /contas/P5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult EditarConta(string id, [FromBody] ContaModel conta)
        {
            try
            {
                DateTime dataNasc;
                if (conta.DataNascimento == null)
                {
                    dataNasc = this.model.getConta(id).getDataNascimento();
                }
                else
                {
                    dataNasc = DateTime.Parse(conta.DataNascimento);
                }
                this.model.editarPerfil(id, conta.PasswordNova, conta.Password, conta.Morada, conta.Codigo_postal, conta.Nome, dataNasc);
            }
            catch(Exception e)
            {
                return Unauthorized(e);
            }
            return Ok(id);
        }

        /* /contas/login
        Login como Paciente ou Medico
        */
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] ContaModel conta)
        {
            Conta c = null;
            try
            {
                c = this.model.login(conta.Email, conta.Password);
                ContaModel cmodel = new ContaModel();
                cmodel.Type = (c.getID().Substring(0, 1).CompareTo("P") == 0) ? "Paciente" : "Medico";
                cmodel.Email = c.getEmail();
                cmodel.Nome = c.getNome();
                cmodel.Id = c.getID();
                cmodel.DataNascimento = c.getDataNascimento().ToString().Substring(0, 10);
                ContaModel user = service.Authenticate(cmodel, conta.Email, cmodel.Type);
                if(user == null)
                {
                    return BadRequest("Erro ao processar login!");
                }
                return Ok(user);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        /* /contas/codReg
        Verificar o código de registo de uma dada conta
        */
        [HttpGet("codReg")]
        [AllowAnonymous]
        public ActionResult CodigoRegisto([FromQuery] string id,
                                          [FromQuery] string codigo)
        {
            bool val = this.model.checkCod(id,codigo);

            if (val) return Ok();
            else return Unauthorized();
        }

        /*
         * Enviar email aquando do registo na conta
         */
        [HttpGet("email")]
        [AllowAnonymous]
        public ActionResult EnviarEmail([FromQuery] string email)
        {
            int codigo = -1;
            try
            {
                codigo = this.model.enviarEmail(email);
            } catch(Exception e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(codigo);
        }


        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }
    }
}









