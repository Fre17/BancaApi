using BancaApi.Models;
using BancaApi.Repository.IRepository;
using BancaApi.Repository.Repository;
using BancaApi.Services.IService;
using BancaApi.Util;

namespace BancaApi.Services.Service
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        public TransaccionService(ITransaccionRepository cuentaBancariaRepository)
        {
            _transaccionRepository = cuentaBancariaRepository;
        }
        public Task<Response<List<TransaccionesModel>>> ConsultaTransaccionBancaria(TransaccionesModel pTransaccion)
        {
            try
            {
                return _transaccionRepository.ConsultaTransaccionBancaria(pTransaccion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Response<TransaccionesModel>> MantenimientoTransaccionBancaria(TransaccionesModel pTransaccion)
        {
            try
            {
                return _transaccionRepository.MantenimientoTransaccionBancaria(pTransaccion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
