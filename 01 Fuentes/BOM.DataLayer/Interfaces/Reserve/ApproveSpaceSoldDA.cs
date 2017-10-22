using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using System.Data.Entity;
using System.Data.Objects;

namespace BOM.DataLayer.Interfaces.Reserve
{
    public interface IApproveSpaceSoldDA
    {
        List<DIO_SP_PUB_APROB_RESERVA_LISTAR_Result> f_ListarAprobacionesReservaDA(string ps_inmueble, string ps_cliente, string ps_ejecutivo);
        List<ADV_T_CLIENTE> f_ListarClientesDA();
        void f_reserva_venderDA(DIO_PUB_T_RESERVA obj);
    }
    public class ApproveSpaceSoldDA : IApproveSpaceSoldDA
    {
        public void Dispose()
        {
            GC.Collect();
        }
        public List<DIO_SP_PUB_APROB_RESERVA_LISTAR_Result> f_ListarAprobacionesReservaDA(string ps_inmueble, string ps_cliente, string ps_ejecutivo)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_PUB_APROB_RESERVA_LISTAR(ps_inmueble, ps_cliente, ps_ejecutivo).ToList();
                }
            }
            catch { throw; }
        }
        public List<ADV_T_CLIENTE> f_ListarClientesDA()
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.ADV_T_CLIENTE
                        where x.cli_c_bactivo == true
                        select x).ToList();
            }
        }

        public void f_reserva_venderDA(DIO_PUB_T_RESERVA obj)
        {
            try
            {
                using (BD_DIONISIOEntities c = new BD_DIONISIOEntities())
                {
                    c.SP_PUB_RESERVA_VENDER(obj.reser_c_iid, obj.pub_esp_c_iid, obj.reser_mast_c_fac_bagencia,obj.reser_c_eprecio_alquiler);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
