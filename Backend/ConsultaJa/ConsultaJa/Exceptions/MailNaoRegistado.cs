using System;

public class MailNaoRegistado : Exception
{
	public MailNaoRegistado() : base() { }

	public MailNaoRegistado(string message) : base(message) { }
}
