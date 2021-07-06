using System;
using System.Collections.Generic;
using System.Text;
using ConsultaJaDB;

namespace ConsultaJa
{
	public class ConsultaJaModel
	{
		/**
		 * Variável de leitura que guarda o nome do
		 * parâmetro que guarda o preço da consulta na base de dados
		 */
		private static readonly string __Const_preco = "preco";

		/**
		 * Coleção na qual guardamos as contas que 
		 * se inscrevem na plataforma. Essa coleção 
		 * tem por base uma base de dados do tipo Relacional
		 */
		private ContaDAO contas;

		/**
		 * Coleção na qual guardamos todos os registos 
		 * relacionados com consultas que se encontram na plataforma.
		 * Essa coleção tem por base uma base de dados do tipo Relacional
		 */
		private ConsultaDAO consultas;

		/**
		 * Coleção na qual guardamos alguns dos parâmetros 
		 * necessários para o funcionamento normal da 
		 * aplicação, nomeadamente o número de médicos 
		 * e pacientes registados na aplicação, o preço 
		 * das consultas, a chave de acesso a direitos 
		 * administrativos, entre outros
		 */
		private ConfigsDAO parametros;

		/**
		 * Variável que guarda os registos de informações 
		 * gerais de pacientes na base de dados
		 */
		private InfoGeralDAO info;

		/**
		 * Construtor para objetos da classe ConsultaJaModel, 
		 * classe essa que representa a classe principal 
		 * da aplicação
		 */
		public ConsultaJaModel()
		{
			this.contas = ContaDAO.getInstance();
			this.consultas = ConsultaDAO.getInstance();
			this.parametros = ConfigsDAO.getInstance();
			this.info = InfoGeralDAO.getInstance();
		}

		/**
		 * Método que retorna um objeto da classe conta 
		 * registado na base de dados, sendo fornecido o 
		 * seu id como parâmetro do mesmo método
		 */
		public Conta getConta(string idConta)
		{
			return this.contas.get(idConta);
		}

		/**
		 * Método que permite a inscrição de 
		 * um novo paciente na aplicação
		 */
		public string novoPaciente(string email, string password, string nome, DateTime dataNascimento, 
			string morada, string nif, string codigo_postal, List<string> contactos, string localidade)
		{
			/* Vamos buscar o próximo idPaciente que será atribuido 
			 * ao novo paciente que está a ser registado */
			int id = parametros.getAndIncrement("pacientes");
			string idPaciente = "P" + id;
			Paciente p = new Paciente(idPaciente, email, password, nome, morada, nif,
				dataNascimento, codigo_postal, localidade);
			/* Adicionamos cada contacto ao perfil do paciente */
			foreach(string contacto in contactos)
			{
				p.addContacto(contacto);
			}
			contas.put(idPaciente, p);
			return idPaciente;
		}

		/**
		 * Método que permite fazer login na aplicação. Throws Exception
		 */
		public Conta login(string id, string email, string password)
		{
			if (!this.contas.contains(id))
				throw new MailNaoRegistado("[Error] id '" + id + "' inválido");

			Conta c = this.contas.get(id);

			if (!c.getEmail().Equals(email))
				throw new MailNaoRegistado("[Error] email '" + email + "' não corresponde ao seu id");

			if (!c.getPassword().Equals(password))
				throw new PasswordErrada("[Error] password errada");

			/* Caso a conta seja de médico mas ainda não tenha sido 
			 * aprovada pelo administrador também lançamos exceção */
			if (c.getID().Contains("M"))
			{
				Medico m = (Medico)c;
				if (!m.aprovado())
					throw new MailNaoRegistado("O seu pedido de inscrição na aplicação " +
						"como médico ainda não foi aceite pelo administrador. Por favor, tente novamente mais tarde");
			}

			return c;
		}

		/**
		 * Método que permite avaliar um médico 
		 * registado na aplicação, fornecendo o seu ID
		 */
		public void avaliarMedico(string idMedico, int classificacao)
		{
			/* Avaliamos o médico */
			this.contas.avaliarMedico(idMedico, classificacao);
		}

		/**
		 * Método que permite aceder ao histórico 
		 * de consultas de um médico ou paciente
		 */
		public List<Consulta> getHistorico(string id)
		{
			if (!contas.contains(id))
				throw new MailNaoRegistado("[Error] Conta inexistente");

			return contas.get(id).getHistorico();
		}

		/**
		 * Método que retorna uma estrutura de dados 
		 * contendo informação acerca de todas as 
		 * consultas agendadas de um médico ou paciente
		 */
		public List<Consulta> getConsultasAgendadas(string id)
		{
			if (!contas.contains(id))
				throw new MailNaoRegistado("[Error] Conta inexistente");

			return contas.get(id).getConsultasAgendadas();
		}

		/**
		 * Método que permite alterar o 
		 * preço das consultas
		 */
		public void mudarPreco(int novoPreco)
		{
			/* Alteramos o valor na base de dados */
			this.parametros.setValue(__Const_preco, novoPreco);
		}

		/**
		 * Método que permite a um utilizador 
		 * da aplicação alterar a sua password
		 */
		public void alterarPassword(string id, string password, string novaPass)
		{
			Conta c = this.contas.get(id);

			// TERMINAR
		}

		/**
		 * Método que permite a um paciente solicitar 
		 * uma consulta na aplicação ConsultaJa
		 */
		public void solicitarConsulta(string idPaciente, int ano, int mes, int dia, int hora, int minuto)
		{
			Paciente p;
			(p = (Paciente)this.contas.get(idPaciente)).addPropostaConsulta(p.getCodigo_Postal(), 
				ano, mes, dia, hora, minuto, 0);
		}

		/**
		 * Método que permite a um médico propor 
		 * uma consulta a um paciente, tendo este 
		 * previamente criado uma solicitação de 
		 * consulta
		 */
		public void proporConsulta(string idMedico, int idConsulta)
		{
			Medico m = (Medico)this.contas.get(idMedico);
			/* Vamos a base de dados buscar o preço por 
			 * consulta atualmente em vigor */
			int preco = this.parametros.get(__Const_preco);
			m.submeterProposta(idConsulta, preco);
		}

		/**
		 * Método que permite a um cliente aceitar uma 
		 * proposta de consulta feita por um médico
		 */
		public void aceitaConsulta(int idConsulta)
		{
			this.consultas.aceitarProposta(idConsulta);
		}

		/**
		 * Método que permite aceder a pedidos enviados 
		 * por cliente para marcação de consultas
		 */
		public List<Consulta> getPedidos(string cod_postal)
		{
			return consultas.getPedidos(cod_postal);
		}

		/**
		 * Método que permite desmarcar uma 
		 * consulta fornecendo o id do utente 
		 * autenticado e o id da consulta a 
		 * ser desmarcada
		 */
		public void desmarcaConsulta(string id, int idConsulta)
		{
			/* Em primeiro lugar vamos testar se o 
			 * pedido de desmarcação foi feito 
			 * por um médico */
			 // FALTA GERIR A DESMARCAÇÃO DE CONSULTAS
		}

		/**
		 * Método que permite efetuar carregamento de 
		 * um certo montante na carteira digital do 
		 * paciente cujo id é enviado como parâmetro 
		 * do método
		 */
		public void efetuaCarregamento(string idPaciente, int montante)
		{
			if (!this.contas.contains(idPaciente))
				throw new MailNaoRegistado("[Error] Id de usuário não se encontra atribuido");

			/* Se a conta existir efetuamos o carregamento */
			this.contas.efetuarCarregamento(idPaciente, montante);
		}

		/**
		 * Método que permite fazer um novo
		 * pedido de inscrição de um médico
		 */
		public void fazerPedidoInscricao(string email, string password, string nome, DateTime dataNascimento, string nif, string morada, string codigo_postal, string localidade)
		{
			/* Vamos buscar um id para o 
			 * candidato a médico */
			int num = parametros.getAndIncrement("medicos");
			string id = "M" + num;
			/* Criamos um médico com o número de classificações 
			 * negativo o que significa que ainda não
			 * foi aceite como médico na aplicação */
			Medico m = new Medico(id, email, password, nome, -1, -1, dataNascimento, 
				nif, morada, codigo_postal, localidade);
			/* Registamos a conta na 
			 * base de dados */
			contas.put(id,m);
		}

		/**
		 * Método que permite aceitar ou rejeitar um 
		 * pedido de inscrição feito por parte de um médico
		 */
		public string trataPedido(string idProvisorio, Boolean action)
		{
			string ret = null;
			/* Caso o pedido não seja aceite eliminamos 
			 * o registo da base de dados por completo */
			if(!action)
				contas.remove(idProvisorio);
			/* Caso o pedido seja aceite e o 
			 * id se refira a um médico */
			else if (action && idProvisorio.Contains("M"))
			{
				Conta c = contas.get(idProvisorio);
				/* Sabemos que c é um médico */
				Medico novo = (Medico)c;
				/* Aceitamos o médico */
				this.contas.aceitaMedico(idProvisorio);
			}
			/* Caso contrário lançamos exceção */
			else
				throw new MailNaoRegistado("[Fatal Error] Ocorreu um erro ao tratar o pedido de médico.");
			/* Se tudo correr bem retornamos o id 
			 * do novo médico */
			return ret;
		}

		/**
		 * Método que permite registar uma dada informação geral 
		 * associada a um paciente na base de dados
		 */
		public void registarInfoGeralPaciente(string idPaciente, string tipo, string info)
		{
			this.info.put(idPaciente, tipo, info);
		}

		/**
		 * Método que permite associar um novo contacto 
		 * à conta cujo id é passado por parâmetro
		 */
		public void addNovoContacto(string id, string contacto)
		{
			this.contas.addContacto(id, contacto);
		}

		/**
		 * Método que permite aceder ao codigo de 
		 * acesso do administrador
		 */
		public string getAdminCode()
		{
			return this.parametros.get("admin.code").ToString();
		}

		/**
		 * Método que permite aceder aos parâmetros da base de 
		 * dados e retornar o numero de médicos/pedidos de médico 
		 * no sistema
		 */
		public string getMedicos()
		{
			return this.parametros.get("medicos").ToString();
		}

		/**
		 * Método que permite aceder aos parâmetros da base de 
		 * dados e retornar o numero de médicos/pedidos de médico 
		 * no sistema
		 */
		public string getPacientes()
		{
			return this.parametros.get("pacientes").ToString();
		}

		/**
		 * Método que permite aceder aos parâmetros da base de 
		 * dados e retornar o numero de médicos/pedidos de médico 
		 * no sistema
		 */
		public string getPreco()
		{
			return this.parametros.get("preco").ToString();
		}

		/**
		 * Método que permite obter uma lista de contas 
		 * que pertencem a médicos ainda não aceites pelo administrador
		 */
		public List<Conta> getCandidatos()
		{
			return this.contas.getCandidatos();
		}

		/**
		 * Método que permite associar um determinado 
		 * ficheiro .pdf com as receitas da consulta 
		 * a uma determinada consulta
		 */
		public void addPrescricao(int idConsulta, string nomeFarmaco, decimal quantidade, string posologia)
		{
			this.consultas.addPrescricao(idConsulta, nomeFarmaco, quantidade, posologia);
        }

		/**
		 * Método que permite obter o ficheiro .pdf com 
		 * as receitas para uma determinada consultaa
		 */
		public Receita getReceita(int idConsulta)
        {
			return this.consultas.getReceita(idConsulta);
        }

		/**
		 * Método que nos permite saber se uma dada consulta 
		 * possui uma receita a si associada
		 */
		public bool consultaContainsReceita(int idConsulta)
        {
			return this.consultas.constainsPrescricao(idConsulta);
        }

		/**
		 * Método que permite adicionar um conjunto de observações 
		 * a uma determinada consulta cujo id se encontra especificado 
		 * como parâmetro do método
		 */
		public void addObsToConsulta(int idConsulta, string obs)
        {
			this.consultas.addObservavoes(idConsulta,obs);
        }

		/**
		 * Método que permite marcar uma consulta 
		 * agendada como realizada
		 */
		public void marcarRealizada(int idConsulta)
		{
			this.consultas.marcarRealizada(idConsulta);
		}
}