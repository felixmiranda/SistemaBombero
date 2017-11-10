using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOM.EntityLayer;
using BOM.EntityLayer.Interfaces.Reserve;
using BOM.BusinessLayer.Interfaces.Reserve;
using BOM.BusinessLayer;
using BOM.UIGeneral;
using System.Text;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Reflection;
using System.Drawing;
using excel = Microsoft.Office.Interop.Excel;
namespace BOM.UserLayer.Interfaces.Reserve
{
    public partial class frmSpaceAdvertinsingReport : System.Web.UI.Page
    {
        #region "VARIABLES Y PROPIEDADES"
        IApproveSpaceSoldBL objAprob = new ApproveSpaceSoldBL();
        IReserveSpaceAdvertisingBL objReser = new ReserveSpaceAdvertisingBL();
        ISpaceAdvertinsingReportBL objReport = new SpaceAdvertinsingReportBL();
        IInmuebleBL objInmueble = new InmuebleBL();
        
        private ReserveSpaceAdvertisingBL _blReserveSpace = null;

        private IList<DIO_PUB_T_RESERVA> reservasVendidas
        {
            get { return (IList<DIO_PUB_T_RESERVA>)ViewState["vsreservasVendidas"]; }
            set { ViewState["vsreservasVendidas"] = value; }
        }

        private IList<int> listreservasConCruce
        {
            get { return (IList<int>)ViewState["vslistreservasConCruce"]; }
            set { ViewState["vslistreservasConCruce"] = value; }
        }
        private DataTable _dt;

        public DataTable dt
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        }
        #endregion
        #region "METODOS Y FUNCIONES"

       
        private void m_ListarEjecutivo()
        {
            IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> lista = objReser.f_listar_ejecutivosBTLBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "usua_c_cdoc_id", "nombCompletoEjecutivo", this.ddlEjecutivo, "Ejecutivo de Ventas", "0");
            }
        }

        private void m_ListarEstadoReserva() {
            IList<DIO_PUB_T_ESPACIO_OCUP_ESTADO> listaEstado = objReport.f_ListarEstadoEspacioPublicitario();
            if (listaEstado.Count > 0)
            {
                UIPage.Fill(listaEstado, "esp_ocu_est_c_iid", "esp_ocu_est_c_vnomb", this.ddlEstado, "Estado", "0");
            }
        }
        void buscar()
        {
            string sEjecutivo = this.ddlEjecutivo.SelectedItem.Value.ToString();
            string sInmueble = this.ddlInmueble.SelectedValue.ToString();
            Int32 iTipoProducto = Convert.ToInt32( this.ddlProducto.SelectedItem.Value.ToString());
            Int32 iEstado = Convert.ToInt32( this.ddlEstado.SelectedItem.Value.ToString());

            IList<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> lista = objReport.f_ListaReporteEspaciosPublicitariosBL(sInmueble, sEjecutivo, iTipoProducto, iEstado);

            this.gvReservas.DataSource = lista;
            this.gvReservas.DataBind();
          
        }

        void ExportarExcel() {
            string sEjecutivo = this.ddlEjecutivo.SelectedItem.Value.ToString();
            string sInmueble = this.ddlInmueble.SelectedValue.ToString();
            Int32 iTipoProducto = Convert.ToInt32(this.ddlProducto.SelectedItem.Value.ToString());
            Int32 iEstado = Convert.ToInt32(this.ddlEstado.SelectedItem.Value.ToString());

            IList<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> lista = objReport.f_ListaReporteEspaciosPublicitariosBL(sInmueble, sEjecutivo, iTipoProducto, iEstado);

          
          ListtoDataTableConverter converter = new ListtoDataTableConverter();
          dt = converter.ToDataTable(lista.ToList());
         
            
            string ruta = @"D:\Temp\ReporteExcel"+"_" + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".xlsx";
          WriteToExcel(dt, ruta);

        }
        void f_cargarComboProducto()
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<ADV_T_PUB_PRODUCTO> lista = _blReserveSpace.f_listar_pub_productosBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "pub_prod_c_iid", "pub_prod_c_vnomb", ddlProducto, "", "");
            }

        }
        void f_cargarComboInmueble()
        {
            var objUsuario = (SGA_T_USUARIO)Session["sga_t_usuario"];
            var iCodPerfil = (int)Session["S_COD_PERFIL"];
            objInmueble = new InmuebleBL();

            List<LISTA_INMUEBLES_COLABORADOR_Result> lista = null;
            if (objUsuario != null)
            {
                lista = objInmueble.ListarInmueblesPorColaboradorBL(objUsuario.usua_c_cusu_red, iCodPerfil);
                if (lista.Count > 0)
                {
                    UIPage.Fill(lista, "inm_c_icod", "inm_c_vnomb", ddlInmueble, "Inmueble", "0");
                }
            }
            
        }
        public void m_CrearExcel(DataTable pobj_dt, string ps_Nombre)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(pobj_dt);

                //Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + ps_Nombre + "_" + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        #endregion
        #region "SERVICIO WEB"
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] metodAutocompletarInmueble(string prefixText, int count, string contextKey)
        {
            IInmuebleBL blInmueble = new InmuebleBL();
            List<ADV_T_INMUEBLE> lista = blInmueble.ListarInmueblesRealPlazaBL();
            return (from x in lista
                    where x.inm_c_vnomb.Contains(prefixText.ToUpper())
                    select x.inm_c_vnomb).Take(count).ToArray();
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack){
                f_cargarComboInmueble(); 
                f_cargarComboProducto();
                m_ListarEjecutivo();
                m_ListarEstadoReserva();
                this.gvReservas.DataSource = "";
                this.gvReservas.DataBind();
                
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExportarExcel);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

            // Confirms that an HtmlForm control is rendered for

        }
        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //ExportarExcel();
            var fecha = DateTime.Now.ToString("yyyMMdd mm:ss");
            var nombreArchivo = string.Concat("ReporteEspacios_", fecha, ".xls");
            if (gvReservas.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", nombreArchivo));
                Response.ContentType = "application/vnd.xls";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
                Response.Charset = "utf-8";
                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                gvReservas.HeaderRow.Style.Add("background-color", "#ffffff");
                
                for (int i = 0; i < gvReservas.HeaderRow.Cells.Count; i++)
                {
                    gvReservas.HeaderRow.Cells[i].Style.Add("background-color", "#237b96");
                    gvReservas.HeaderRow.Cells[i].Style.Add("color", "#fff");
                }
                gvReservas.RenderControl(tw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                lblmensajeAccion.Text = "No se encontró registros en la búsqueda";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
            }

        }
        private void WriteToExcel(System.Data.DataTable dt, string location)
        {
            //instantiate excel objects (application, workbook, worksheets)
            excel.Application XlObj = new excel.Application();
            XlObj.Visible = false;
            excel._Workbook WbObj = (excel.Workbook)(XlObj.Workbooks.Add(""));
            excel._Worksheet WsObj = (excel.Worksheet)WbObj.ActiveSheet;

            //run through datatable and assign cells to values of datatable
            try
            {
                int row = 1; int col = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    //adding columns
                    WsObj.Cells[row, col] = column.ColumnName;
                    col++;
                }
                //reset column and row variables
                col = 1;
                row++;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //adding data
                    foreach (var cell in dt.Rows[i].ItemArray)
                    {
                        WsObj.Cells[row, col] = cell;
                        col++;
                    }
                    col = 1;
                    row++;
                }
                WbObj.SaveAs(location);
            }
         
            catch (Exception ex)
            {
                
            }
            finally
            {
                WbObj.Close();
            }
        }
    
    }

    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable("Reporte");
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}