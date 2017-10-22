using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOM.BusinessLayer;
using BOM.EntityLayer;
using BOM.UIGeneral;
using System.Globalization;
using System.Text;

namespace BOM.UserLayer.MasterPage.Principal
{
    public partial class Site : System.Web.UI.MasterPage
    {
        SGA_T_USUARIO objUsuario = new SGA_T_USUARIO();

        #region METODOS Y FUNCIONES CREADOS POR EL USUARIO
        /// <summary>
        /// Descripción: Metodo que envia los parametros a la BD y retorna el menu con los niveles
        /// Autor: Miguel Martinez Q RP0945
        /// Fecha y Hora Creación: 25/02/2017
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        protected void m_ObtenenMenu()
        {
            var objUsuario = (SGA_T_USUARIO)Session["sga_t_usuario"];
            lblNombreUsuario.Text = objUsuario.usua_c_cape_nombres.ToUpper();// +" " + objUsuario.usua_c_cape_pat.ToUpper();
            // lblusuario.InnerText = objUsuario.usua_c_cape_nombres.ToUpper();
            string sCuentaLoguin = objUsuario.usua_c_cdoc_id;
            int iSistema = Convert.ToInt32(IEnum.Sistema.Bombero);
            SeguridadBL objSeguridadBL = new SeguridadBL();
            StringBuilder sbMenu = new StringBuilder();
            try
            {
                var objListaPadres = objSeguridadBL.ObtenerMenuPadres(sCuentaLoguin, iSistema);
                foreach (SGA_T_MENU m in objListaPadres)
                {
                    //if (m.IIdPadre == 0)
                    //{
                    sbMenu.Append("<li class='mm-dropdown'>");
                    sbMenu.Append("<a href='javascript:void(0);'><i class='" + m.menu_c_vpag_asp2 + "'></i><span class='mm-text'>" + m.menu_c_vnomb + "</span></a>");
                    sbMenu.Append("<ul>");

                    var objListaHijos = objSeguridadBL.ObtenerMenuHijos(sCuentaLoguin, m.menu_c_iid, iSistema);

                    if (objListaHijos.Count() > 0)
                    {
                        foreach (var hijo in objListaHijos)
                        {
                            sbMenu.Append("<li>");
                            //sbMenu.Append("<a tabindex='-1' href='" + ResolveClientUrl("~" + mh.SPagAsp) + "'><i class='"+ mh.SPagAsp2 +"'></i><span class='mm-text'>"+ mh.SNombre +"</span></a>");
                            sbMenu.Append(string.Format("<a tabindex='-1' href='../..{0}'><div style='float: left;height: 25px;'><i class='{1}'></i></div><div><span class='mm-text'>{2}</span></div></a>", hijo.menu_c_vpag_asp, hijo.menu_c_vpag_asp2, hijo.menu_c_vnomb));
                            sbMenu.Append("</li>");
                        }
                    }
                    sbMenu.Append("</ul>");
                    sbMenu.Append("</li>");
                    //}
                }
                litMenuPrincipal.Text = sbMenu.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
                //ClsTrace.m_TraceError(ClsConstantesCore.K_APPNAME, "--", Environment.MachineName, ex);
            }
            //return sbMenu;




        }
        #endregion

        #region EVENTOS DEL SISTEMA
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SGA_T_USUARIO"] == null)
                {
                    Response.Redirect("../../Entry/Access/frmLogin.aspx");
                }
                else
                {
                    //SeguridadBL ObjSeguridadBL = new SeguridadBL();
                    //var objUsuario = (SGA_T_USUARIO)Session["SGA_T_USUARIO"];
                    //UIFunciones objUIFunciones = new UIFunciones();
                    //int pi_sistema = Convert.ToInt32(IEnum.Sistema.Bombero);
                    //if (!(objUIFunciones.m_validarAcceso(objUsuario.usua_c_cdoc_id, pi_sistema)))
                    //{
                    //    Response.Redirect(@"../../Interfaces/Restringido/frmAccesoRestringido.aspx", false);
                    //}
                }
                m_ObtenenMenu();
            }
        }
        protected void lnkbtnSalir_Click(object sender, EventArgs e)
        {
            //c_Util objUtil = new c_Util();
            //objUtil.f_InsertarBitacora(c_Util.TipoAccion.Salida);
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session["SGA_T_USUARIO"] = null;
            Response.Redirect("../../Entry/Access/frmLogin.aspx");
        }
        #endregion
    }
}
