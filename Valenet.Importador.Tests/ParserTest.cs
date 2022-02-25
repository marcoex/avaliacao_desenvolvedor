using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Valenet.Importador.Entities;
using Valenet.Importador.Parser;

namespace Valenet.Importador.Tests
{
	[TestClass]
	public class ParserTest
	{
		[TestMethod]
		public void Parse()
		{
			var parser = new ParserProcessor<Pedido>();
			parser.File = new FileInfo("dados.txt");
			parser.Parse();
			Assert.AreEqual(0, parser.ErrorList.Count);
		}
	}
}
