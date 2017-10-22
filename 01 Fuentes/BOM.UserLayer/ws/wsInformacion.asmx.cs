using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using BOM.BusinessLayer.ws;
using BOM.UIGeneral;
namespace BOM.UserLayer.ws
{
    /// <summary>
    /// Summary description for wsInformacion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsInformacion : System.Web.Services.WebService
    {
        #region VARIABLES GLOBALES
        wsBomberoBL objBL;
        UISeguridad objSeg;
        #endregion
        [WebMethod]
        public DataSet metodEspacioReservado(string idEspacioEncriptado)
        {
            objBL = new wsBomberoBL();
            objSeg = new UISeguridad();
            int idEspacio = Convert.ToInt32(objSeg.f_Desencriptar(idEspacioEncriptado));

            DataSet ds = new DataSet();
            DataTable dataT = new DataTable();

            dataT.Columns.Add("codigo");
            dataT.Columns.Add("descripcion");

            int result = objBL.f_EspacioReservadoBL(idEspacio);

            DataRow fila = null;
            fila = dataT.NewRow();

            fila["codigo"] = result;
            if (result == 0)
            {
                fila["descripcion"] = "No hay reserva registrada para este espacio publicitario";
                dataT.Rows.Add(fila);
            }
            else if (result == 1)
            {
                fila["descripcion"] = "Hay reserva registrada para este espacio publicitario";           
                dataT.Rows.Add(fila);
            }
            if (result == -1)
            {
                fila["descripcion"] = "Hubo un error en el servicio web de BOMBERO, comuniquese con Service Desk";
                dataT.Rows.Add(fila);
            }

            ds.Tables.Add(dataT);
            ds.Tables[0].TableName = "reserva";
       
            return ds;
        }
    }
}
