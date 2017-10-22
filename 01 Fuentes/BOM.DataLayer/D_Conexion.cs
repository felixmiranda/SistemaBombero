using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace BOM.DataLayer
{
    public class D_Conexion
    {
        public static string conx
        {
            get
            {
                //return ConfigurationManager.ConnectionStrings["adv"].ConnectionString; //"Server=10.56.205.172;Database=ADVDB_MM;uid=sa;Password=HoremHabX11;Connection Timeout = 20000";
                return "Server=10.56.205.172;Database=ADVDB_MM;uid=sa;Password=HoremHabX11;Connection Timeout = 20000";
            }
        }
    }
}

