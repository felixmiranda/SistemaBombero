using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaBombero.Control_de_Usuario
{
    public partial class CUMensajeInformacion : System.Web.UI.UserControl
    {        
        #region DECLARACIONES

        #endregion

        #region METODOS
        /// <summary>
        /// Descripción: Recibe los mensaje y los escribe
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_Titulo"></param>
        /// <param name="ps_Mensaje"></param>
        public void m_EscribirMensaje(string ps_Titulo, string ps_Mensaje)
        {
            lblCUTitulo.Text = ps_Titulo == string.Empty ? "Información" : ps_Titulo;
            lblCUMensaje.Text = ps_Mensaje;
        }
        #endregion

        #region EVENTOS
        /// <summary>
        /// Descripción: Evento load
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

    }
}