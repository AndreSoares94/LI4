using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaJa.Models
{
    public class ConsultaModel
    {
        public int Id { get; set; }
        public string Paciente { get; set; }
        public string Medico { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
		public string Localidade { get; set; }
		public int PrecoUni { get; set; }
        public string Morada { get; set; }
        public int Estado { get; set; }
        public string Observacoes { get; set; }
	}
}
