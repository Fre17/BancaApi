using BancaApi.Models;
using BancaApi.Repository.IRepository;
using BancaApi.Util;
using Microsoft.Data.SqlClient;

namespace BancaApi.Repository.Repository
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private SqlContext _contexto;

        public TransaccionRepository(SqlContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<Response<List<TransaccionesModel>>> ConsultaTransaccionBancaria(TransaccionesModel pTransaccion)
        {
            Response<List<TransaccionesModel>> response;
            try
            {
                using (SqlConnection _cnxSql = _contexto.ObtenerConexionBaseDeDatos())
                {
                    using (SqlCommand _cmdSql = new SqlCommand("PA_CON_TBL_BANCA_transacciones", _cnxSql))
                    {
                        _cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
                        _cmdSql.Parameters.AddWithValue("@P_OPCION", pTransaccion.Opcion);
                        _cmdSql.Parameters.AddWithValue("@P_USUARIO", pTransaccion.Usuario);
                        _cmdSql.Parameters.AddWithValue("@P_PK_TBL_BANCA_TRANSACCIONES", pTransaccion.Pk_Tbl_Banca_Transacciones);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_TIPO_TRANSACCION", pTransaccion.Fk_Tbl_Banca_Tipo_Transaccion.Pk_Tbl_Banca_Tipo_Transaccion);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_CUENTA_BANCARIA_ORIGEN", pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Pk_Tbl_Cuenta_Bancaria);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_CUENTA_BANCARIA_DESTINO", pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Pk_Tbl_Cuenta_Bancaria);
                        
                        using (SqlDataReader _readerSql = _cmdSql.ExecuteReader())
                        {
                            List<TransaccionesModel> oListaCuentaBancaria = new List<TransaccionesModel>();

                            while (_readerSql.Read())
                            {
                                oListaCuentaBancaria.Add(new TransaccionesModel
                                {
                                    Pk_Tbl_Banca_Transacciones = Util.BDUtilitario.ObtieneInt(_readerSql, "PK_TBL_BANCA_TRANSACCIONES"),
                                    Fk_Tbl_Banca_Tipo_Transaccion = new TipoTransaccionModel
                                    {
                                        Pk_Tbl_Banca_Tipo_Transaccion = Util.BDUtilitario.ObtieneInt(_readerSql, "FK_TBL_BANCA_TIPO_TRANSACCION"),
                                        Descripcion = Util.BDUtilitario.ObtieneString(_readerSql, "DESCRIPCION"),
                                    },
                                    Fk_Tbl_Banca_Cuenta_Bancaria_Origen = new CuentaBancariaModel
                                    {
                                        Pk_Tbl_Cuenta_Bancaria = Util.BDUtilitario.ObtieneInt(_readerSql, "FK_TBL_BANCA_CUENTA_BANCARIA_ORIGEN"),
                                    },
                                    Fk_Tbl_Banca_Cuenta_Bancaria_Destino = new CuentaBancariaModel
                                    {
                                        Pk_Tbl_Cuenta_Bancaria = Util.BDUtilitario.ObtieneInt(_readerSql, "FK_TBL_BANCA_CUENTA_BANCARIA_DESTINO"),
                                    },
                                    Monto = Util.BDUtilitario.ObtieneDecimal(_readerSql, "MONTO"),
                                    Detalle = Util.BDUtilitario.ObtieneString(_readerSql, "DETALLE"),
                                    FechaMovimiento = Util.BDUtilitario.ObtieneDateTime(_readerSql, "FECHA_CREACION"),
                                });
                            }
                            // Crear la respuesta basada en el resultado de la consulta
                            if (oListaCuentaBancaria.Any())
                            {
                                response = new Response<List<TransaccionesModel>>
                                {
                                    Datos = oListaCuentaBancaria,
                                    Mensaje = "Transacción bancaria encontrada con éxito",
                                    Exitoso = true,
								};
                            }
                            else
                            {
								response = new Response<List<TransaccionesModel>>
								{
									Datos = null,
									Mensaje = "Transacción bancaria no existe",
									Exitoso = false,
								};
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        public async Task<Response<TransaccionesModel>> MantenimientoTransaccionBancaria(TransaccionesModel pTransaccion)
        {
            Response<TransaccionesModel> response;
            try
            {
                using (SqlConnection _cnxSql = _contexto.ObtenerConexionBaseDeDatos())
                {
                    using (SqlCommand _cmdSql = new SqlCommand("PA_MAN_TBL_BANCA_TRANSACCIONES", _cnxSql))
                    {
                        _cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
                        _cmdSql.Parameters.AddWithValue("@P_OPCION", pTransaccion.Opcion);
                        _cmdSql.Parameters.AddWithValue("@P_USUARIO", pTransaccion.Usuario);
                        _cmdSql.Parameters.AddWithValue("@P_PK_TBL_BANCA_TRANSACCIONES", pTransaccion.Pk_Tbl_Banca_Transacciones);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_TIPO_TRANSACCION", pTransaccion.Fk_Tbl_Banca_Tipo_Transaccion.Pk_Tbl_Banca_Tipo_Transaccion);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_CUENTA_BANCARIA_ORIGEN", pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Pk_Tbl_Cuenta_Bancaria);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_CUENTA_BANCARIA_DESTINO", pTransaccion.Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Pk_Tbl_Cuenta_Bancaria);
                        _cmdSql.Parameters.AddWithValue("@P_MONTO", pTransaccion.Monto);
                        _cmdSql.Parameters.AddWithValue("@P_DETALLE", pTransaccion.Detalle);

                        _cmdSql.ExecuteNonQuery();
                    }
                }
                response = response = new Response<TransaccionesModel> 
                { 
                    Datos = null, 
                    Mensaje = "Operación de mantenimiento realizada con éxito", 
                    Exitoso = true 
                };
            }
            catch (Exception ex)
            {
                response = new Response<TransaccionesModel>
				{
					Datos = null,
					Mensaje = $"Error en la operación: {ex.Message}",
					Exitoso = false,
				}; 
            }
            return response;
        }
    }
}
