using BancaApi.Models;
using BancaApi.Util;

namespace BancaApi.Repository.IRepository
{
    public interface ICuentaBancariaRepository
    {
        public Task<Response<List<CuentaBancariaModel>>> ConsultaCuentaBancaria(CuentaBancariaModel pCuentaBancaria);
        public Task<Response<CuentaBancariaModel>> MantenimientoCuentaBancaria(CuentaBancariaModel pCuentaBancaria);
    }
}
