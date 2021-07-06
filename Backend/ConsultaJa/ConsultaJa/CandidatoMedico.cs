using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaJa
{
    /* 
     * Classe que representa uma pedido 
     * de inscrição na aplicação por 
     * parte de um médico 
     */
    public class CandidatoMedico
    {
        /**
         * Variável que guarda o nome do médico 
         * a ser inscrito na aplicação 
         */
        private string nome;

        /**
         * Variável que guarda o email do candidato 
         * em questão 
         */
        private string email;

        /**
         * Variável que guarda a data de nascimento 
         * do candidato a médico em questão
         */
        private DateTime dataNascimento;

        /**
         * Contactos pertencentes ao candidato
         * a médico pelos quais os responsáveis 
         * pela aplicação poderão contactá-lo para 
         * comprovar o seu estatuto
         */
        private List<string> contactos;

        /**
         * Variável que guarda a morada do 
         * candidato em questão
         */
        private string morada;

        /**
         * Variável que guarda o nif do candidato 
         * em questão
         */
        private string nif;

        /**
         * Variável que guarda o código postal 
         * do candidato em questão
         */
        private string codigo_postal;

        /**
         * Variável que guarda a localidade 
         * do Candidato em questão
         */
        private string localidade;

        /**
         * Construtor para objetos da 
         * classe CandidatoMedico
         */
        public CandidatoMedico(string nome, string email, DateTime dataNascimento, string morada,
            string nif, string codigo_postal, string localidade)
        {
            this.nome = nome;
            this.email = email;
            this.dataNascimento = dataNascimento;
            this.morada = morada;
            this.nif = nif;
            this.codigo_postal = codigo_postal;
            this.localidade = localidade;
            this.contactos = new List<string>();
        }

        /**
         * Método que retorna o nome do objeto da 
         * classe CandidatoMedico ao qual é enviado 
         * o parâmetro
         */
        public string getNome()
        {
            return this.nome;
        }

        /**
         * Método que retorna o email do objeto da 
         * classe CandidatoMedico ao qual é enviado 
         * o parâmetro
         */
        public string getEmail()
        {
            return this.email;
        }

        /**
         * Método que retorna a data de nascimento do objeto da 
         * classe CandidatoMedico ao qual é enviado 
         * o parâmetro
         */
        public DateTime getDataNascimento()
        {
            return this.dataNascimento;
        }

        /**
         * Método que permite adicionar um contacto 
         * a um objeto da classe CandidatoMedico
         */
        public void addContacto(string contacto)
        {
            this.contactos.Add(contacto);
        }

        /**
         * Método que retorna uma lista com os contactos 
         * do objeto da classe CandidatoMedico ao qual é 
         * enviado o método
         */
        public List<string> getContactos()
        {
            List<string> ret = new List<string>();
            foreach(string contacto in this.contactos)
            {
                ret.Add(contacto);
            }
            return ret;
        }

        /**
         * Método que retorna a morada do objeto da 
         * classe CandidatoMedico ao qual é enviado 
         * o método
         */
        public string getMorada()
        {
            return this.morada;
        }

        /**
         * Método que retorna o nif do objeto da 
         * classe CandidatoMedico ao qual é enviado 
         * o método
         */
        public string getNif()
        {
            return this.nif;
        }

        /**
         * Método que retorna o código postal
         */
        public string getCodigo_Postal()
        {
            return this.codigo_postal;
        }

        /**
         * Método que retorna a localidade do objeto 
         * da classe CandidatoMedico ao qual é 
         * enviado o método
         */
        public string getLocalidade()
        {
            return this.localidade;
        }
    }
}
