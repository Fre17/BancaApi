using BancaApi.Models;
using BancaApi.Repository.IRepository;
using BancaApi.Services.IService;
using BancaApi.Util;

namespace BancaApi.Services.Service
{
    public class CuentaBancariaService : ICuentaBancariaService
    {
        private readonly ICuentaBancariaRepository _cuentaBancariaRepository;
        public CuentaBancariaService(ICuentaBancariaRepository cuentaBancariaRepository)
        {
            _cuentaBancariaRepository = cuentaBancariaRepository;
        }
        public Task<Response<List<CuentaBancariaModel>>> ConsultaCuentaBancaria(CuentaBancariaModel pCuentaBancaria)
        {
            try
            {
                return _cuentaBancariaRepository.ConsultaCuentaBancaria(pCuentaBancaria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Response<CuentaBancariaModel>> MantenimientoCuentaBancaria(CuentaBancariaModel pCuentaBancaria)
        {
            try
            {
                return _cuentaBancariaRepository.MantenimientoCuentaBancaria(pCuentaBancaria);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
