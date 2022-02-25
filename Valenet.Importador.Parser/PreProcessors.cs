using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Valenet.Importador.Parser
{
	/// <summary>
	/// Funções pré-definidas de pré-processamento.
	/// </summary>
	public static class PreProcessors
	{
		public static double? MoneyToDouble(string value)
		{
			double? parsed = null;
			var cultureInfo = new CultureInfo("en-US");
			parsed = Double.Parse(value, cultureInfo);
			return parsed;
		}
	}

}
