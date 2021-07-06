using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaJa
{
    public class Receita
    {
        /**
         * Variável que guarda uma lista com todas 
         * as prescrições de farmacos especificadas 
         * na propria receita
         */
        private List<Prescricao> prescricoes;

        /**
         * Variável que guarda o inteiro identificador 
         * da receita que corresponde ao id da consulta 
         * à qual a receita está associada
         */
        private int idReceita;

        /**
         * Variável que guarda o nome do Paciente 
         * associado à receita
         */
        private string nomePaciente;

        /**
         * Variável que guarda a lista de 
         * contactos do paciente
         */
        private List<string> contactosPaciente;

        /**
         * Variável que guarda o Nif do paciente 
         * associado à receita
         */
        private string nifPaciente;

        /**
         * Variável que guarda o nome do Medico 
         * associado à receita
         */
        private string nomeMedico;

        /**
         * Variável que guarda a lista de 
         * contactos do medico
         */
        private List<string> contactosMedico;

        /**
         * Variável que guarda o Nif do medico 
         * associado à receita
         */
        private string nifMedico;

        /**
         * Construtor para objetos da classe Receita
         */
        public Receita(List<Prescricao> prescricoes, int idReceita, string nomePaciente, List<string> contactosPaciente, 
            string nifPaciente, string nomeMedico, List<string> contactosMedico, string nifMedico)
        {
            this.prescricoes = prescricoes;
            this.idReceita = idReceita;
            this.nomePaciente = nomePaciente;
            this.contactosPaciente = contactosPaciente;
            this.nifPaciente = nifPaciente;
            this.nomeMedico = nomeMedico;
            this.contactosMedico = contactosMedico;
            this.nifMedico = nifMedico;
        }

        public List<Prescricao> getPrescricoes()
        {
            return this.prescricoes;
        }

        public int getIdReceita()
        {
            return this.idReceita;
        }

        public string getNomePaciente()
        {
            return this.nomePaciente;
        }

        public List<string> getContactosPaciente()
        {
            return this.contactosPaciente;
        }

        public string getNifPaciente()
        {
            return this.nifPaciente;
        }

        public string getNomeMedico()
        {
            return this.nomeMedico;
        }

        public List<string> getContactosMedico()
        {
            return this.contactosMedico;
        }

        public string getNifMedico()
        {
            return this.nifMedico;
        }
    }
}
