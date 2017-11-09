using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using BOM.DataLayer;

namespace BOM.DataLayer
{
    public interface IInmuebleDA
    {
        List<ADV_T_INMUEBLE> ListarInmueblesRealPlazaDA();
        List<LISTA_INMUEBLES_COLABORADOR_Result> ListarInmueblesPorColaboradorDA(string codigoColaborador);
    }


    public class InmuebleDA : IInmuebleDA
    {

        public List<ADV_T_INMUEBLE> ListarInmueblesRealPlazaDA()
        {
            using (BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return (from x in contexto.ADV_T_INMUEBLE
                        where x.inm_c_bactivo == true && x.inm_c_vnomb.Contains("ReaL Plaza")
                        select x).ToList();
            }
        }
        public List<LISTA_INMUEBLES_COLABORADOR_Result> ListarInmueblesPorColaboradorDA(string codigoColaborador)
        {
            using(BD_DIONISIOEntities contexto = new BD_DIONISIOEntities())
            {
                return contexto.SP_ADV_OBTENER_LISTA_INMUEBLES_COLABORADOR(codigoColaborador).ToList();
            }
        }
        //table.Where(t => uids.Contains(t.uid));
    }


}
