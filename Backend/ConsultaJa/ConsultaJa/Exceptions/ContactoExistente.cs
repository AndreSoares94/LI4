using System;

namespace Exceptions
{

	public class ContactoExistente : Exception
	{
		public ContactoExistente(): base() { }

		public ContactoExistente(string s): base(s) { }
	}
}