using BancaApi.Models;
using BancaApi.Util;

namespace BancaApi.Services.IService
{
    public interface ICuentaBancariaService
    {
        public Task<Response<List<CuentaBancariaModel>>> ConsultaCuentaBancaria(CuentaBancariaModel pCuentaBancaria);
        public Task<Response<CuentaBancariaModel>> MantenimientoCuentaBancaria(CuentaBancariaModel pCuentaBancaria);
    }
}
