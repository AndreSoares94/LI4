using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaJa
{
    class Teste
    {
        public static void Main(string[] args)
        {
            try
            {
                ConsultaJaModel cjm = new ConsultaJaModel();

                /* Registamos um paciente */
                //string idPaciente = cjm.novoPaciente("carlosSilva@gmail.com", "fcportocampeao", "Carlos Costa da Silva", new DateTime(1987, 11, 25), "Rua da Igreja",
                //        "251458221", "2785-191", new List<string>(), "Faro");

                //cjm.registarInfoGeralPaciente("P0", "Alergias", "Paracetemol");
                //cjm.registarInfoGeralPaciente("P0", "Tipo de Sangue", "O (-)");

                //cjm.addNovoContacto("P0", "910257200");
                //cjm.addNovoContacto("P0", "253645255");
                //cjm.addNovoContacto("P0", "939100251");
                //cjm.efetuaCarregamento("P1", 15000);
                //cjm.fazerPedidoInscricao("migueloliveira985@gmail.com", "miguelito1999", "Miguel Oliveira",
                //    new DateTime(1999, 08, 07), "266951155", "Rua do Cruzeiro nº38", "4730-280", "Braga");
                //cjm.trataPedido("M0", true);
                //cjm.solicitarConsulta("P0", 2020, 08, 15, 19, 45);
                //cjm.solicitarConsulta("P0", 2020, 09, 28, 14, 45);
                //cjm.proporConsulta("M0", 1);
                //cjm.aceitaConsulta(1);
                //cjm.proporConsulta("M1", 7);
                //cjm.addPrescricao(1, "Paracetemol 100mg", 2, "1 de manha e 1 antes de deitar");
                //cjm.addPrescricao(1, "Bruffen 1000mg", 5, "quando sentir dor");
                //Console.WriteLine("Pedidos pendentes:");
                //Receita r = cjm.getReceita(1);
                //cjm.addObsToConsulta(1, "Consulta de rotina\nProxima consulta será em 2022");
                foreach(Consulta c in cjm.getPedidos("4730-280"))
                {
                    Console.WriteLine(c.ToString());
                }
                //Console.WriteLine("Admin.code: " + cjm.getAdminCode());
                Console.WriteLine("Test program exited successfully");
            }
            catch (Exception exc)
            {
                Console.WriteLine("[" + exc.GetType() + "] - [" + exc.GetBaseException() + "] - " + exc.Message);
            }
        }
    }
}
