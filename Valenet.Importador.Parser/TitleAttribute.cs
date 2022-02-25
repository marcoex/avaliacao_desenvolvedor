using System;

namespace Valenet.Importador.Parser
{
	/// <summary>
	/// Título do campo conforme aparecerá no arquivo de importação.
	/// </summary>
	public class TitleAttribute : Attribute
	{
		public TitleAttribute(string val)
		{
			this.Value = val;
		}

		public string Value { get; }
	}
}