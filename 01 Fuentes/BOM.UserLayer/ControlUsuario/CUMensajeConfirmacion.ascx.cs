using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaBombero.ControlUsuario
{
    public partial class CUMensajeConfirmacion : System.Web.UI.UserControl
    {
        #region DECLARACIONES
        public event Click_AceptarEventHandler Click_Aceptar;
        public delegate void Click_AceptarEventHandler();
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
            lblCUTitulo.Text = ps_Titulo == string.Empty ? "Confirmar la siguiente transacción" : ps_Titulo;
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
        protected void btnCUAceptar_Click(object sender, EventArgs e)
        {
            if (Click_Aceptar != null)
            {
                Click_Aceptar();
            }
        }
        #endregion
    }
}