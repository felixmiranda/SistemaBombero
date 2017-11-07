using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Configuration;

using BOM.BusinessLayer;
using BOM.EntityLayer;
using BOM.UIGeneral;
using BOM.UserLayer;

namespace BOM.UserLayer.Entry.Access
{
    public partial class frmLogin : System.Web.UI.Page
    {
        #region PROPIEDADES 
        //hmedina - RPEXT094 - 28/06/2016
        private string strNombre
        {
            get { return (string)ViewState["vsDANombre"]; }
            set { ViewState["vsDANombre"] = value; }
        }
        private string strCorreo
        {
            get { return (string)ViewState["vsDACorreo"]; }
            set { ViewState["vsDACorreo"] = value; }
        }
        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {


            List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> objUsuarioResult = new List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result>();
            objUsuarioResult = f_ObtenenUsuario(txtUsuario.Text.Trim(), Convert.ToInt32(IEnum.Sistema.Bombero));

            if (objUsuarioResult.Count < 1)
            {
                m_MostrarError("Usuario no tiene permisos.");
            }
            else
            {
                if (f_ConsultarLoginAD())
                {
                    SGA_T_USUARIO objUsuario = new SGA_T_USUARIO();
                    objUsuario.usua_c_cusu_red = objUsuarioResult[0].usua_c_cusu_red;
                    //objUsuario.usua_c_bpropietarioadministrador = objUsuarioResult[0].usua_c_bpropietarioadministrador;
                    //objUsuario.usua_c_cidempresa = objUsuarioResult[0].usua_c_cidempresa;
                    objUsuario.usua_c_cape_pat = objUsuarioResult[0].usua_c_cape_pat;
                    objUsuario.usua_c_cape_mat = objUsuarioResult[0].usua_c_cape_mat;
                    objUsuario.usua_c_cape_nombres = objUsuarioResult[0].usua_c_cape_nombres;
                    objUsuario.usua_c_cdoc_id = objUsuarioResult[0].usua_c_cdoc_id;
                    objUsuario.usua_c_vcorreo1 = objUsuarioResult[0].usua_c_vcorreo;
                    objUsuario.usua_c_vnuevacont = objUsuarioResult[0].usua_c_vnuevacont;
                    Session["SGA_T_USUARIO"] = objUsuario;
                    SeguridadBL objSeguridadBL = new SeguridadBL();
                    int iCodPerfil = objSeguridadBL.f_ObtenerPerfilBL(objUsuario.usua_c_cdoc_id, Convert.ToInt32(IEnum.Sistema.Bombero));
                    Session["S_COD_PERFIL"] = iCodPerfil;
                    Response.Redirect("../../Interfaces/Default/Default.aspx");
                    //m_MensajeError(UIConstantes.ConsTituloMensajePopUp, "El usuario o contraseña no son correctos.");
                }
                else
                {
                    m_MostrarError("Usuario no pertenece al Directorio Activo Real Plaza");
                }
                    
            }
           
        }
        #endregion
        #region METODOS

        /// <summary>
        /// Descripción: Funcion para obtener las credenciales del usuario
        /// Autor: Jair Tasayco bautista RP0689
        /// Fecha y Hora Creación: 16-02-2017
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <param name="ps_Usuario"></param>
        /// <param name="ps_Contraseña"></param>
        private List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> f_ObtenenUsuario(string ps_Usuario, int pi_Sistema)
        {
            SGA_T_USUARIO objusuario = new SGA_T_USUARIO();
            List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result> objUsuarioObtenido = new List<SGA_SP_VALIDAR_USUARIO_DA_SELECCIONAR_Result>();
            objusuario.usua_c_cusu_red = ps_Usuario;
            objusuario.usua_c_vcontrasena = "";
            SeguridadBL objSeguridadBL = new SeguridadBL();
            objUsuarioObtenido = objSeguridadBL.f_ObtenerUsuarioDA_BL(objusuario, pi_Sistema);
            return objUsuarioObtenido;
        }


        /// <summary>
        /// Descripción: Obtiene Codigo de Objeto JSON
        /// Autor: Jair Tasayco bautista RP0689
        /// Fecha y Hora Creación: 16-02-2017
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        public bool f_ConsultarLoginAD()
        {
            bool bEstado = false;
            string s_BaseURL = WebConfigurationManager.AppSettings["RutaURLSWLogin"].ToString() + "&json={'ps_userName':'" + this.txtUsuario.Text.Trim() + "','ps_password':'" + this.txtContrasena.Text.Trim() + "'}";

            WebClient n = new WebClient();
            var json = n.DownloadString(s_BaseURL);

            JObject o = JObject.Parse(json);

            foreach (var item in o)
            {
                if (item.Key == "metodAtlasLoginResult")
                {
                    var i_CodigoAcesso = item.Value["diffgr:diffgram"]["NewDataSet"]["AD"]["CODIGO"];
                    if (i_CodigoAcesso.ToString() == "100")
                    {
                        strNombre = item.Value["diffgr:diffgram"]["NewDataSet"]["AD"]["NOMBRE_COMPLETO"].ToString();
                        strCorreo = item.Value["diffgr:diffgram"]["NewDataSet"]["AD"]["CORREO"].ToString();
                        bEstado = true;
                    }
                    break;
                }
            }

            //bEstado = true; //solo para pruebas locales
            return bEstado;
        }
        public void m_MostrarError(String ps_Mensaje)
        {
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "error", ClsUtilCore.UIMensajes.f_ObtenerScriptErrorSimple(ps_Mensaje));
        }
        #endregion
        
    }
}