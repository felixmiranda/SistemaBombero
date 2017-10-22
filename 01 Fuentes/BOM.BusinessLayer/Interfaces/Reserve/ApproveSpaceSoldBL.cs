using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer.Interfaces.Reserve;
namespace BOM.BusinessLayer.Interfaces.Reserve
{
    public interface IApproveSpaceSoldBL
    {
        List<DIO_SP_PUB_APROB_RESERVA_LISTAR_Result> f_ListarAprobacionesReservaBL(string ps_inmueble, string ps_cliente, string ps_ejecutivo);
        List<ADV_T_CLIENTE> f_ListarClientesBL();
        void f_reserva_venderBL(DIO_PUB_T_RESERVA obj);
    }
    public class ApproveSpaceSoldBL : IApproveSpaceSoldBL
    {
        public void Dispose()
        {
            GC.Collect();
        }
        public List<DIO_SP_PUB_APROB_RESERVA_LISTAR_Result> f_ListarAprobacionesReservaBL(string ps_inmueble, string ps_cliente, string ps_ejecutivo)
        {
            return new ApproveSpaceSoldDA().f_ListarAprobacionesReservaDA(ps_inmueble, ps_cliente, ps_ejecutivo);
        }
        public List<ADV_T_CLIENTE> f_ListarClientesBL()
        {
            return new ApproveSpaceSoldDA().f_ListarClientesDA();
        }
        public void f_reserva_venderBL(DIO_PUB_T_RESERVA obj)
        {
            new ApproveSpaceSoldDA().f_reserva_venderDA(obj);
        }
         
    }
}
