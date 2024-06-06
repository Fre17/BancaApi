using BancaApi.Models;
using BancaApi.Util;

namespace BancaApi.Repository.IRepository
{
    public interface ITransaccionRepository
    {
        public Task<Response<List<TransaccionesModel>>> ConsultaTransaccionBancaria(TransaccionesModel pTransaccion);
        public Task<Response<TransaccionesModel>> MantenimientoTransaccionBancaria(TransaccionesModel pTransaccion);
    }
}
