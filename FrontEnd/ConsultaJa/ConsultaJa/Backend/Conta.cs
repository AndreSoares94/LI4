using System;
using System.Collections.Generic;
using ConsultaJa.DataBase;
using ConsultaJa.Exceptions;

namespace ConsultaJa.Backend
{
	public abstract class Conta
	{
		/**
		 * Variável que guarda o id único da 
		 * conta aquando do seu registo na 
		 * aplicação
		 */
		private string id;

		/**
		 * Variável que guarda o email 
		 * associado à conta
		 */
		private string email;

		/**
		 * Variável que guarda a password 
		 * da conta
		 */
		private string password;

		/**
		 * Variável que guarda o nome 
		 * associado à conta
		 */
		private string nome;

		/**
		 * Variável que guarda a data de nascimento 
		 * do utilizador da conta
		 */
		private DateTime dataNascimento;

		/**
		 * Variável que guarda uma lista com os 
		 * contactos associados a um objeto da 
		 * classe Conta
		 */
		private List<string> contactos;

		/**
		 * Método que retorna a string correspondente 
		 * ao id da conta à qual é enviado o método
		 */
		public string getID()
		{
			return this.id;
		}

		/**
		 * Método que retorna o mail associado 
		 * à conta à qual é enviado o método
		 */
		public string getEmail()
		{
			return this.email;
		}

		/**
		 * Método que retorna a password associada 
		 * à conta à qual é enviado o método
		 */
		public string getPassword()
		{
			return this.password;
		}

		/**
		 * Método que retorna o nome associado
		 * à conta à qual é enviado o método
		 */
		public string getNome()
		{
			return this.nome;
		}

		/**
		 * Método que retorna a data de 
		 * nascimento associada à conta
		 */
		public DateTime getDataNascimento()
		{
			return this.dataNascimento;
		}

		/**
		 * Método que retorna o histórico de uma conta
		 */
		public abstract List<Consulta> getHistorico();

		/**
		 * Método que retorna um conjunto de consultas 
		 * agendadas associadas à conta à qual é 
		 * enviado o método
		 */
		public abstract List<Consulta> getConsultasAgendadas();

		/**
		 * Método que permite atribuir um valor à 
		 * variável id do objeto da classe conta ao 
		 * qual é enviado o método
		 */
		public void setID(string id)
		{
			this.id = id;
		}

		/**
		 * Método que permite alterar a password do 
		 * objeto da classe Conta ao qual é enviado 
		 * o método
		 */
		public void setPassword(string password)
		{
			this.password = password;
		}

		/**
		 * Construtor para objetos da classe Conta
		 */
		public Conta(string email, string password, string nome, DateTime dataNascimento)
		{
			this.id = "";
			this.email = email;
			this.password = password;
			this.nome = nome;
			this.dataNascimento = dataNascimento;
			this.contactos = new List<string>();
		}

		/**
		 * Método que permite alterar a password 
		 * da conta, fornecendo a sua pass antiga
		 */
		public void alterarPassword(string password, string novaPass)
		{
			if (!this.password.Equals(password))
				throw new PasswordErrada("Password incorreta");

			this.password = novaPass;
		}

		/**
		 * Método que permite adicionar um 
		 * contacto a um paciente
		 */
		public void addContacto(string contacto)
		{
			if (this.contactos.Contains(contacto))
				throw new Exceptions.ContactoExistente("Contacto já existe.");
			this.contactos.Add(contacto);
		}

		/**
		 * Método que retorna os contactos 
		 * associados a uma conta
		 */
		public List<string> getContactos()
		{
			return this.contactos;
		}
	}
}

