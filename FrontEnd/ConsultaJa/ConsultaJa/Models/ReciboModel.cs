using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaJa.Models
{
    public class ReciboModel
    {
        public int Id { get; set; }
        public string Preco { get; set; }
        public string Data { get; set; }
        public string Utente { get; set; }
        public string ContactoUt { get; set; }
        public string NIFUt { get; set; }
        public string CodPostalUt { get; set; }
        public string DistritoUt { get; set; }
        public string Medico { get; set; }
        public string ContactoMed { get; set; }
        public string NIFMed { get; set; }
        public string CodPostalMed { get; set; }
        public string DistritoMed { get; set; }
    }
}
