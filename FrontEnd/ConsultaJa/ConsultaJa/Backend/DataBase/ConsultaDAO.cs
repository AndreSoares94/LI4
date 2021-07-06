using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ConsultaJa.Exceptions;
using ConsultaJa.Backend;
using MySql.Data.MySqlClient;

namespace ConsultaJa.DataBase
{
	/**
	 * Classe que permite aceder à base de dados
	 * e extrair informações relacionadas
	 * com contas
	 */
	public class ConsultaDAO
	{
		/**
		 * Valores que caracterizam o estado
		 * de uma consulta
		 */
		private static readonly int PEDIDO = 3;
		private static readonly int PENDENTE = 1;
		private static readonly int AGENDADA = 2;
		private static readonly int REALIZADA = 0;

		/**
		 * Variável de instância da classe 
		 * ContaDAO que é retornada quando 
		 * é pedida uma nova instância
		 */
		private static ConsultaDAO inst = null;

		/**
		 * String a partir da qual podemos 
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
		private ConsultaDAO()
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
		public static ConsultaDAO getInstance()
		{
			if (ConsultaDAO.inst == null)
				ConsultaDAO.inst = new ConsultaDAO();
			return ConsultaDAO.inst;
		}

		/**
		 * Método que nos diz qual o número de 
		 * contas contidas num objeto da classe 
		 * ContaDAO
		 */
		public int size()
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			connection.Open();
			DataTable dt = new DataTable();

			MySqlDataAdapter msda = new MySqlDataAdapter("select * from Consulta", connection);

			msda.Fill(dt);

			int ret = dt.Rows.Count;

			/* Fechamos a conexão */
			connection.Close();
			return ret;
		}

		/**
		 * Método que nos diz se uma dada 
		 * chave existe no mapeamento
		 */
		public bool contains(int id)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("select * from Consulta where idConsulta=");
			sb.Append(id);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Fechamos a conexão */
			connection.Close();
			return dt.Rows.Count != 0;
		}

		/**
		 * Método que permite remover uma consulta 
		 * da base de dados fornecendo o seu ID
		 */
		public void remove(int id)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("delete from Consulta where idConsulta=");
			sb.Append(id);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Fechamos a conexão */
			connection.Close();
		}

		/**
		 * Método que permite converter um objeto da 
		 * classe DateTime para um string que possa 
		 * ser introduzida numa base de dados MySql
		 */
		private string criarData(DateTime data)
		{
			StringBuilder sbdata = new StringBuilder();
			sbdata.Append(data.Year);
			sbdata.Append("-");
			sbdata.Append(data.Month);
			sbdata.Append("-");
			sbdata.Append(data.Day);
			sbdata.Append(" ");
			sbdata.Append(data.Hour);
			sbdata.Append(":");
			sbdata.Append(data.Minute);
			sbdata.Append(":");
			sbdata.Append(data.Second);
			return sbdata.ToString();
		}

		/**
		 * Método que adiciona um objeto da classe 
		 * consulta à base de dados. É retornado o 
		 * identificador que ficou associado à consulta
		 */
		private void putTableConsulta(Consulta value, MySqlConnection connection)
		{
			int ret=0;

			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into Consulta (data_hora,localidade,morada,estado,observaçoes,idPaciente,idMedico) values ('");
			sb.Append(this.criarData(value.getData_Hora()));
			sb.Append("','");
			sb.Append(value.getLocalidade());
			sb.Append("','");
			sb.Append(value.getMorada());
			sb.Append("','");
			sb.Append(value.getEstado());
			sb.Append("','");
			if (value.getObservacoes() == null)
				sb.Append("Sem observações");
			else
				sb.Append(value.getObservacoes());
			sb.Append("','");
			sb.Append(value.getPaciente().getID());

			if (value.getMedico() != null)
			{
				sb.Append("','");
				sb.Append(value.getMedico().getID());
				sb.Append("')");
			}
			else
				sb.Append("',null)");
			

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite adicionar uma nova consulta
		 * à base de dados, tendo fornecido o seu 
		 * identificador e o próprio objeto consulta
		 */
		public void put(Consulta value)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			/* Colocar a informação
			 * na tabela consulta */
			this.putTableConsulta(value, connection);

			/* Fechamos a conexão */
			connection.Close();
		}

		/**
		 * Método que dado um código postal
		 * retorna a localidade a si atribuida
		 */
		private string getLocalidade(string idConta, MySqlConnection connection)
		{
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			/* Se o id corresponder a um médico */
			if (idConta.Contains("M"))
				sb.Append("select localidade from medico c, codigo_postal cp where c.idMedico='");
			/* Caso o id corresponda a um paciente */
			else
				sb.Append("select localidade from paciente c, codigo_postal cp where c.idPaciente='");

			sb.Append(idConta);
			sb.Append("' and cp.codigo_postal=c.codigo_postal");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Apenas haverá uma linha que 
			 * corresponda a esta pesquisa */
			return dt.Rows[0].Field<string>("localidade");
		}

		/**
		 * Método que dado os dados encontrados apos 
		 * consultar a tabela conta, constroi um 
		 * objeto da classe Medico
		 */
		private Medico createMedico(string idMedico, string nome, string password,
			string email, DateTime dataNascimento, MySqlConnection connection)
		{
			Medico m = null;

			StringBuilder sb = new StringBuilder();

			DataTable dt = new DataTable();

			sb.Append("select * from Medico where idMedico='");
			sb.Append(idMedico);
			sb.Append("'");
			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			string localidade = this.getLocalidade(idMedico, connection);
			/* 
			 * Apenas existirá, no máximo 1 linha,
			 * visto que os id's são únicos
			 */
			DataRow dr = dt.Rows[0];
			m = new Medico(dr.Field<string>("idMedico"), email, password, nome, (double)dr.Field<decimal>("classificacao"),
				dr.Field<int>("numClassificacao"), dataNascimento, dr.Field<string>("nif"), dr.Field<string>("morada"), 
				dr.Field<String>("codigo_postal"),localidade);

			return m;
		}

		/**
		 * Método que dado os dados encontrados apos 
		 * consultar a tabela conta, constroi um 
		 * objeto da classe Paciente
		 */
		private Paciente createPaciente(string idPaciente, string nome, string password,
			string email, DateTime dataNascimento, MySqlConnection connection)
		{
			Paciente p = null;

			StringBuilder sb = new StringBuilder();

			DataTable dt = new DataTable();

			sb.Append("select * from Paciente where idPaciente='");
			sb.Append(idPaciente);
			sb.Append("'");
			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Vamos buscar a localidade do paciente em questão */
			string localidade = this.getLocalidade(idPaciente, connection);

			/* 
			 * Apenas existirá, no máximo 1 linha,
			 * visto que os id's são únicos
			 */
			DataRow dr = dt.Rows[0];
			p = new Paciente(dr.Field<string>("idPaciente"), email, password, nome, dr.Field<string>("morada"),
				dr.Field<string>("nif"), dataNascimento,
				dr.Field<string>("codigo_postal"),localidade);

			return p;
		}

		/**
		 * Método que carrega os contactos associados 
		 * à conta na base de dados para a respetiva 
		 * conta
		 */
		private void getContactos(string id, Conta c, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("select * from contactos where Conta_idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* 
			 * Cada contacto lido é adicionado 
			 * à respetiva conta
			 */
			foreach (DataRow dr in dt.Rows)
			{
				c.addContacto(dr.Field<string>("telemovel"));
			}
		}

		/**
		 * Método que permite obter um objeto 
		 * da classe Conta fornecendo o seu id
		 */
		private Conta getConta(string id, MySqlConnection connection)
		{
			Conta c = null;
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("select * from conta where idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			if (dt.Rows.Count == 0)
				throw new MailNaoRegistado("[Error] Id de conta inexistente");

			/* Só teremos um item nesta coleção */
			DataRow dr = dt.Rows[0];
			/* Estamos perante um médico */
			if (id.Contains("M"))
			{
				c = this.createMedico(id, dr.Field<string>("nome"), dr.Field<string>("password"),
					dr.Field<string>("email"), dr.Field<DateTime>("dataNascimento"), connection);
			}
			/* Estamos perante um paciente */
			if (id.Contains("P"))
			{
				c = this.createPaciente(id, dr.Field<string>("nome"), dr.Field<string>("password"),
					dr.Field<string>("email"), dr.Field<DateTime>("dataNascimento"),connection);
			}
			

			this.getContactos(id, c, connection);

			return c;
		}

		/**
		 * Método que permite obter uma consulta 
		 * acedendo à tabela consulta existente 
		 * na base de dados
		 */
		private Consulta getFromTableConsulta(int id, MySqlConnection connection)
		{
			Consulta ret = null;
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("select * from Consulta where idConsulta=");
			sb.Append(id);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			if (dt.Rows.Count == 0)
				throw new MailNaoRegistado("[Error] Id de consulta não está atribuido");

			/* Só teremos um item nesta coleção */
			DataRow dr = dt.Rows[0];
			Medico m;
			if (dr.Field<string>("idMedico") != null)
				m = (Medico)this.getConta(dr.Field<string>("idMedico"), connection);
			else
				m = null;
			Paciente p = (Paciente)this.getConta(dr.Field<string>("idPaciente"), connection);
			DateTime dta = dr.Field<DateTime>("data_hora");
			string obs = dr.Field<string>("observaçoes");
			if (obs.Equals("Sem observações"))
				obs = null;

			ret = new Consulta(dr.Field<int>("idConsulta"),p, m, dr.Field<int>("preco"), dr.Field<string>("localidade"), dr.Field<string>("morada"), obs,
				dta.Year, dta.Month, dta.Day, dta.Hour, dta.Minute, dta.Second, Int32.Parse(dr.Field<string>("estado")));
			return ret;
		}

		/**
		 * Método que permite obter um objeto da 
		 * classe Consulta proveniente do seu 
		 * acesso na base de dados
		 */
		public Consulta get(int id)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			Consulta c = this.getFromTableConsulta(id, connection);

			/* Fechamos a conexão */
			connection.Close();

			return c;
		}

		/**
		 * Método que vai buscar todas as consultas 
		 * de um determinado tipo, cujo id de médico 
		 * é passado como parâmetro do método
		 */
		private List<Consulta> getAsMedicoType(string idMedico, int identifier)
		{
			/* Objeto a ser retornado */
			List<Consulta> list = new List<Consulta>();
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão para a base de dados */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("select idConsulta from Consulta where idMedico='");
			sb.Append(idMedico);
			sb.Append("' and estado=");
			sb.Append(identifier);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Fechamos a conexão */
			connection.Close();

			/* Para cada entrada da tabela consulta que 
			 * resulte da pesquisa, acrescentamos um novo 
			 * objeto Consulta à lista a retornar*/
			 foreach(DataRow dr in dt.Rows)
			{
				list.Add(this.get(dr.Field<int>("idConsulta")));
			}
			return list;
		}

		/**
		 * Método que vai buscar todas as consultas 
		 * de um determinado tipo, cujo id de paciente
		 * é passado como parâmetro do método
		 */
		private List<Consulta> getAsPacienteType(string idPaciente, int identifier)
		{
			/* Objeto a ser retornado */
			List<Consulta> list = new List<Consulta>();
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão para a base de dados */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("select idConsulta from Consulta where idPaciente='");
			sb.Append(idPaciente);
			sb.Append("' and estado=");
			sb.Append(identifier);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Para cada entrada da tabela consulta que 
			 * resulte da pesquisa, acrescentamos um novo 
			 * objeto Consulta à lista a retornar*/
			foreach (DataRow dr in dt.Rows)
			{
				list.Add(this.get(dr.Field<int>("idConsulta")));
			}
			return list;
		}

		/**
		 * Método que retorna as consultas agendadas 
		 * para um médico
		 */
		public List<Consulta> getAsMedicoAgendadas(string idMedico)
		{
			return this.getAsMedicoType(idMedico, AGENDADA);
		}

		/**
		 * Método que retorna as consultas realizadas
		 * de um médico
		 */
		public List<Consulta> getAsMedicoHistorico(string idMedico)
		{
			return this.getAsMedicoType(idMedico, REALIZADA);
		}

		/**
		 * Método que retorna uma lista com as consultas
		 * agendadas para um dado paciente
		 */
		public List<Consulta> getAsPacienteAgendadas(string idPaciente)
		{
			return this.getAsPacienteType(idPaciente, AGENDADA);
		}

		/**
		 * Método que retorna uma lista com as consultas
		 * realizadas de um dado paciente
		 */
		public List<Consulta> getAsPacienteHistorico(string idPaciente)
		{
			return this.getAsPacienteType(idPaciente, REALIZADA);
		}

		/**
		 * Método que retorna uma lista com as consultas
		 * à espera de confirmação de um dado paciente
		 */
		public List<Consulta> getAsPacientePendentes(string idPaciente)
		{
			return this.getAsPacienteType(idPaciente, PENDENTE);
		}

		/**
		 * Método que retorna todas os pedidos de 
		 * consulta feitos por parte de pacientes
		 */
		//public List<Consulta> getPedidos()
		//{
		//	List<Consulta> ret = new List<Consulta>();
		//	Consulta c;
		//	/* Vamos buscar o número de consultas */
		//	int num = this.size();
		//	int i = 0, j = 0;
		//	while (i < num)
		//	{
		//		/* Caso exista esse id */
		//		if (this.contains(j))
		//		{
		//			c = this.get(j);
		//			if (c.isPedido())
		//			{
		//				ret.Add(c);
		//			}
		//			i++;
		//		}
		//		j++;
		//	}
		//	return ret;
		//}

		/**
		 * Método que permite obter uma determinada localidade 
		 * tendo fornecido previamente o seu código postal
		 */
		private string localidade(string cod_postal)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão para a base de dados */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("select localidade from Codigo_Postal where codigo_postal='");
			sb.Append(cod_postal);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();

			return dt.Rows[0].Field<string>("localidade");

		}

		/**
		 * Método que retorna todas os pedidos de 
		 * consulta feitos por parte de pacientes
		 */
		public List<Consulta> getPedidos(string cod_postal)
		{
			string distritoMedico = localidade(cod_postal);
			string distritoConsulta;
			List<Consulta> ret = new List<Consulta>();
			Consulta c;
			/* Vamos buscar o número de consultas */
			int num = this.size();
			int i = 0, j = 0;
			while (i < num)
			{
				/* Caso exista esse id */
				if (this.contains(j))
				{
					c = this.get(j);
					if (c.isPedido())
					{
						distritoConsulta = localidade(c.getLocalidade());
						//Console.WriteLine("Localidade Medico: " + distritoMedico);
						//Console.WriteLine("Localidade Consulta: " + distritoConsulta);
						if (distritoConsulta.Equals(distritoMedico))
							ret.Add(c);
					}
					i++;
				}
				j++;
			}
			return ret;
		}

		/**
		 * Método que permite marcar uma consulta 
		 * agendada como realizada
		 */
		public void marcarRealizada(int idConsulta)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("update consulta set estado=");
			sb.Append(REALIZADA);
			sb.Append(" where idConsulta=");
			sb.Append(idConsulta);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que retorna a data para a qual 
		 * ficou marcada/foi realizada a consulta
		 */
		private DateTime getDateOfConsulta(int idConsulta)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("select data_hora from Consulta where idConsulta=");
			sb.Append(idConsulta);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();

			/* Apenas existirá uma linha após a pesquisa */
			return dt.Rows[0].Field<DateTime>("data_hora");
		}

		/**
		 * Método que valida se é possível ou não 
		 * aceitar uma proposta para consulta
		 */
		private bool validaAceitacao(int idConsulta)
		{
			DateTime consulta, now;
			bool ret = (consulta = this.getDateOfConsulta(idConsulta)).CompareTo((now = DateTime.Now)) > 0;
			//bool minumum2Days = now.Year >= consulta.Year ||
			//			(now.Month >= consulta.Month && now.Year == consulta.Year )||
			//			(now.Day >= consulta.Day + 2 && now.Month == consulta.Month && now.Year == consulta.Year);
			return ret;

		}

		/**
		 * Método que mediante um pedido de consulta 
		 * permite fazer uma proposta ao paciente em 
		 * questão
		 */
		public void submeterProposta(int idConsulta, string idMedico, int precoDB)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("update consulta set estado=");
			sb.Append(PENDENTE);
			sb.Append(", idMedico='");
			sb.Append(idMedico);
			sb.Append("', preco=");
			sb.Append(precoDB);
			sb.Append(" where idConsulta=");
			sb.Append(idConsulta);
			
			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite aceitar uma 
		 * proposta de consulta
		 */
		public void aceitarProposta(int idConsulta)
		{
			/* Se a data da consulta for inferior à data atual 
			 * não é possível aceitar a consulta */
			if (!this.validaAceitacao(idConsulta))
			{
				this.remove(idConsulta);
				throw new Exception("[Error] Impossível aceitar consulta");
			}
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Close();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("update consulta set estado=");
			sb.Append(AGENDADA);
			sb.Append(" where idConsulta=");
			sb.Append(idConsulta);
			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite rejeitar uma 
		 * proposta de consulta
		 */
		public void rejeitarProposta(int idConsulta)
		{
			/* Se a data da consulta for inferior à data atual 
			 * não é possível rejeitar a consulta */
			if (!this.validaAceitacao(idConsulta))
			{
				this.remove(idConsulta);
				throw new Exception("[Error] Impossível aceitar consulta");
			}
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Close();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("update consulta set estado=");
			sb.Append(PEDIDO);
			sb.Append(" where idConsulta=");
			sb.Append(idConsulta);
			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}


		/**
		 * Método que retorna o número de prescrições 
		 * associadas a uma determinada consulta
		 */
		private int numPrescricoes(int idConsulta)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("select * from Prescricao where idConsulta=");
			sb.Append(idConsulta);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			int ret = dt.Rows.Count;

			/* Fechamos a conexão */
			connection.Close();
			return ret;
		}

		/**
		 * Método que permite anexar um determinado 
		 * ficheiro pdf a uma consulta na base de dados
		 */
		public void addPrescricao(int idConsulta, string nomeFarmaco, decimal quantidade, string posologia)
		{
			int how = this.numPrescricoes(idConsulta);

			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("insert into prescricao (idPrescricao, idConsulta, nomeFarmaco, quantidade, posologia) values (");
			sb.Append(how);
			sb.Append(",");
			sb.Append(idConsulta);
			sb.Append(",'");
			sb.Append(nomeFarmaco);
			sb.Append("',");
			sb.Append(quantidade);
			sb.Append(",'");
			sb.Append(posologia);
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite obter o pdf anexado a uma 
		 * dada consulta e gerá-lo para ser visualizado
		 */
		public Receita getReceita(int idConsulta)
		{
			List<Prescricao> list = new List<Prescricao>();

			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("select * from Prescricao where idConsulta=");
			sb.Append(idConsulta);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();

			foreach (DataRow dr in dt.Rows)
			{
				Prescricao nova = new Prescricao(dr.Field<int>("idPrescricao"), dr.Field<int>("idConsulta"), dr.Field<string>("nomeFarmaco"),
					dr.Field<decimal>("quantidade"), dr.Field<string>("posologia"));

				list.Add(nova);
			}

			Consulta c = this.get(idConsulta);
			Paciente p = c.getPaciente();
			Medico m = c.getMedico();

			return new Receita(list, idConsulta, p.getNome(), p.getContactos(), p.getNif(), m.getNome(), m.getContactos(), m.getNif());
		}

		/**
		 * Método que nos indica se uma dada consulta 
		 * possui alguma receita associada
		 */
		public bool containsPrescricao(int idConsulta)
		{
			bool ret = true;

			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("select * from Prescricao where idConsulta=");
			sb.Append(idConsulta);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			if (dt.Rows.Count == 0)
				ret = false;

			connection.Close();

			return ret;
		}

		/**
		 * Método que permite adicionar 
		 * observações a uma consulta
		 */
		public void addObservavoes(int idConsulta, string observacoes)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("update consulta set observaçoes='");
			if (observacoes.Equals(""))
				sb.Append("Sem observações");
			else
				sb.Append(observacoes);
			sb.Append("' where idConsulta=");
			sb.Append(idConsulta);

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}
	}
}
