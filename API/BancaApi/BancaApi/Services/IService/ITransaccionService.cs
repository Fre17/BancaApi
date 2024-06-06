using BancaApi.Models;
using BancaApi.Util;

namespace BancaApi.Services.IService
{
    public interface ITransaccionService
    {
        public Task<Response<List<TransaccionesModel>>> ConsultaTransaccionBancaria(TransaccionesModel pTransaccion);
        public Task<Response<TransaccionesModel>> MantenimientoTransaccionBancaria(TransaccionesModel pTransaccion);
    }
}
