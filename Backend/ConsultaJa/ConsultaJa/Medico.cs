using System;
using System.Collections.Generic;
using System.Text;
using ConsultaJaDB;

namespace ConsultaJa
{
    public class Medico : Conta
    {
        /**
         * Variável que guarda a morada do médico
         */
        private string morada;

        /**
         * Variável que guarda o nif do médico
         */
        private string nif;

        /**
         * Variável que guarda a classificação 
         * do médico em questão
         */
        private double classificacao;

        /**
         * Variável que guarda a quantidade 
         * de classificações feitas ao médico
         */
        private int numClassificacoes;

        /**
         * Variável que guarda o saldo 
         * do médico em questão
         */
        private int saldo;

        /**
         * Variável de instância que guarda 
         * o código postal pertencente ao médico
         */
        private string codigo_postal;

        /**
         * Variável que guarda a localidade do médico
         */
        private string localidade;

        /**
         * Variável que permite aceder às 
         * consultas na base de dados
         */
        private ConsultaDAO consultas;

        /**
         * Construtor para objetos 
         * da classe médico
         */
        public Medico(string id, string email, string password, string nome, 
            double classificacao, int numClassificacoes, DateTime dataNascimento, 
            string nif, string morada, string codigo_postal,string localidade) : 
            base(email,password,nome,dataNascimento)
        {
            this.setID(id);
            this.nif = nif;
            this.morada = morada;
            this.classificacao = classificacao; /* Valor que representa ausência de qualquer classificação */
            this.numClassificacoes = numClassificacoes;
            this.saldo = 0;
            this.codigo_postal = codigo_postal;
            this.localidade = localidade;
            this.consultas = ConsultaDAO.getInstance();
        }

        /**
         * Método que retorna o saldo do objeto da 
         * classe Medico ao qual é enviado o método
         */
        public double getSaldo()
        {
            return (double)saldo / 100;
        }

        /**
         * Método que retorna a classificação do 
         * objeto da classe Médico ao qual é enviado 
         * o respetivo médico
         */
        public double getClassificacao()
        {
            return this.classificacao;
        }

        /**
         * Método que retorna a morada do objeto da 
         * classe Medico ao qual é enviado o método
         */
        public string getMorada()
        {
            return this.morada;
        }

        /**
         * Método que retorna o nif do objeto da 
         * classe Medico ao qual é enviado o método
         */
        public string getNif()
        {
            return this.nif;
        }

        /**
         * Método que permite obter o número 
         * de classificações feitas a um médico
         */
        public int getNumClassificacoes()
        {
            return this.numClassificacoes;
        }

        /**
         * Método que retorna o código 
         * postal de um médico
         */
        public string getCodigo_Postal()
        {
            return this.codigo_postal;
        }

        /**
         * Método que retorna a localidade 
         * de um dado médico
         */
        public string getLocalidade()
        {
            return this.localidade;
        }

        /**
         * Método que retorna uma lista com todos os 
         * contactos disponíveis associados a um médico
         */
        public List<string> getContactos()
        {
            return base.getContactos();
        }

        /**
         * Método que retorna todas as consultas 
         * agendadas para um médico
         */
        public override List<Consulta> getConsultasAgendadas()
        {
            return consultas.getAsMedicoAgendadas(this.getID());
        }

        /**
         * Método que retorna todas as consultas 
         * realizadas de um médico
         */
        public override List<Consulta> getHistorico()
        {
            return consultas.getAsMedicoHistorico(this.getID());
        }

        /**
         * Método que permite fazer uma proposta de 
         * consulta mediante um pedido de consulta 
         * previamente feito por um paciente
         */
        public void submeterProposta(int idConsulta, int precoDB)
        {
            this.consultas.submeterProposta(idConsulta, this.getID(), precoDB);
        }

        /**
         * Método que permite adicionar um certo 
         * numerário à carteira digital do médico
         */
        public void pagamento(int numerario)
        {
            this.saldo += numerario;
        }

        /**
         * Método que permite adicionar um contacto 
         * à lista de contactos de um médico
         */
        public void addContacto(string contacto)
        {
            base.addContacto(contacto);
        }

        /**
         * Método que permite adicionar uma classificação 
         * ao objeto da classe Medico ao qual é enviado 
         * o método
         */
        public void classificar(double classificacao)
        {
            if (classificacao <= 5 && classificacao >= 0)
            {
                this.classificacao *= (double)this.numClassificacoes;
                this.classificacao += classificacao;
                this.numClassificacoes++;
                this.classificacao /= (double)this.numClassificacoes;
            }
            else
                throw new ClassificacaoInvalida("Classificação Inválida");
        }

        /**
         * Implementação do método ToString 
         * para objetos da classe Medico
         */
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Nif: ");
            sb.Append(this.nif);
            sb.Append("; Morada: ");
            sb.Append(this.morada);
            sb.Append("; Classificação: ");
            sb.Append(this.classificacao);
            sb.Append("; Contactos: {");
            foreach (string s in this.getContactos())
            {
                sb.Append(s);
                sb.Append(", ");
            }
            sb.Append("}");
            return sb.ToString();
        }

        /**
		 * Método que permite efetuar um carregamento 
		 * para a carteira digital de um cliente na 
		 * aplicação
		 */
        public int efetuaCarregamento(int montante)
        {
            return this.saldo += montante;
        }

        /**
         * Método que torna o médico como 
         * aceite na aplicação
         */
        public void aceitaMedico()
        {
            if (this.numClassificacoes < 0)
                this.numClassificacoes = 0;
        }

        /**
         * Método que retorna true se e só se o médico 
         * foi aceite pelo administrador da aplicação
         */
        public Boolean aprovado()
        {
            return this.numClassificacoes >= 0;
        }

        /**
         * Implementação do método equals 
         * para objetos da classe Medico
         */
        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            if (obj == null || !this.GetType().Equals(obj.GetType())) return false;

            Medico m = (Medico)obj;

            return m.getID().Equals(this.getID());
        }
    }
}