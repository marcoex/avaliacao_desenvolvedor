using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valenet.Importador.Models;

namespace Valenet.Importador.Web.Models
{
	public class ImportacaoViewModel
	{
		public double ReceitaBrutalTotal { get;set;}

		public List<PedidoViewModel> Pedidos { get; internal set; } = new List<PedidoViewModel>();
	}
}
