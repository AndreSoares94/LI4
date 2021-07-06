using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaJa.Models
{
    public class AdminModel
    {
        public string Type { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public string NumMedicos { get; set; }
        public string NumPacientes { get; set; }
        public string Preco { get; set; }
    }
}

