using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaBombero.ControlUsuario
{
    public partial class CUOperacionesGenericas : System.Web.UI.UserControl
    {
        #region DECLARACIONES

        #endregion

        #region METODOS

        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "Procesando objetos sin cerrar el modal";
        }
        #endregion
    }
}