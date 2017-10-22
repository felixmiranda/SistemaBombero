using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BOM.DataLayer
{
    public class temporalDA
    {
        private SqlCommand mCm;
        private SqlDataReader mDr;
        private SqlDataAdapter mDa;
        private DataSet mdd;
        SqlConnection con = new SqlConnection(D_Conexion.conx);


        public DataTable obtenerEspaciosxfiltro(string _elementosSeleccionados, string _inmueblesSeleccionados,
            string _areaDesde, string _areaHasta, int pi_idProducto, string ps_razonSocial)
        {
            try
            {
                mDa = new SqlDataAdapter("PUBLICIDAD.ADV_SP_PUB_SERVICIO_ESPACIOS_XFILTRO_LISTAR", con);
                mDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                mDa.SelectCommand.Parameters.AddWithValue("@pv_elementosSeleccion", _elementosSeleccionados);
                mDa.SelectCommand.Parameters.AddWithValue("@pv_inmueblesSeleccion", _inmueblesSeleccionados);
                mDa.SelectCommand.Parameters.AddWithValue("@pv_areaDesde", _areaDesde);
                mDa.SelectCommand.Parameters.AddWithValue("@pv_areaHasta", _areaHasta);
                mDa.SelectCommand.Parameters.AddWithValue("@idProducto", pi_idProducto);
                
                mDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                mdd = new DataSet();
                mDa.Fill(mdd);
                return mdd.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable obtenerinfoespacio(int _idespacio)
        {
            try
            {
                mDa = new SqlDataAdapter("PUBLICIDAD.ADV_SP_PUB_SERVICIO_ESPACIO_XID_SELECCIONAR", con);
                mDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                mDa.SelectCommand.Parameters.AddWithValue("@pi_pub_esp_c_iid", _idespacio);
                mDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                mdd = new DataSet();
                mDa.Fill(mdd);
                return mdd.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable listarElementoActivacionxproductoDA(int id_espacio)
        {
            try
            {
                mDa = new SqlDataAdapter("WS_MOBILE.ADV_SP_PUB_ELEMENTO_ACTIVACION_X_ESPACIO_LISTAR", con);
                mDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                mDa.SelectCommand.Parameters.AddWithValue("@pi_idEspacio", id_espacio);
                mDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                mdd = new DataSet();
                mDa.Fill(mdd);
                return mdd.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
