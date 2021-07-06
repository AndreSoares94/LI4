using System;

public class ClassificacaoInvalida : Exception
{
	public ClassificacaoInvalida() : base() { }

	public ClassificacaoInvalida(string message) : base(message) { }
}
