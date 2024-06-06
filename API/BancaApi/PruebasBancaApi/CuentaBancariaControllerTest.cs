using BancaApi.Controllers;
using BancaApi.Models;
using BancaApi.Repository.IRepository;
using BancaApi.Repository.Repository;
using BancaApi.Services.IService;
using BancaApi.Services.Service;
using BancaApi.Util;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PruebasBancaApi
{
	public class CuentaBancariaControllerTest
	{
		//cuenta bancaria controller 
		private readonly CuentaBancariaController _cuentaBancariaController;
		private readonly ICuentaBancariaService _cuentaBancariaService;
		private readonly ICuentaBancariaRepository _cuentaBancariaRepository;		
		private readonly SqlContext _contexto;

		public CuentaBancariaControllerTest()
		{
			HtmlSanitizer htmlSanitizer = new HtmlSanitizer();
			// Construir configuración a partir del archivo appsettings.json
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			_contexto = new SqlContext(configuration);
			//cuenta bancaria controller 
			_cuentaBancariaRepository = new CuentaBancariaRepository(_contexto);
			_cuentaBancariaService = new CuentaBancariaService(_cuentaBancariaRepository);
			_cuentaBancariaController = new CuentaBancariaController(htmlSanitizer, _cuentaBancariaService);
		}
		//pruebas unitarias
		//pruebas a cuenta bancaria controller 
		//metodo crea una cuenta bancaria
		[Fact]
		public async Task CrearCuentaBancaria()
		{
			//se cuenta unicamente con el usuario pk = 1
			IActionResult result = await _cuentaBancariaController.MantenimientoCuentaBancaria(new BancaApi.Models.CuentaBancariaModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
				Pk_Tbl_Cuenta_Bancaria = 0,
				Fk_Tbl_Usuario = new BancaApi.Models.UsuarioModel
				{
					Pk_Tbl_Banca_Usuario = 1,
				},
				Saldo = 1000,
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//metodo para actualizar una cuenta bancaria
		[Fact]
		public async Task ActualizarCuentaBancaria()
		{
			//se cuenta unicamente con el usuario pk = 1
			IActionResult result = await _cuentaBancariaController.MantenimientoCuentaBancaria(new BancaApi.Models.CuentaBancariaModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
				Pk_Tbl_Cuenta_Bancaria = 10,
				Fk_Tbl_Usuario = new BancaApi.Models.UsuarioModel
				{
					Pk_Tbl_Banca_Usuario = 1,
				},
				Saldo = 1500,
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//metodo consulta todas las cuentas bancarias 
		[Fact]
		public async Task ConsultaCuentaBancaria()
		{
			IActionResult result = await _cuentaBancariaController.ConsultaCuentaBancaria(new BancaApi.Models.CuentaBancariaModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
			});
			Assert.IsType<OkObjectResult>(result);
		}
		//metodo consulta cuenta bancaria aplicando filtros
		[Fact]
		public async Task ConsultaCuentaBancariaFiltros()
		{
			IActionResult result = await _cuentaBancariaController.ConsultaCuentaBancaria(new BancaApi.Models.CuentaBancariaModel
			{
				Opcion = 0,
				Usuario = "Unit Test",
				Pk_Tbl_Cuenta_Bancaria = 10,
				Fk_Tbl_Usuario = new BancaApi.Models.UsuarioModel
				{
					Pk_Tbl_Banca_Usuario = 1,
					Identificacion = "117460382",
				},
				Saldo = 1500,
			});
			Assert.IsType<OkObjectResult>(result);
		}
		
		
	}
}