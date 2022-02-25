#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Valenet.Importador.Parser;

namespace Valenet.Importador.Entities
{
	public class Pedido
	{
		[Order(0)]
		[MaxLength(100)]
		public string? Comprador;

		[Order(1)]
		[Title("Descrição")]
		[MaxLength(200)]
		public string? Descricao;

		[Order(4)]
		[Title("Endereço")]
		[MaxLength(300)]
		public string? Endereco;

		[Order(5)]
		[MaxLength(100)]
		public string? Fornecedor;

		[Order(2)]
		[Title("Preço Unitário")]
		[PreProcessor(nameof(PreProcessors.MoneyToDouble))]
		[Range(0, double.MaxValue)]
		public double? PrecoUnitario;

		[Order(3)]
		[Range(0, short.MaxValue)]
		public short? Quantidade;

	}
}
