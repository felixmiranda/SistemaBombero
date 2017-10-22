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
    }


    public class InmuebleBL : IInmuebleBL
    {
        public List<ADV_T_INMUEBLE> ListarInmueblesRealPlazaBL()
        {
            return new InmuebleDA().ListarInmueblesRealPlazaDA();
        }

    }


}
