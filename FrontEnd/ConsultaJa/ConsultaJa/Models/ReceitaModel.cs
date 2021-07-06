using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaJa.Models
{
    public class ReceitaModel
    {
        public int Id { get; set; }
        public string Observacoes { get; set; }
        public List<PrescricaoModel> Prescricoes { get; set; }
        public string Utente { get; set; }
        public string ContactoUt { get; set; }
        public string NIFUt { get; set; }
        public string Medico { get; set; }
        public string ContactoMed { get; set; }
        public string NIFMed { get; set; }
    }
}
