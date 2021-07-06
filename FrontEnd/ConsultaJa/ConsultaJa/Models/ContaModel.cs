using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaJa.Models
{
    public class ContaModel
    {
        public string Type { get; set; }
        public string Token { get; set; }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordNova { get; set; }
        public string DataNascimento { get; set; }
        public string Morada { get; set; }
        public string Nif { get; set; }
        public string Codigo_postal { get; set; }
        public string Contactos { get; set; }
        public string Localidade { get; set; }
    }
}
