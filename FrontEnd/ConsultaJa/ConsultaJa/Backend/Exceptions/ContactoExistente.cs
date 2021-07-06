using System;

namespace ConsultaJa.Exceptions
{

	public class ContactoExistente : Exception
	{
		public ContactoExistente(): base() { }

		public ContactoExistente(string s): base(s) { }
	}
}