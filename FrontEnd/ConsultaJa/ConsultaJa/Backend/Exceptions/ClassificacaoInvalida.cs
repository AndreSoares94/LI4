using System;

namespace ConsultaJa.Exceptions
{
	public class ClassificacaoInvalida : Exception
	{
		public ClassificacaoInvalida() : base() { }

		public ClassificacaoInvalida(string message) : base(message) { }
	}
}
