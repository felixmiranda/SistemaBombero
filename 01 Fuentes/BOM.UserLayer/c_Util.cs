using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOM.EntityLayer;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;


namespace BOM.UserLayer
{
    public class c_Util : Page
    {
        public enum TipoAccion
        {
            Ingreso = 0,
            Salida = 1
        }
        /// <summary>
        /// Descripción: Permite insertar una bitacora en la Bd local Taurus
        /// Autor: Jair Tasayco Bautista
        /// Fecha y Hora Creación: 2016-11-28
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <returns></returns>
        //public bool f_InsertarBitacora(c_Util.TipoAccion pi_TipoAccion)
        //{
        //    string sRegistraAuditoria = WebConfigurationManager.AppSettings["RegistraAuditoria"];
        //    if (sRegistraAuditoria == "0")
        //    {
        //        return false;
        //    }

        //    bool bEstado = false;
        //    string DANombre;
        //    try
        //    {
        //        SGA_T_USUARIO objUser = new SGA_T_USUARIO();
        //        objUser = (SGA_T_USUARIO)Session["SGA_T_USUARIO"];


        //        string pi_CodAplicacion = "8";
        //        string pi_CodTransaccion = "";
        //        string ps_Descripcion = "";

        //        if (c_Util.TipoAccion.Ingreso == pi_TipoAccion)
        //        {
        //            pi_CodTransaccion = "15";
        //            ps_Descripcion = "INGRESO A PORTAL DE PROVEEDORES";
        //        }
        //        else if (c_Util.TipoAccion.Salida == pi_TipoAccion)
        //        {
        //            pi_CodTransaccion = "16";
        //            ps_Descripcion = "CIERRE DE SESIÓN DEL PORTAL DE PROVEEDORES";
        //        }

        //        string ps_CodUsuario = objUser.usua_c_cusu_red;

        //        string ps_IpCliente = Server.HtmlEncode(HttpContext.Current.Request.UserHostAddress);
        //        string ps_HostNameCliente = HttpContext.Current.Request.UserHostName;
        //        string ps_HostNameServer = Dns.GetHostName();//DONDE ESTA LA APLICACION

        //        string s_BaseURL = WebConfigurationManager.AppSettings["RutaURLSWAuditoria"].ToString() +
        //            "&json={'pi_CodAplicacion':'" + pi_CodAplicacion + "'," +
        //            "'pi_CodTransaccion':'" + pi_CodTransaccion + "'," +
        //            "'ps_CodUsuario':'" + ps_CodUsuario + "'," +
        //            "'ps_Descripcion':'" + ps_Descripcion + "'," +
        //            "'ps_IpCliente':'" + ps_IpCliente + "'," +
        //            "'ps_HostNameCliente':'" + ps_HostNameCliente + "'," +
        //            "'ps_HostNameServer':'" + ps_HostNameServer + "'}";

        //        WebClient n = new WebClient();
        //        var json = n.DownloadString(s_BaseURL);

        //        JObject o = JObject.Parse(json);

        //        foreach (var item in o)
        //        {
        //            if (item.Key == "metodAudInsertarAuditoriaResult")
        //            {
        //                //no se necesita leer la respuesta
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }


        //    return bEstado;
        //}


    }

}