using System;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Threading;
using ConsultaJa.Exceptions;
using ConsultaJa.Backend;
using System.Collections.Generic;

namespace ConsultaJa.DataBase
{
	/**
	 * Classe que permite aceder à base de dados
	 * e extrair informações relacionadas
	 * com contas
	 */
	public class ContaDAO
	{
		/**
		 * Variável de instância da classe 
		 * ContaDAO que é retornada quando 
		 * é pedida uma nova instância
		 */
		private static ContaDAO inst = null;

		/**
		 * String utilizada para aceder 
		 * à base de dados
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
		private ContaDAO()
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
		public static ContaDAO getInstance()
		{
			if (ContaDAO.inst == null)
				ContaDAO.inst = new ContaDAO();
			return ContaDAO.inst;
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

			MySqlDataAdapter msda = new MySqlDataAdapter("select * from Conta", connection);

			msda.Fill(dt);

			int ret = dt.Rows.Count;

			/* Fechamos a conexão */
			connection.Close();
			return ret;
		}

		/**
		 * Método que retorna o número de médicos 
		 * inscritos na aplicação
		 */
		public int sizeMedicos()
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			connection.Open();
			DataTable dt = new DataTable();

			MySqlDataAdapter msda = new MySqlDataAdapter("select * from Medico", connection);

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
		public bool contains(string id)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("select * from Conta where idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Fechamos a conexão */
			connection.Close();
			return dt.Rows.Count != 0;
		}

		/**
		 * Método que nos diz se existe alguma conta 
		 * registada com um determinado email
		 */
		public bool containsWithEmail(string email)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("select * from Conta where email='");
			sb.Append(email);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			/* Fechamos a conexão */
			connection.Close();
			return dt.Rows.Count != 0;
		}

		/**
		 * Método que permite remover todas as entradas 
		 * na tabela Contactos que referenciem a conta 
		 * cujo id é passado por parâmetro do método
		 */
		private void removeFromTableContactos(string id, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("delete from Contactos where Conta_idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite remover a entrada na 
		 * tabela medico referente ao id que é passado 
		 * como parâmetro
		 */
		private void removeFromTableMedico(string id, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("delete from Medico where idMedico='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite remover a entrada na 
		 * tabela paciente referente ao id que é passado 
		 * como parâmetro
		 */
		private void removeFromTablePaciente(string id, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("delete from Paciente where idPaciente='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite remover a entrada na 
		 * tabela conta referente ao id que é passado 
		 * como parâmetro
		 */
		private void removeFromTableConta(string id, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("delete from Conta where idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite remover uma conta 
		 * da base de dados fornecendo o seu ID
		 */
		public void remove(string id)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			/* Removemos todas as linhas da tabela 
			 * contacto que referenciem a conta que 
			 * se pretende remover 
			 */
			this.removeFromTableContactos(id, connection);

			/* Se o id corresponder a um médico 
			 * removemos a respetiva entrada da 
			 * tabela medicò 
			 */
			if (id.Contains("M"))
				this.removeFromTableMedico(id, connection);

			/* Se corresponder a um paciente removemos 
			 * a respetiva linha da tabela Paciente 
			 */
			if (id.Contains("P"))
				this.removeFromTablePaciente(id, connection);

			/* Finalmente removemos a respetiva entrada 
			 * da tabela conta à qual se encontra atribuido 
			 * o id que é passado como parâmetro do método
			 */
			this.removeFromTableConta(id, connection);

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
			return sbdata.ToString();
		}

		/**
		 * Método que nos diz se um dado código 
		 * postal já existe na base de dados
		 */
		private Boolean existeCodigoPostal(string codigo, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("select * from codigo_postal where codigo_postal='");
			sb.Append(codigo);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			return dt.Rows.Count != 0;
		}

		/**
		 * Método que permite adicionar um 
		 * código postal à base de dados
		 */
		private void addCodigoPostal(string codigo, string localidade, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into codigo_postal (codigo_postal,localidade) values ('");
			sb.Append(codigo);
			sb.Append("','");
			sb.Append(localidade);
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
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
			if(idConta.Contains("M"))
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
		 * Método privado que permite adicionar 
		 * uma conta apenas na tabela Conta da 
		 * base de dados
		 */
		private void putTabConta(string id, Conta value, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into Conta (idConta,nome,password,email,dataNascimento) values ('");
			sb.Append(id);
			sb.Append("','");
			sb.Append(value.getNome());
			sb.Append("','");
			sb.Append(value.getPassword());
			sb.Append("','");
			sb.Append(value.getEmail());
			sb.Append("','");
			sb.Append(this.criarData(value.getDataNascimento()));
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

		}

		/**
		 * Método que dado um identificador de uma 
		 * conta e o próprio objeto correspondente 
		 * acrescenta à base de dados todos os seus 
		 * contactos
		 */
		private void putTabContactos(string id, Conta value, MySqlConnection connection)
		{
			DataTable dt = new DataTable();

			StringBuilder sb;
			MySqlDataAdapter msda;

			foreach (string contacto in value.getContactos())
			{
				sb = new StringBuilder();
				sb.Append("insert into Contactos (telemovel, Conta_idConta) values ('");
				sb.Append(contacto);
				sb.Append("','");
				sb.Append(id);
				sb.Append("')");

				msda = new MySqlDataAdapter(sb.ToString(),connection);

				msda.Fill(dt);
			}
			
		}

		/**
		 * Método que permite inserir uma nova 
		 * entrada na tabela medico da base de dados
		 */
		private void putTableMedico(string id, Medico value, MySqlConnection connection)
		{
			string cp;
			/* Se não existir o código postal na base 
			 * de dados teremos que o adicionar 
			 */
			if (!this.existeCodigoPostal((cp = value.getCodigo_Postal()), connection))
				this.addCodigoPostal(cp, value.getLocalidade(), connection);

			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into medico (morada,nif,classificacao,numClassificacao,idMedico,codigo_postal,saldo) values ('");
			sb.Append(value.getMorada());
			sb.Append("','");
			sb.Append(value.getNif());
			sb.Append("','");
			sb.Append(value.getClassificacao());
			sb.Append("','");
			sb.Append(value.getNumClassificacoes());
			sb.Append("','");
			sb.Append(id);
			sb.Append("','");
			sb.Append(value.getCodigo_Postal());
			sb.Append("','");
			sb.Append(value.getSaldo());
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite inserir uma nova 
		 * entrada na tabela paciente da base de dados
		 */
		private void putTablePaciente(string id, Paciente value, MySqlConnection connection)
		{
			string cp;
			/* Se não existir o código postal na base 
			 * de dados teremos que o adicionar 
			 */
			if (!this.existeCodigoPostal((cp = value.getCodigo_Postal()), connection))
				this.addCodigoPostal(cp, value.getLocalidade(), connection);

			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into paciente (nif,morada,saldo,idPaciente,codigo_postal) values ('");
			sb.Append(value.getNif());
			sb.Append("','");
			sb.Append(value.getMorada());
			sb.Append("','");
			sb.Append(value.getSaldo());
			sb.Append("','");
			sb.Append(id);
			sb.Append("','");
			sb.Append(value.getCodigo_Postal());
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite adicionar uma nova conta 
		 * à base de dados, tendo fornecido o seu 
		 * identificador e o próprio objeto conta
		 */
		public void put(string id, Conta value)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();

			/* Colocar o novo objeto 
			 * na tabela conta */
			this.putTabConta(id, value, connection);

			/* Colocar os contactos da conta 
			 * na base de dados */
			this.putTabContactos(id, value, connection);

			if (value is Medico)
				this.putTableMedico(id, (Medico)value, connection);

			if (value is Paciente)
				this.putTablePaciente(id, (Paciente)value, connection);

			/* Fechamos a conexão */
			connection.Close();
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

			/* 
			 * Apenas existirá, no máximo 1 linha,
			 * visto que os id's são únicos
			 */
			DataRow dr = dt.Rows[0];
			string localidade = this.getLocalidade(idMedico,connection);

			m = new Medico(dr.Field<string>("idMedico"), email,password, nome, (double)dr.Field<decimal>("classificacao"), 
				dr.Field<int>("numClassificacao"), dataNascimento, dr.Field<string>("nif"), dr.Field<string>("morada"), dr.Field<string>("codigo_postal"),
				localidade);
			
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
			foreach (DataRow dr in dt.Rows)
			{
				p = new Paciente(dr.Field<string>("idPaciente"), email, password, nome, dr.Field<string>("morada"), 
					dr.Field<string>("nif"), dataNascimento, 
					dr.Field<string>("codigo_postal"),localidade);
			}
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
			foreach(DataRow dr in dt.Rows)
			{
				c.addContacto(dr.Field<string>("telemovel"));
			}
		}

		/**
		 * Método que permite obter um objeto 
		 * da classe Conta fornecendo o seu id
		 */
		public Conta get(string id)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			Conta c = null;

			/* Abrir a conexão */
			connection.Open();
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
			foreach(DataRow dr in dt.Rows)
			{
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
						dr.Field<string>("email"), dr.Field<DateTime>("dataNascimento"), connection);
				}
			}

			this.getContactos(id, c, connection);

			connection.Close();
			return c;
		}

		/**
		* Método que permite obter um objeto 
		* da classe Conta fornecendo o seu email
		*/
		public Conta getbyEmail(string email)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			Conta c = null;
			string id = "";

			/* Abrir a conexão */
			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();
			sb.Append("select * from conta where email='");
			sb.Append(email);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			if (dt.Rows.Count == 0)
				throw new MailNaoRegistado("[Error] Id de conta inexistente");

			/* Só teremos um item nesta coleção */
			foreach (DataRow dr in dt.Rows)
			{
				id = dr.Field<string>("idConta");
				/* Estamos perante um médico */
				if (id.Contains("M"))
				{
					c = this.createMedico(id, dr.Field<string>("nome"), dr.Field<string>("password"),
						email, dr.Field<DateTime>("dataNascimento"), connection);
				}
				/* Estamos perante um paciente */
				if (id.Contains("P"))
				{
					c = this.createPaciente(id, dr.Field<string>("nome"), dr.Field<string>("password"),
						email, dr.Field<DateTime>("dataNascimento"), connection);
				}
			}

			this.getContactos(id, c, connection);

			connection.Close();
			return c;
		}

		/**
		 * Método que permite efetuar um carregamento para a ca
		 */
		public void efetuarCarregamento(string id, int montante)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			if (id.Contains("M"))
				sb.Append("update medico set saldo=saldo+");
			else
				sb.Append("update paciente set saldo=saldo+");
			sb.Append(montante);
			if (id.Contains("M"))
				sb.Append(" where idMedico='");
			else
				sb.Append(" where idPaciente='");
			sb.Append(id);
			sb.Append("'");
			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);
			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite atualizar os parâmetros 
		 * relacionados com a avaliação do médico
		 */
		private void atualizarClassificacao(string idMedico, double novaClassif, int nClassif, MySqlConnection connection)
		{
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();
			sb.Append("update medico set classificacao=");
			sb.Append(novaClassif);
			sb.Append(", numClassificacao=");
			sb.Append(nClassif);
			sb.Append(" where idMedico='");
			sb.Append(idMedico);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);
		}

		/**
		 * Método que permite avaliar o médico cujo id 
		 * é passado como parâmetro do método
		 */
		public void avaliarMedico(string id, int classificacaoNew)
		{
			/* Se houver tentativa de avaliar 
			 * um paciente lançamos uma exceção */
			if (id.Contains("P"))
				throw new Exception("[Error] Funcionalidade indisponível para pacientes");

			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			/* Garantimos exclusão mútua para 
			 * atualizar a classificação */
			sb.Append("select classificacao, numClassificacao from medico where id='");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), this.connectionstring);
			msda.Fill(dt);
			/* Vamos buscar os valores. Obtemos a exclusão 
			 * mútua do objeto neste local */
			Monitor.Enter(this);
			try
			{
				double classAtual = dt.Rows[0].Field<double>("classificacao");
				int nClassifs = dt.Rows[0].Field<int>("numClassificacao");
				/* Cálculo da nova classificação média atual */
				double calc = (classAtual * nClassifs + classificacaoNew) / ++nClassifs;
				/* Atualizamos o valor */
				this.atualizarClassificacao(id, calc, nClassifs, connection);
			}
			finally{
				Monitor.Exit(this);
			}
			/* Fechamos a conexão */

			connection.Close();
		}

		/**
		 * Método que altera a password de uma conta 
		 * no registo da conta, cujo id é passado como 
		 * parâmetro do método, que se encontra na 
		 * base de dados
		 */
		public void changePassword(string id, string oldPassword, string newPassword)
		{
			/* Vamos buscar a password existente 
			 * na base de dados */
			string oldcryptedPass = this.get(id).getPassword();
			if (!PasswordHasher.VerificaHash(oldPassword, oldcryptedPass))
				throw new PasswordErrada("Password incorreta");
			MySqlConnection connection = new MySqlConnection(this.connectionstring);
			/* Abrimos a conexão */
			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("update conta set password='");
			sb.Append(newPassword);
			sb.Append("' where idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();

		}

		/**
		 * Método que permite alterar a 
		 * morada de um utilizador
		 */
		public void changeMorada(string id, string novaMorada)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);

			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			if (id.Contains("M"))
				sb.Append("update medico set morada='");
			else 
				sb.Append("update paciente set morada='");
			sb.Append(novaMorada);
			sb.Append("'");

			if (id.Contains("M"))
				sb.Append(" where idMedico='");
			else
				sb.Append(" where idPaciente='");

			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite alterar o código postal 
		 * de um utilizador dado o seu id
		 */
		public void changeCodigoPostal(string id, string novoCP)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);

			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			if (id.Contains("M"))
				sb.Append("update medico set codigo_postal='");
			else
				sb.Append("update paciente set codigo_postal='");
			sb.Append(novoCP);
			sb.Append("'");

			if (id.Contains("M"))
				sb.Append(" where idMedico='");
			else
				sb.Append(" where idPaciente='");

			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite alterar o nome de 
		 * um utilizador dado o seu id único
		 */
		public void changeNome(string id, string nome)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);

			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("update conta set nome='");
			sb.Append(nome);
			sb.Append("' where idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite alterar a data de nascimento 
		 * de um utilizador dado o seu identificador único
		 */
		public void changeDataNascimento(string id, DateTime dataNascimento)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);

			connection.Open();
			DataTable dt = new DataTable();

			StringBuilder sb = new StringBuilder();

			sb.Append("update conta set dataNascimento='");
			sb.Append(this.criarData(dataNascimento));
			sb.Append("' where idConta='");
			sb.Append(id);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite associar um contacto ao 
		 * perfil de um determinado utilizador
		 */
		public void addContacto(string id, string contacto)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);

			connection.Open();

			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("insert into contactos (telemovel,Conta_idConta) values ('");
			sb.Append(contacto);
			sb.Append("','");
			sb.Append(id);
			sb.Append("')");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que permite aceitar um pedido de 
		 * inscrição na aplicação como médico
		 */
		public void aceitaMedico(string idProposto)
		{
			MySqlConnection connection = new MySqlConnection(this.connectionstring);

			connection.Open();
			DataTable dt = new DataTable();
			StringBuilder sb = new StringBuilder();

			sb.Append("update medico set numClassificacao=0 where idMedico='");
			sb.Append(idProposto);
			sb.Append("'");

			MySqlDataAdapter msda = new MySqlDataAdapter(sb.ToString(), connection);

			msda.Fill(dt);

			connection.Close();
		}

		/**
		 * Método que retorna uma lista com o conjunto de 
		 * utilizadores da aplicação que se candidataram a médico
		 */
		public List<Conta> getCandidatos()
		{
			List<Conta> list = new List<Conta>();
			Conta c; Medico m;
			StringBuilder sb = new StringBuilder();
			string id;
			/* Vamos buscar o número de contas */
			int num = this.sizeMedicos();
			int i = 0, j = 0;
			while (i < num)
			{
				sb.Append("M");
				sb.Append(j);
				id = sb.ToString();
				/* Caso exista esse id */
				if (this.contains(id))
				{
					c = this.get(id);
					m = (Medico)c;
					if (!m.aprovado())
					{
						list.Add(m);
					}
					i++;
				}
				j++;
				sb.Clear();
			}
			return list;
		}
	}
}