using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer.Interfaces.Reserve;
namespace BOM.BusinessLayer.Interfaces.Reserve
{
    public interface ISpaceAdvertinsingReportBL
    {
        List<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> f_ListaReporteEspaciosPublicitariosBL(string ps_inmueble, string ps_ejecutivo, Int32 ps_tipoProducto, Int32 ps_estado);

        IList<DIO_PUB_T_ESPACIO_OCUP_ESTADO> f_ListarEstadoEspacioPublicitario();
    }
    public class SpaceAdvertinsingReportBL : ISpaceAdvertinsingReportBL
    {
        public void Dispose()
        {
            GC.Collect();
        }

        public IList<DIO_PUB_T_ESPACIO_OCUP_ESTADO> f_ListarEstadoEspacioPublicitario()
        {
            return new SpaceAdvertinsingReportDA().f_ListarEstadoEspacioPublicitarioDA();
        }

        public List<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> f_ListaReporteEspaciosPublicitariosBL(string ps_inmueble, string ps_ejecutivo, Int32 ps_tipoProducto, Int32 ps_estado)
        {
            return new SpaceAdvertinsingReportDA().f_ListarReporteEspaciosPublicitariosDA(ps_inmueble, ps_ejecutivo, ps_tipoProducto, ps_estado); 
        }
    }
}
