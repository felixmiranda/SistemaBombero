using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer;

namespace BOM.BusinessLayer
{
    public interface IInmuebleBL
    {
        List<ADV_T_INMUEBLE> ListarInmueblesRealPlazaBL();
        List<LISTA_INMUEBLES_COLABORADOR_Result> ListarInmueblesPorColaboradorBL(string codigoColaborador);
    }


    public class InmuebleBL : IInmuebleBL
    {
        public List<ADV_T_INMUEBLE> ListarInmueblesRealPlazaBL()
        {
            return new InmuebleDA().ListarInmueblesRealPlazaDA();
        }
        public List<LISTA_INMUEBLES_COLABORADOR_Result> ListarInmueblesPorColaboradorBL(string codigoColaborador)
        {
            return new InmuebleDA().ListarInmueblesPorColaboradorDA(codigoColaborador);
        }

    }


}
