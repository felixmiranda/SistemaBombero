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
    public partial class frmMyReservations : System.Web.UI.Page
    {
        #region "VARIABLES Y PROPIEDADES"
        IMyReservationsBL myresBL = new MyReservationsBL();
        IReserveSpaceAdvertisingBL objReser = new ReserveSpaceAdvertisingBL();


        private SGA_T_USUARIO objUsuario
        {
            get { return (SGA_T_USUARIO)ViewState["vsObjUsuario"]; }
            set { ViewState["vsObjUsuario"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.gvReservas.DataSource = "";
                this.gvReservas.DataBind();

                objUsuario = (SGA_T_USUARIO)Session["SGA_T_USUARIO"];
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }
        protected void gvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lkReservaMaster = (LinkButton)e.Row.FindControl("lkReservaMaster");

                    int iMostrar = Convert.ToInt16(gvReservas.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values["MARCAR"]);

                    if (lkReservaMaster != null)
                    {
                        lkReservaMaster.Visible = Convert.ToBoolean(iMostrar);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void buscar()
        {
            try
            {
                IList<DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR_Result> lista=new List<DIO_SP_PUB_RESERVA_PENDIENTE_XEJECUTIVO_LISTAR_Result>();
                int iPerfil = Convert.ToInt32(Session["S_COD_PERFIL"]);
                if (iPerfil == Convert.ToInt32(IEnum.Perfiles.Administrador) || iPerfil == Convert.ToInt32(IEnum.Perfiles.Coordinador_BTL))
                {
                    lista = myresBL.f_ListarReservasPendientesXEjecutivoBL(this.txtInmueble.Text.Trim(), this.txtCliente.Text.Trim(),"");
                }
                else
                {
                    lista = myresBL.f_ListarReservasPendientesXEjecutivoBL(this.txtInmueble.Text.Trim(), this.txtCliente.Text.Trim(),
                    objUsuario.usua_c_cdoc_id);
                }
                this.gvReservas.DataSource = lista;
                this.gvReservas.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] metodAutocompletarCliente(string prefixText, int count, string contextKey)
        {
            IApproveSpaceSoldBL objAprob = new ApproveSpaceSoldBL();

            List<ADV_T_CLIENTE> lista = objAprob.f_ListarClientesBL();
            return (from x in lista
                    where x.cli_c_vraz_soc.Contains(prefixText.ToUpper())
                    select x.cli_c_vraz_soc).Take(count).ToArray();
        }
        #endregion

        protected void gvReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editarReserva")
                {
                    UISeguridad obj = new UISeguridad();
                    string idMaster = gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["reser_mast_c_iid"].ToString();
                    string codEjecutivo = gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["ejec_c_cdoc_id"].ToString();
                    string fdesde = Convert.ToDateTime(gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["reser_c_dfech_inicio"]).ToShortDateString();
                    string fhasta = Convert.ToDateTime(gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["reser_c_dfech_fin"]).ToShortDateString();
                    Response.Redirect("../Reserve/frmReserveSpaceAdvertising.aspx?codMaster=" + obj.f_Encriptar(idMaster) + "&codEjec=" + obj.f_Encriptar(codEjecutivo) + "&fDesde=" + fdesde + "&fHasta=" + fhasta);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}