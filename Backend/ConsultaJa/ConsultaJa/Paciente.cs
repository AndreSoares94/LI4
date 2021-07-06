using System;
using System.Collections.Generic;
using System.Text;
using ConsultaJaDB;

namespace ConsultaJa
{
	public class Paciente : Conta
	{

		/**
		 * Variável que guarda a morada do paciente 
		 */
		private string morada;

		/**
		 * Variável que representa o nif 
		 * do objeto da classe paciente 
		 */
		private string nif;

		/**
		 * Variável que guarda o saldo do paciente 
		 */
		private int saldo;

		/**
		 * Variável de instância que corresponde ao 
		 * código postal do paciente
		 */
		private string codigo_postal;

		/**
		 * Variável que guarda a localidade do paciente
		 */
		private string localidade;

		/**
		 * Variável que nos permite aceder às consultas 
		 * registadas na base de dados
		 */
		private ConsultaDAO consultas;

		/**
		 * Variável que nos permite aceder às informações 
		 * gerais de um paciente na base de dados
		 */
		private InfoGeralDAO info;

		/**
		 * Construtor para objetos da classe Paciente 
		 */
		public Paciente(string id, string email, string password, string nome, 
			string morada, string nif, DateTime dataNascimento, string codigo_postal,
			string localidade) : 
			base(email,password,nome,dataNascimento)
		{
			this.setID(id);
			this.morada = morada;
			this.nif = nif;
			this.saldo = 0;
			this.codigo_postal = codigo_postal;
			this.consultas = ConsultaDAO.getInstance();
			this.localidade = localidade;
			this.info = InfoGeralDAO.getInstance();
		}

		/**
		 * Método que retorna o mail do objeto 
		 * da classe Paciente ao qual é enviado 
		 * o método
		 */
		public string getEmail() 
		{
			return base.getEmail();
		}

		/**
		 * Método que retorna a password do objeto 
		 * da classe Paciente ao qual é enviado 
		 * o método
		 */
		public string getPassword()
		{
			return base.getPassword();
		}

		/**
		 * Método que retorna o nome do 
		 * paciente ao qual é enviado o método
		 */
		public string getNome()
		{
			return base.getNome();
		}

		/**
		 * Método que retorna o código
		 * postal do objeto da classe Paciente 
		 * ao qual é enviado o método
		 */
		public string getCodigo_Postal()
		{
			return this.codigo_postal;
		}

		/**
		 * Método que retorna o nif do objeto da 
		 * classe Paciente ao qual é enviado o 
		 * método
		 */
		public string getNif()
		{
			return this.nif;
		}

		/**
		 * Método que retorna o saldo do objeto 
		 * da classe Paciente ao qual é enviado 
		 * o método
		 */
		public int getSaldo()
		{
			return this.saldo;
		}

		/**
		 * Método que retorna a morada do objeto 
		 * da classe Paciente ao qual é enviado 
		 * o método
		 */
		public string getMorada()
		{
			return this.morada;
		}

		/**
         * Método que retorna uma lista com todos os 
         * contactos disponíveis associados a um paciente
         */
		public List<string> getContactos()
		{
			return base.getContactos();
		}

		/**
         * Método que retorna a localidade 
         * de um dado paciente
         */
		public string getLocalidade()
		{
			return this.localidade;
		}

		/**
		 * Método que permite aceder ao histórico de 
		 * consultas de um médico
		 */
		public override List<Consulta> getHistorico()
		{
			return consultas.getAsMedicoHistorico(this.getID());
		}

		/**
		 * Método que permite aceder à lista de consultas 
		 * agendadas para um paciente
		 */
		public override List<Consulta> getConsultasAgendadas()
		{
			return consultas.getAsPacienteAgendadas(this.getID());
		}

		/**
		 * Método que permite a atribuição de um 
		 * valor à variavel id do paciente ao qual 
		 * é enviado o método
		 */
		public void setID(string id)
		{
			base.setID(id);
		}

		/**
		 * Método que permite adicionar um 
		 * contacto a um paciente
		 */
		public void addContacto(string contacto)
		{
			base.addContacto(contacto);
		}

		/**
		 * Método que permite adicionar uma informação 
		 * ao perfil de um paciente 
		 */
		public void addInfo(string descricao, string info)
		{
			this.info.put(this.getID(), descricao, info);
		}

		/**
		 * Implementação do método ToString 
		 * para objetos da classe Paciente 
		 */
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Email: ");
			sb.Append(this.getEmail());
			sb.Append("; ");
			sb.Append("NIF: ");
			sb.Append(this.nif);
			return sb.ToString();
		}

		/**
		 * Método que permite propor uma 
		 * consulta a um paciente
		 */
		public void addPropostaConsulta(string localidade, int ano, int mes, int dia, 
			int hora, int minuto, int segundo)
		{
			/* É de notar que o id inserido neste caso em 
			 * nada afeta, visto que ao inserir no cdao, 
			 * esse id é despresado
			 */
			Consulta c = new Consulta(-1, this, null, localidade, this.morada, null, ano, mes, dia, hora, minuto, segundo, Consulta.PEDIDO);
			/* Notar que a única maneira de adicionar 
			 * consultas à aplicação é a partir do paciente*/
			this.consultas.put(c);
		}

		/**
		 * Método que permite aceitar uma proposta 
		 * de consulta fornecendo o id da respetiva 
		 * consulta
		 */
		public void aceitarProposta(int idConsulta)
		{
			/* Aceitamos a proposta de consulta */
			this.consultas.aceitarProposta(idConsulta);
		}

		/**
		 * Método que move uma consulta da lista 
		 * de agendadas para o histórico
		 */
		public void moveParaHistorico(int idConsulta)
		{
			/* Marcamos a consulta como realizada */
			this.consultas.marcarRealizada(idConsulta);
		}

		/**
		 * Método que permite rejeitar uma proposta 
		 * de consulta fornecendo o id da respetiva 
		 * consulta
		 */
		public void rejeitarProposta(int idConsulta)
		{
			/* Apenas removemos a consulta da 
			 * lista de pendentes */
			this.consultas.remove(idConsulta);
		}

		/**
         * Método que permite desmarcar a consulta de um médico
         */
		public void desmarcarConsulta(int idConsulta)
		{
			/* Removemos a consulta da lista das agendadas */
			//VER COMO VAMOS GERIR A DESMARCAÇÃO DE CONSULTAS
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
         * Implementação do método equals 
         * para objetos da classe Medico
         */
		public override bool Equals(object obj)
		{
			if (this == obj) return true;

			if (obj == null || !this.GetType().Equals(obj.GetType())) return false;

			Paciente p = (Paciente)obj;

			return p.getID().Equals(this.getID());
		}
	}
}