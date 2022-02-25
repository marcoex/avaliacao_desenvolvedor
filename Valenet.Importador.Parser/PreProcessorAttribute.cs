using System;

namespace Valenet.Importador.Parser
{
	/// <summary>
	/// Nome da função de pré-processamento utilizada.
	/// </summary>
	/// <remarks>As funções de pré-processamento estão na classe PreProcessors.</remarks>
	public class PreProcessorAttribute : Attribute
	{
		public PreProcessorAttribute(string preProcessor)
		{
			this.PreProcessor = preProcessor;
		}

		public string PreProcessor { get; }
	}
}