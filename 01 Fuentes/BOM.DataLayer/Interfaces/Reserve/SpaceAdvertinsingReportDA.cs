using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using System.Data.Entity;
using System.Data.Objects;

namespace BOM.DataLayer.Interfaces.Reserve
{
    public interface ISpaceAdvertinsingReportDA
    {
        List<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> f_ListarReporteEspaciosPublicitariosDA(string ps_inmueble, string ps_ejecutivo, Int32 ps_tipoProducto, Int32 ps_estado);
        List<DIO_PUB_T_ESPACIO_OCUP_ESTADO> f_ListarEstadoEspacioPublicitarioDA();
    }

    public class SpaceAdvertinsingReportDA : ISpaceAdvertinsingReportDA
    {
        public void Dispose()
        {
            GC.Collect();
        }

              


        public List<DIO_PUB_T_ESPACIO_OCUP_ESTADO> f_ListarEstadoEspacioPublicitarioDA()
        {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return (from x in contexto.DIO_PUB_T_ESPACIO_OCUP_ESTADO
                            select x
                            ).ToList();
                }
            
        }

        public List<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> f_ListarReporteEspaciosPublicitariosDA(string ps_inmueble, string ps_ejecutivo, Int32 ps_tipoProducto, Int32 ps_estado)
        {
            try
            {
                using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
                {
                    return contexto.DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS(ps_inmueble, ps_ejecutivo, ps_estado, ps_tipoProducto).ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

       
    }
}
