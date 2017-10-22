using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BOM.DataLayer;
namespace BOM.BusinessLayer
{
    public interface itemporalBL
    {
        DataTable obtenerEspaciosxfiltro(string _elementosSeleccionados, string _inmueblesSeleccionados,
             string _areaDesde, string _areaHasta, int pi_idProducto, string ps_razonSocial);
        DataTable obtenerinfoespacio(int _idespacio);
        DataTable listarElementoActivacionxproductoBL(int id_espacio);
    }

    public class temporalBL : itemporalBL
    {
        temporalDA objdao = new temporalDA();


        public DataTable obtenerEspaciosxfiltro(string _elementosSeleccionados, string _inmueblesSeleccionados,
            string _areaDesde, string _areaHasta, int pi_idProducto, string ps_razonSocial)
        {
            return objdao.obtenerEspaciosxfiltro(_elementosSeleccionados, _inmueblesSeleccionados,
                _areaDesde,_areaHasta,pi_idProducto,ps_razonSocial);
        }
        public DataTable obtenerinfoespacio(int _idespacio)
        {
            return objdao.obtenerinfoespacio(_idespacio);
        }
        public DataTable listarElementoActivacionxproductoBL(int id_espacio)
        {
            return objdao.listarElementoActivacionxproductoDA(id_espacio);
        }
    }
}
