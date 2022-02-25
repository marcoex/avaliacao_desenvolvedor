#nullable enable
using System;

namespace Valenet.Importador.Models
{
	public class PedidoViewModel : Entities.Pedido
	{
		public int Index;

		public bool Success = false;

		public string? Message;
	}
}
