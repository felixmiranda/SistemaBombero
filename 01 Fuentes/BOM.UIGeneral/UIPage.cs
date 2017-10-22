using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BOM.EntityLayer;
using System.IO;

namespace BOM.UIGeneral
{
    public class UIPage : Page
    {
        #region Events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string str_Url = Path.Combine(Request.ApplicationPath, "../../Ingreso/Acceso/frmIngreso.aspx");

            try
            {
                if (Context.Session == null)
                {
                    Response.Redirect(str_Url);
                }
            }
            catch (NullReferenceException ex)
            {
                if (Context.Session == null)
                {
                    Response.Redirect(str_Url);
                }
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static void Fill(Object Lst, String ValueField, String TextField, DropDownList Cbo, String strEmptyText, String strEmptyValue)
        {
            Cbo.Items.Clear();
            Cbo.DataSource = Lst;
            Cbo.DataValueField = ValueField;
            Cbo.DataTextField = TextField;

            if (strEmptyText.Length != 0 && strEmptyValue.Length != 0)
            {
                Cbo.Items.Add(new ListItem(strEmptyText, strEmptyValue));
            }

            if (Lst != null)
            {
                Cbo.AppendDataBoundItems = true;
                Cbo.DataBind();
                Cbo.AppendDataBoundItems = false;
            }

        }

        public static void Fill(Object Lst, String ValueField, String TextField, CheckBoxList Cbo, String strEmptyText, String strEmptyValue)
        {
            Cbo.Items.Clear();
            Cbo.DataSource = Lst;
            Cbo.DataValueField = ValueField;
            Cbo.DataTextField = TextField;

            if (strEmptyText.Length != 0 && strEmptyValue.Length != 0)
            {
                Cbo.Items.Add(new ListItem(strEmptyText, strEmptyValue));
            }

            if (Lst != null)
            {
                Cbo.AppendDataBoundItems = true;
                Cbo.DataBind();
                Cbo.AppendDataBoundItems = false;
            }

        }
    }
}
