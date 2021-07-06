using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaJa;
using MySql.Data.MySqlClient;

namespace ConsultaJaDB
{
    /**
	 * Classe que permite aceder à base de dados
	 * e extrair informações relacionadas
	 * com informações de pacientes
	 */
    class InfoGeralDAO
    {
		/**
		 * Variável de instância da classe 
		 * InfoGeralDAO que é retornada quando 
		 * é pedida uma nova instância
		 */
		private static InfoGeralDAO inst = null;

		/**
		 * String a partir da qual conseguimos 
		 * aceder à base de dados
		 */
		private string connectionstring;

		/**
		 * Método que permite carregar a password 
		 * de um ficheiro configs.txt
		 */
		private static string getPassword()
		{
			/* Criamos um objeto para ler 
			 * do ficheiro configs */
			StreamReader sr = new StreamReader("configs.txt");
			string ret = sr.ReadLine();
			/* Fechamos a stream que usamos 
			 * para ler do ficheiro */
			sr.Close();
			return ret;
		}

		/**
		 * Construtor para objetos da classe ContaDAO. 
		 * É de notar que este construtor é privado
		 */
		private InfoGeralDAO()
		{
			string server = "localhost";
			string database = "consultaja";
			string uid = "root";
			string password = getPassword();
			this.connectionstring = "SERVER=" + server + ";" + "DATABASE=" +
			database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
		}

		/**
		 * Método que permite obter um único 
		 * objeto da classe ContaDAO
		 */
		public static InfoGeralDAO getInstance()
		{
			if (InfoGeralDAO.inst == null)
				InfoGeralDAO.inst = new InfoGeralDAO();
			return InfoGeralDAO.inst;
		}

		/**
		 * Método que retorna o número de tipos 
		 * de informações acerca de um cliente na base de dados
		 */
		public int size(string idPaciente)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("select * from listinfogeral where Paciente_idPaciente='");
			sb.Append(idPaciente);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			int ret = dt.Rows.Count;

			/* Fechamos a conexão */
			connection.Close();

			return ret;
		}

		/**
		 * Método que nos diz se existe alguma 
		 * informação associada a um dado paciente 
		 * de um tipo específico
		 */
		public bool contains(string idPaciente, string tipo)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("select * from listinfogeral where Paciente_idPaciente='");
			sb.Append(idPaciente);
			sb.Append("' and InfoGeral_tipo='");
			sb.Append(tipo);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Fechamos a conexão */
			connection.Close();

			return dt.Rows.Count != 0;
		}

		/**
		 * Método que permite associar um novo tipo 
		 * de infogeral a um cliente na base de dados
		 */
		private void putNewType(string idPaciente, string tipo, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into listinfogeral (InfoGeral_tipo,Paciente_idPaciente) values ('");
			sb.Append(tipo);
			sb.Append("','");
			sb.Append(idPaciente);
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite adicionar uma informação 
		 * geral a um paciente na base de dados
		 */
		private void putInfo(string idPaciente, string tipo, string info, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into infogeral (tipo,descricao,idPaciente) values ('");
			sb.Append(tipo);
			sb.Append("','");
			sb.Append(info);
			sb.Append("','");
			sb.Append(idPaciente);
			sb.Append("')");
			MySqlDataAdapter msdaa = new MySqlDataAdapter(sb.ToString(), connection);

			msdaa.Fill(dt);
		}


		/**
		 * Método que adiciona uma nova informação de um 
		 * dado tipo ao paciente cujo id é, do mesmo modo, 
		 * passado por parâmetro do método
		 */
		public void put(string idPaciente, string tipo, string info)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Verificamos se o paciente possui o 
			 * tipo de informação geral fornecido */
			bool exists = this.contains(idPaciente, tipo);

			/* Abrimos a conexão */
			connection.Open();

			/* Se o cliente não possuir nenhuma info 
			 * do tipo especificado criar esse novo 
			 * tipo na base de dados*/
			if (!exists)
				this.putNewType(idPaciente, tipo, connection);

			/* Agora inserimos a nova informação desse mesmo tipo */
			this.putInfo(idPaciente, tipo, info, connection);

			/* Fechamos a conexão */
			connection.Close();
		}

		/**
		 * Método que permite obter todas as informações 
		 * de um dado tipo associadas a um paciente
		 */
		public List<string> get(string idPaciente, string tipo)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			if (!contains(idPaciente, tipo))
				throw new Exception("[Error] Não existe informação geral desse tipo");

			/* Objeto a ser retornado no final 
			 * da execução do método */
			List<string> ret = new List<string>();

			/* Abrimos a conexão */
			connection.Open();

			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("select * from infogeral where tipo='");
			sb.Append(tipo);
			sb.Append("' and idPaciente='");
			sb.Append(idPaciente);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			foreach(DataRow dr in dt.Rows)
			{
				ret.Add(dr.Field<string>("descricao"));
			}

			connection.Close();

			return ret;
		}
	}
}
