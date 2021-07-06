using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConsultaJa.Models;
using Newtonsoft.Json;
using System;
using ConsultaJa.Backend;
using ConsultaJa.Exceptions;
using ConsultaJa.Services;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ConsultaJa.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ReactPolicy")]
    public class AdminController : ControllerBase
    {
        private ConsultaJaModel model = new ConsultaJaModel();
        private AdminService service = new AdminService();

        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        //GET /admin
       [HttpGet]
       [Authorize]
        public async Task<IActionResult> Get([FromQuery]string admin)
        {
            AdminModel cmodel = new AdminModel();

            cmodel.Preco = this.model.getPreco();
            cmodel.NumMedicos = this.model.getMedicos();
            cmodel.NumPacientes = this.model.getPacientes();
            
            return Ok(cmodel);
        }

        /* /admin/aceitaMed */
        [HttpGet("aceitaMed")]
        [Authorize]
        public async Task<IActionResult> aceitaMedico([FromQuery]string id, [FromQuery]string action)
        {
            bool med = false;
            if (action.CompareTo("true") == 0)
                med = true;
            try
            {
                this.model.trataPedido(id, med);
            }
            catch (MailNaoRegistado e)
            {
                return Unauthorized();
            }

            return Ok();
        }

        /* GET /admin/mudarpreco
         * Mudar preco por consulta
         */
        [HttpGet("mudarpreco")]
        [Authorize]
        public async Task<IActionResult> MudarPreco([FromQuery] float novopreco)
        {
            this.model.mudarPreco((int)(novopreco*100));

            return Ok();
        }

        /* /admin/login */
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AdminModel admin)
        {
            Conta c = null;
            try
            {
                string code = this.model.getAdminCode();
                if (code.CompareTo(admin.Senha)==0)
                {
                    AdminModel amodel = new AdminModel();
                    amodel.Type = "Admin";
                    amodel.Preco = this.model.getPreco();
                    amodel.NumMedicos = this.model.getMedicos();
                    amodel.NumPacientes = this.model.getPacientes();
                    AdminModel adminM = service.Authenticate(amodel);
                    if (adminM == null)
                    {
                        return BadRequest("Erro ao processar login!");
                    }
                    return Ok(adminM);
                } else
                {
                    return BadRequest("Senha inserida incorreta!!!!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /* /admin/listaMed
        Obter a lista das consultas agendadas dado o id de um Medico ou Paciente
        */
        [HttpGet("listaMed")]
        [Authorize]
        public ActionResult Get()
        {
            List<Conta> lc = null;
            List<ContaModel> lcm = new List<ContaModel>();

            lc = this.model.getCandidatos();

            foreach(Conta c in lc)
            {
                ContaModel m = new ContaModel();

                m.Type = "Medico";
                m.Id = c.getID();
                m.Nome = c.getNome();
                m.Email = c.getEmail();
                m.Password = c.getPassword();
                m.DataNascimento = c.getDataNascimento().ToString().Substring(0,10);
                //m.Morada = c.getMorada();
                //m.Nif = c.getNif();
                //m.Codigo_postal = c.getCodigoPostal();
                m.Contactos = c.getContactos()[0];
                //m.Localidade = c.getLocalidade();

                lcm.Add(m);

            }

            return Ok(lcm);
        }

        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }
    }
}
