using System;
using System.Collections.Generic;

#nullable disable

namespace Valenet.Importador.Data.Model
{
    public partial class Pedido
    {
        public Guid Id { get; set; }
        public string Comprador { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public string Fornecedor { get; set; }
        public decimal? PrecoUnitario { get; set; }
        public short? Quantidade { get; set; }
    }
}
