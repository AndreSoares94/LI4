using System;

namespace ConsultaJa
{
	public class PasswordErrada : Exception
	{
		public PasswordErrada() : base() { }

		public PasswordErrada(String message) : base(message) { }
	}
}