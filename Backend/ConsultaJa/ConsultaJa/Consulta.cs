using System;
using System.Text;

namespace ConsultaJa
{
	public class Consulta
	{
		/**
		 * Valores que caracterizam o estado
		 * de uma consulta
		 */
		public static readonly int PEDIDO = 3;
		public static readonly int PENDENTE = 1;
		public static readonly int AGENDADA = 2;
		public static readonly int REALIZADA = 0;

		/**
		 * Variável que identifica univocamente 
		 * uma consulta
		 */
		private int id;

		/**
		 * Paciente ao qual a consulta 
		 * é administrada
		 */
		private Paciente p;

		/**
		 * Médico responsável por administrar 
		 * a consulta
		 */
		private Medico m;

		/**
		 * Momento da realização da consulta
		 */
		private DateTime data_hora;

		/**
		 * Variável que guarda a localidade onde 
		 * será/foi realizada a consulta
		 */
		private string localidade;

		/**
		 * Variável que guarda o preço ao qual 
		 * cada consulta foi/será administrada
		 */
		private int precoUni;

		/**
		 * Variável que guarda a morada onde 
		 * será/foi administrada a consulta
		 */
		private string morada;

		/**
		 * Variável que define o estado 
		 * da consulta
		 */
		private int estado;

		/**
		 * Variável que contém algumas observações 
		 * feitas após a administração da consulta
		 */
		private string observacoes;

		/**
		 * Variável que permite anexar um ficheiro pdf 
		 * através de um array de bytes a uma consulta
		 */
		private byte[] pdf;

		/**
		 * Construtor para objetos da classe Consulta
		 */
		public Consulta(int id, Paciente p, Medico m, string localidade, string morada, string observacoes,
			int ano, int mes, int dia, int hora, int min, int sec, int estado)
		{
			this.id = id;
			this.m = m;
			this.p = p;
			this.localidade = localidade;
			this.precoUni = -1;
			this.morada = morada;
			this.estado = PEDIDO;
			this.observacoes = observacoes;
			this.data_hora = new DateTime(ano, mes, dia, hora, min, sec);
			this.estado = estado;
			this.pdf = null;
		}

		/**
		 * Método que retorna o id da consulta 
		 * à qual é enviado o método
		 */
		public int getID()
		{
			return this.id;
		}

		/**
		 * Método que retorna o objeto 
		 * paciente envolvido na consulta
		 */
		public Paciente getPaciente()
		{
			return this.p;
		}

		/**
		 * Método que retorna o objeto 
		 * medico envolvido na consulta
		 */
		public Medico getMedico()
		{
			return this.m;
		}

		/**
		 * Método que retorna a data 
		 * e hora da consulta
		 */
		public DateTime getData_Hora()
		{
			return this.data_hora;
		}

		/**
		 * Método que retorna a localidade 
		 * onde será/foi realizada a consulta
		 */
		public string getLocalidade()
		{
			return this.localidade;
		}

		/**
		 * Método que retorna a morada onde 
		 * será/foi realizada a consulta
		 */
		public string getMorada()
		{
			return this.morada;
		}

		/**
		 * Método que retorna um inteiro que
		 * identifica o estado atual do objeto
		 * da classe consulta ao qual é enviado
		 * o método
		 */
		public int getEstado()
		{
			return this.estado;
		}

		/**
		 * Método que retorna o preço ao qual
		 * a consulta foi administrada
		 */
		public int getPrecoUni()
		{
			return this.precoUni;
		}

		/**
		 * Método que retorna as observações 
		 * feitas à à qual é enviado o método
		 */
		public string getObservacoes()
		{
			return this.observacoes;
		}

		/**
		 * Método que permite marcar uma 
		 * consulta como agendada
		 */
		public void agendar()
		{
			this.estado = AGENDADA;
		}

		/**
		 * Método que permite marcar uma 
		 * consulta como pendente
		 */
		public void pendente() 
		{
			this.estado = PENDENTE;
		}

		/**
		 * Método que permite marcar uma 
		 * consulta como realizada
		 */
		public void realizar()
		{
			this.estado = REALIZADA;
		}

		/**
		 * Método que permite desmarcar uma consulta
		 */
		public void desmarcar()
		{
			/* Eliminamos a consulta quer para 
			 * o paciente quer para o médico */
			//this.m.desmarcarConsulta(this.id);
			this.p.desmarcarConsulta(this.id);
		}

		/**
		 * Método que permite associar um médico a uma 
		 * consulta, visto que no princípio tal não é feito, 
		 * tendo em conta que no princípio temos apenas um 
		 * pedido de consulta feito por parte de um cliente
		 */
		public void associaMedico(Medico m)
		{
			this.m = m;
		}

		/**
		 * Método que permite associar um id a uma 
		 * consulta aquando do seu registo no sistema
		 */
		public void setID(int id)
		{
			this.id = id;
		}

		/**
		 * Método que permite atribui um valor à variável 
		 * médico de um objeto da classe `Consulta ao qual 
		 * é enviado o método
		 */
		public void setMedico(Medico m)
		{
			this.m = m;
		}

		/**
		 * Método que permite colocar um valor
		 * no campo pdf de uma consulta
		 */
		public void setPdf(byte[] pdf)
        {
			this.pdf = pdf;
        }

		/**
		 * Método que permite aceder ao 
		 * pdf de uma dada consulta
		 */
		public byte[] getPdf()
        {
			return this.pdf;
        }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Id: ");
			sb.Append(this.id);
			sb.Append("; idPaciente: ");
			sb.Append(this.p.getID());
			sb.Append("; idMedico: ");
			if (m != null)
				sb.Append(this.m.getID());
			else
				sb.Append("*noDoctor*");
			sb.Append("; Data: ");
			sb.Append(this.data_hora);
			sb.Append("; Status: ");
			sb.Append(this.getEstado());

			return sb.ToString();
		}

		/**
		 * Método que retorna true se a 
		 * consulta ainda só é um pedido
		 */
		public Boolean isPedido()
		{
			return this.estado == PEDIDO;
		}
	}
}
