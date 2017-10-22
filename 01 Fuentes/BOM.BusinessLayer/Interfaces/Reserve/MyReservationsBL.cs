using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer.Interfaces.Reserve;
namespace BOM.BusinessLayer.Interfaces.Reserve
{
    public interface IMyReservationsBL
    {
        List<DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR_Result> f_ListarReservasPendientesXEjecutivoBL(string ps_inmueble, string ps_cliente, string ps_ejecutivo);
    }
    public class MyReservationsBL : IMyReservationsBL
    {
        public void Dispose()
        {
            GC.Collect();
        }
        public List<DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR_Result> f_ListarReservasPendientesXEjecutivoBL(string ps_inmueble, string ps_cliente, string ps_ejecutivo)
        {
            return new MyReservationsDA().f_ListarReservasPendientesXEjecutivoDA(ps_inmueble, ps_cliente, ps_ejecutivo);
        }  
    }
}
