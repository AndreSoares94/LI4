using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaJaDB;

namespace ConsultaJa
{
    class TesteDAO
    {
        /**
         * Método que permite registar um novo
         * médico na base de dados
         */
        public static void registaMedico(ContaDAO cdao, ConfigsDAO confdao)
        {
            int id = confdao.getAndIncrement("medicos");
            string idMedico = "M" + id;
            Medico m = new Medico(idMedico,"albertosantos1989@gmail.com", "asantos1989", "Alberto Santos", 0, 0,
                    new DateTime(1989, 7, 15), "242798140", "Rua da Silva", "4730-280", "Braga");
            m.addContacto("932541002");
            m.addContacto("938912002");
            m.addContacto("911411212");
            cdao.put(idMedico, m);
        }

        /**
         * Método que permite registar um novo
         * cliente na base de dados
         */
        public static void registaPaciente(ContaDAO cdao, ConfigsDAO confdao)
        {
            int id = confdao.getAndIncrement("pacientes");
            string idPaciente = "P" + id;
            Paciente p = new Paciente(idPaciente, "carlosSilvaa@hotmail.com", "carlosSSilva1994",
                    "Carlos Santos Silva", "Rua de Baixo nº155", "784512231", new DateTime(1994, 5, 24),
                    "4730-280", "Braga");
            p.addContacto("935425789");
            p.addContacto("917747257");
            cdao.put(idPaciente, p);
        }

        /**
         * Método que permite adicionar uma consulta 
         * fornecendo os dois id's quer de médico 
         * quer de paciente
         */
        public static void registarConsulta(ConsultaDAO consdao, ContaDAO cdao, 
            string idPaciente, string idMedico)
        {
            Medico m = (Medico)cdao.get("M0");
            Console.WriteLine("Id do médico da consulta: " + m.getID());
            Paciente p = (Paciente)cdao.get("P0");
            Console.WriteLine("Id do paciente da consulta: " + p.getID());
            Consulta c = new Consulta(0, p, m, "Braga", "Rua de Cima", null, 2020, 05, 25, 16, 30, 00, Consulta.PEDIDO);
            consdao.put(c);
        }

        /**
         * Método que imprime uma lista para o ecrã
         */
        public static void printList(string title, List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine(title + ": {");
            int i = 0;
            foreach(string s in list)
            {
                if (i != 0)
                    sb.Append(",");
                sb.Append(s);
                Console.WriteLine(sb.ToString());
                sb.Clear();
                i++;
            }
            Console.WriteLine("}");
        }

        /**
         * Método que arranca o programa 
         * de teste
         */
         /*
        public static void Main(string[] args)
        {
            ContaDAO cdao = ContaDAO.getInstance();
            InfoGeralDAO igdao = InfoGeralDAO.getInstance();
            ConsultaDAO consdao = ConsultaDAO.getInstance();
            ConfigsDAO confdao = ConfigsDAO.getInstance();
            try
            {
                //registaPaciente(cdao,confdao);
                //registaMedico(cdao,confdao);
                Console.WriteLine("Médico :");
                Console.WriteLine(cdao.get("M0").ToString());
                Console.WriteLine("Paciente :");
                Console.WriteLine(cdao.get("P0").ToString());
                Console.WriteLine("Size: " + cdao.size());
                //registarConsulta(consdao, cdao, "P0", "M0");
                Console.WriteLine(consdao.get(1).ToString());
                */
                /* Alterar o valor do 
                 * preço por consulta */
                 /*
                confdao.setValue("preco", 35000);
                int cent = confdao.get("preco");
                */
                /* Obter o valor dos parâmetros 
                 * da base de dados */
                 /*
                Console.WriteLine("Nº de Médicos: " + confdao.get("medicos"));
                Console.WriteLine("Nº de Pacientes: " + confdao.get("pacientes"));
                Console.WriteLine("Preço por consulta: " + cent / 100 + "$");
                //igdao.put("P0", "Alergias", "Bruffen");
                //igdao.put("P0", "Alergias", "Paracetemol");
                //igdao.put("P0", "Alergias", "Insetos");
                //igdao.put("P0", "Tipo de Sangue", "O(-)");
                printList("Alergias de P0", igdao.get("P0", "Alergias"));
            }
            catch(Exception exc)
            {
                Console.WriteLine("[" + exc.GetType() + "] - [" + exc.GetBaseException() + "] - " + exc.Message);
            }
        }*/
    }
}