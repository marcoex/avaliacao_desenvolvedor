using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Valenet.Importador.Parser
{
	/// <summary>
	/// Processador de lotes de registros.
	/// </summary>
	/// <typeparam name="T">Tipo da classe tipada de destino.</typeparam>
	public class ParserProcessor<T> where T : new()
	{
		/// <summary>
		/// Caracteres delimitadores de coluna.
		/// </summary>
		public string ColumnDelimiter { get; set; } = "\t";

		/// <summary>
		/// Lista de objetos importados com sucesso. Indexados na chave.
		/// </summary>
		public Dictionary<int, T> ObjectList { get; } = new Dictionary<int, T>();

		/// <summary>
		/// Lista de erros encontrados. Indexados na chave.
		/// </summary>
		public Dictionary<int, string> ErrorList { get; } = new Dictionary<int, string>();

		/// <summary>
		/// Arquivo de entrada para importação.
		/// </summary>
		public FileInfo File { get; set; }

		/// <summary>
		/// Executar processamento de arquivo
		/// </summary>
		public void Parse()
		{
			if (File == null || !File.Exists)
				throw new FileNotFoundException("O arquivo de importação não foi definido ou não foi encontrado.");

			ObjectList.Clear();
			ErrorList.Clear();

			using var reader = new StreamReader(File.FullName);
			var headerLine = reader.ReadLine();
			var headerCols = headerLine.Split(ColumnDelimiter);
			var properties = new T().GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();  //lista de propriedades/atributos
			properties = properties.OrderBy(x => x.GetCustomAttribute<OrderAttribute>().Value).ToList();  //ordenadas pelo indice
			properties.RemoveAll(x => !headerCols.Contains(x.GetCustomAttribute<TitleAttribute>()?.Value ?? x.Name));  //remover propriedades que não estão abrangidas na linha de colunas

			int index = 0;
			while (!reader.EndOfStream) {
				var line = reader.ReadLine();
				//todo:fazer validações de tamanho e range
				try {
					var cols = line.Split(ColumnDelimiter).Select(x => (object)x).ToArray();
					var obj = new T();
					for (int i = 0; i < cols.Length; i++) {
						var ppFunc = properties[i].GetCustomAttribute<PreProcessorAttribute>()?.PreProcessor;
						if (ppFunc != null) {
							cols[i] = typeof(PreProcessors).GetMethod(ppFunc).Invoke(null, new object[] { cols[i] });
						}
						properties[i].SetValue(obj, Convert.ChangeType(cols[i], NativeTypeOf(properties[i].FieldType)));
					}
					ObjectList.Add(index++, obj);
				} catch (Exception ex) {
					ErrorList.Add(index++, $"[{line}] -> {ex.Message}");
				}
			}
		}

		private Type NativeTypeOf(Type fieldType)
		{
			return Nullable.GetUnderlyingType(fieldType) ?? fieldType;
		}
	}
}
