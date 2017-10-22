using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOM.UIGeneral;
using BOM.BusinessLayer;
using BOM.EntityLayer;

namespace SistemaBombero.ControlUsuario
{
    public partial class CUMenuDinamico : System.Web.UI.UserControl
    {
        ISeguridadBL ObjSeguridadBL = null;
        public CUMenuDinamico()
        {
            ObjSeguridadBL = new SeguridadBL();
        }

        #region EVENTOS DEL SISTEMA
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region METODOS Y FUNCIONES
        /// <summary>
        /// Descripción: Metodo para cargar el menu
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="strLogin"></param>
        /// <param name="intSistema"></param>
        public void CargarMenu(string strLogin, int intSistema)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    AgregarMenusPadres(strLogin, intSistema);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Descripción: Metodo paa agregar los menu padre
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="strUsuario"></param>
        /// <param name="intSistema"></param>
        public void AgregarMenusPadres(string strUsuario, int intSistema)
        {
            List<SGA_T_MENU> ListaMenu = null;

            try
            {
                ListaMenu = new List<SGA_T_MENU>(ObjSeguridadBL.ObtenerMenuPadres(strUsuario, intSistema));

                foreach (SGA_T_MENU menu in ListaMenu)
                {
                    MenuItem mnuPadre = new MenuItem("      " + menu.menu_c_vnomb + "      ", menu.menu_c_iid.ToString(), "");

                    mnuPadre.Selectable = false;
                    //Agrego al Menu Principal
                    this.MnDinamico.Items.Add(mnuPadre);
                    //llamo a la rutina que Agrega los Hijos
                    AgregarMenusHijos(ref mnuPadre, strUsuario, intSistema);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Descripción: Metodo para agregar los menu hijos
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: 04/09/2015
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="mnuItem"></param>
        /// <param name="strUsuario"></param>
        /// <param name="intSistema"></param>
        public void AgregarMenusHijos(ref MenuItem mnuItem, string strUsuario, int intSistema)
        {
            List<SGA_T_MENU> ListaMenu = null;

            try
            {
                ListaMenu = new List<SGA_T_MENU>(ObjSeguridadBL.ObtenerMenuHijos(strUsuario, Convert.ToInt32(mnuItem.Value), intSistema));

                if (ListaMenu != null)
                {
                    foreach (SGA_T_MENU menu in ListaMenu)
                    {
                        MenuItem mnuHijo = new MenuItem("&nbsp;&nbsp;&nbsp;" + menu.menu_c_vnomb,
                          menu.menu_c_iid.ToString(), "", "../" + menu.menu_c_vpag_asp);

                        //mnuHijo.ImageUrl = "~/Imagenes/menuItem.gif";
                        mnuItem.ChildItems.Add(mnuHijo);

                        if (menu.menu_c_ynivel == 2 & menu.menu_c_vpag_asp == null)
                        {
                            mnuHijo.Selectable = false;
                            mnuHijo.NavigateUrl = "#";
                        }
                        //llamo a la rutina que agrega los hijos               
                        AgregarMenusHijos(ref mnuHijo, strUsuario, intSistema);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}