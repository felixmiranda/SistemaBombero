using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using System.Data.Entity;
using System.Data.Objects;

namespace BOM.DataLayer.Interfaces.Reserve
{
    public interface IMyReservationsDA
    {
        List<DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR_Result> f_ListarReservasPendientesXEjecutivoDA(string ps_inmueble, string ps_cliente, string ps_ejecutivo);
    }
    public class MyReservationsDA : IMyReservationsDA
    {
        public void Dispose()
        {
            GC.Collect();
        }
        public List<DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR_Result> f_ListarReservasPendientesXEjecutivoDA(string ps_inmueble, string ps_cliente, string ps_ejecutivo)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR(ps_inmueble, ps_cliente, ps_ejecutivo).ToList();
                }
            }
            catch { throw; }
        }
    }
}
