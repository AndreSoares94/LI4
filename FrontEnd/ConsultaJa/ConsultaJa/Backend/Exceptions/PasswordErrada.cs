using System;

namespace ConsultaJa.Exceptions
{
	public class PasswordErrada : Exception
	{
		public PasswordErrada() : base() { }

		public PasswordErrada(String message) : base(message) { }
	}
}