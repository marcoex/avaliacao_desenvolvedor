using System;

namespace Valenet.Importador.Parser
{
	/// <summary>
	/// Determina a ordem que o campo aparece no arquivo de texto.
	/// </summary>
	public class OrderAttribute : Attribute
	{
		public OrderAttribute(short val)
		{
			this.Value = val;
		}

		public short Value { get; }
	}
}