using Azure;
using BancaApi.Models;
using BancaApi.Repository.IRepository;
using BancaApi.Util;
using Microsoft.Data.SqlClient;

namespace BancaApi.Repository.Repository
{
    public class CuentaBancariaRepository : ICuentaBancariaRepository
    {
        private SqlContext _contexto;

        public CuentaBancariaRepository(SqlContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<Util.Response<List<CuentaBancariaModel>>> ConsultaCuentaBancaria(CuentaBancariaModel pCuentaBancaria)
        {
            Util.Response<List<CuentaBancariaModel>> response;
            try
            {
                using (SqlConnection _cnxSql = _contexto.ObtenerConexionBaseDeDatos())
                {
                    using (SqlCommand _cmdSql = new SqlCommand("PA_CON_TBL_BANCA_CUENTA_BANCARIA", _cnxSql))
                    {
                        _cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
                        _cmdSql.Parameters.AddWithValue("@P_OPCION", pCuentaBancaria.Opcion);
                        _cmdSql.Parameters.AddWithValue("@P_USUARIO", pCuentaBancaria.Usuario);
                        _cmdSql.Parameters.AddWithValue("@P_PK_TBL_BANCA_CUENTA_BANCARIA", pCuentaBancaria.Pk_Tbl_Cuenta_Bancaria);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_USUARIOS", pCuentaBancaria.Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario);
                        _cmdSql.Parameters.AddWithValue("@P_IDENTIFICACION", pCuentaBancaria.Fk_Tbl_Usuario.Identificacion);

                        using (SqlDataReader _readerSql = _cmdSql.ExecuteReader())
                        {
                            List<CuentaBancariaModel> oListaCuentaBancaria = new List<CuentaBancariaModel>();

                            while (_readerSql.Read())
                            {
                                oListaCuentaBancaria.Add(new CuentaBancariaModel
                                {
                                    Pk_Tbl_Cuenta_Bancaria = Util.BDUtilitario.ObtieneInt(_readerSql, "PK_TBL_BANCA_CUENTA_BANCARIA"),
                                    Fk_Tbl_Usuario = new UsuarioModel
                                    {
                                        Pk_Tbl_Banca_Usuario = Util.BDUtilitario.ObtieneInt(_readerSql, "FK_TBL_BANCA_USUARIO"),
                                        Identificacion = Util.BDUtilitario.ObtieneString(_readerSql, "IDENTIFICACION"),
                                        Nombre = Util.BDUtilitario.ObtieneString(_readerSql, "NOMBRE"),
                                    },
                                    Saldo = Util.BDUtilitario.ObtieneDecimal(_readerSql, "SALDO"),
                                });
                            }
                            // Crear la respuesta basada en el resultado de la consulta
                            if (oListaCuentaBancaria.Any())
                            {
                                response = new Util.Response<List<CuentaBancariaModel>>
                                {
                                    Datos = oListaCuentaBancaria,
                                    Mensaje = "Cuenta bancaria encontrada con éxito",
                                    Exitoso = true,
								};
                            }
                            else
                            {
                                response = new Util.Response<List<CuentaBancariaModel>>
                                {
                                    Datos = null,
                                    Mensaje = "Cuenta bancaria no existe",
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

        public async Task<Util.Response<CuentaBancariaModel>> MantenimientoCuentaBancaria(CuentaBancariaModel pCuentaBancaria)
        {
            Util.Response<CuentaBancariaModel> response;
            try
            {
                using (SqlConnection _cnxSql = _contexto.ObtenerConexionBaseDeDatos())
                {
                    using (SqlCommand _cmdSql = new SqlCommand("PA_MAN_TBL_BANCA_CUENTA_BANCARIA", _cnxSql))
                    {
                        _cmdSql.CommandType = System.Data.CommandType.StoredProcedure;
                        _cmdSql.Parameters.AddWithValue("@P_OPCION", pCuentaBancaria.Opcion);
                        _cmdSql.Parameters.AddWithValue("@P_USUARIO", pCuentaBancaria.Usuario);
                        _cmdSql.Parameters.AddWithValue("@P_PK_TBL_BANCA_CUENTA_BANCARIA", pCuentaBancaria.Pk_Tbl_Cuenta_Bancaria);
                        _cmdSql.Parameters.AddWithValue("@P_FK_TBL_BANCA_USUARIOS", pCuentaBancaria.Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario);
                        _cmdSql.Parameters.AddWithValue("@P_SALDO", pCuentaBancaria.Saldo);

                        _cmdSql.ExecuteNonQuery();
                    }
                }
                response = new Util.Response<CuentaBancariaModel> { Datos = null, Mensaje = "Operación de mantenimiento realizada con éxito", Exitoso = true };
            }
            catch (Exception ex)
            {
                response = new Util.Response<CuentaBancariaModel> { Datos = null, Mensaje = $"Error en la operación: {ex.Message}", Exitoso = false };
            }
            return response;
        }
    }
}
