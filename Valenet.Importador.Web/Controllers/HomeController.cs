﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Valenet.Importador.Data;
using Valenet.Importador.Models;
using Valenet.Importador.Parser;
using Valenet.Importador.Web.Models;

namespace Valenet.Importador.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ValenetContext _context;
		private readonly IMapper _mapper;

		public HomeController(ILogger<HomeController> logger, Data.ValenetContext context, IMapper mapper)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var model = new ImportacaoViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(IFormFile file)
		{
			var pp = new Parser.ParserProcessor<Entities.Pedido>();
			pp.File = await DownloadTempFile(file);
			pp.Parse();

			var success = pp.ObjectList.Select(x => new PedidoViewModel() {
				Index = x.Key,
				Comprador = x.Value.Comprador,
				Descricao = x.Value.Descricao,
				Endereco = x.Value.Endereco,
				Fornecedor = x.Value.Fornecedor,
				PrecoUnitario = x.Value.PrecoUnitario,
				Quantidade = x.Value.Quantidade,
				Success = true,
			});
			var errors = pp.ErrorList.Select(x => new PedidoViewModel() {
				Index = x.Key,
				Success = false,
				Message = x.Value,
			});

			foreach (var item in pp.ObjectList) {
				var pedido = _mapper.Map<Data.Model.Pedido>(item.Value);
				pedido.Id = Guid.NewGuid();
				_context.Pedidos.Add(pedido);
			}
			await _context.SaveChangesAsync();

			var model = new ImportacaoViewModel();
			model.Pedidos = Enumerable.Union(success, errors).OrderBy(x => x.Index).ToList();
			model.ReceitaBrutalTotal = model.Pedidos.Sum(x => x.PrecoUnitario * x.Quantidade).Value;

			return View(model);
		}

		/// <summary>
		/// Salvar arquivo enviado via formulário em um arquivo temporário do windows.
		/// </summary>
		/// <returns></returns>
		private async Task<FileInfo> DownloadTempFile(IFormFile file)
		{
			var path = Path.GetTempFileName();
			using var tempFile = new FileStream(path, FileMode.Truncate);
			file.CopyTo(tempFile);
			return new FileInfo(path);
		}


	}
}
