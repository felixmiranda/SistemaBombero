using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BOM.UserLayer.Interfaces.Default
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SGA_T_USUARIO"] == null)
                {
                    Response.Redirect("../../Entry/Access/frmLogin.aspx");
                }

                bool? acceso = Request.QueryString["acceso"] == null ? (bool?)null : Convert.ToBoolean(Request.QueryString["acceso"]);

                if (acceso != null)
                {
                    if (!(bool)acceso)
                    {
                    }
                }

            }
            
        }
    }
}