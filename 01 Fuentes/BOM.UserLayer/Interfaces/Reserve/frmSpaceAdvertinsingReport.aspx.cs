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

namespace BOM.UserLayer.Interfaces.Reserve
{
    public partial class frmSpaceAdvertinsingReport : System.Web.UI.Page
    {
        #region "VARIABLES Y PROPIEDADES"
        IApproveSpaceSoldBL objAprob = new ApproveSpaceSoldBL();
        IReserveSpaceAdvertisingBL objReser = new ReserveSpaceAdvertisingBL();
        ISpaceAdvertinsingReportBL objReport = new SpaceAdvertinsingReportBL();
        
        
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

        #endregion
        #region "METODOS Y FUNCIONES"

        //void buscar()
        //{
        //    string sEjecutivo = this.ddlEjecutivo.SelectedItem.Value.ToString();
        //    IList<DIO_SP_PUB_APROB_RESERVA_LISTAR_Result> lista = objAprob.f_ListarAprobacionesReservaBL(this.txtInmueble.Text.Trim(), this.txtInmueble.Text.Trim(),
        //        sEjecutivo);

        //    this.gvReservas.DataSource = lista;
        //    this.gvReservas.DataBind();
        //}
        private void m_ListarEjecutivo()
        {
            IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> lista = objReser.f_listar_ejecutivosBTLBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "usua_c_cdoc_id", "nombCompletoEjecutivo", this.ddlEjecutivo, "SELECCIONE", "0");
            }
        }

        private void m_ListarEstadoReserva() {
            IList<DIO_PUB_T_ESPACIO_OCUP_ESTADO> listaEstado = objReport.f_ListarEstadoEspacioPublicitario();
            if (listaEstado.Count > 0)
            {
                UIPage.Fill(listaEstado, "esp_ocu_est_c_iid", "esp_ocu_est_c_vnomb", this.ddlEstado, "SELECCIONE", "0");
            }
        }
        void buscar()
        {
            string sEjecutivo = this.ddlEjecutivo.SelectedItem.Value.ToString();
            string sInmueble = this.txtInmueble.Text.ToString();
            Int32 iTipoProducto = Convert.ToInt32( this.ddlProducto.SelectedItem.Value.ToString());
            Int32 iEstado = Convert.ToInt32( this.ddlEstado.SelectedItem.Value.ToString());

            IList<DIO_SP_PUB_REPORTE_ESPACIOS_PUBLICITARIOS_Result> lista = objReport.f_ListaReporteEspaciosPublicitariosBL(sInmueble, sEjecutivo, iTipoProducto, iEstado);

            this.gvReservas.DataSource = lista;
            this.gvReservas.DataBind();
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

                f_cargarComboProducto();
                m_ListarEjecutivo();
                m_ListarEstadoReserva();
                this.gvReservas.DataSource = "";
                this.gvReservas.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            buscar();
        }

        protected void gvReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvReservas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}