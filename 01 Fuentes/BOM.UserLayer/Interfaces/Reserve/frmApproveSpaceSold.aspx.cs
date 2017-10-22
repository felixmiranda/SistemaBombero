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
    public partial class frmApproveSpaceSold : System.Web.UI.Page
    {
        #region "VARIABLES Y PROPIEDADES"
        IApproveSpaceSoldBL objAprob = new ApproveSpaceSoldBL();
        IReserveSpaceAdvertisingBL objReser = new ReserveSpaceAdvertisingBL();

       
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

        #region "EVENTOS"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                reservasVendidas = new List<DIO_PUB_T_RESERVA>();
                listreservasConCruce = new List<int>();
                m_ListarEjecutivo();
                this.gvReservas.DataSource = "";
                this.gvReservas.DataBind();
                ltvalidacionReservaVendido.Text = "";
                reservasVendidas.Clear();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
            ltvalidacionReservaVendido.Text = "";
        }
        #endregion

        #region "METODOS Y FUNCIONES"

        void buscar()
        {
            string sEjecutivo = this.ddlEjecutivo.SelectedItem.Value.ToString();
            IList<DIO_SP_PUB_APROB_RESERVA_LISTAR_Result> lista = objAprob.f_ListarAprobacionesReservaBL(this.txtInmueble.Text.Trim(), this.txtCliente.Text.Trim(),
                sEjecutivo);

            this.gvReservas.DataSource = lista;
            this.gvReservas.DataBind();
        }
        private void m_ListarEjecutivo()
        {
            IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> lista = objReser.f_listar_ejecutivosBTLBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "usua_c_cdoc_id", "nombCompletoEjecutivo", this.ddlEjecutivo, "SELECCIONE", "0");
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

        protected void gvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkEspacio = (CheckBox)e.Row.FindControl("chkEspacio");


                    RadioButton rbfacturarCliente = (RadioButton)e.Row.FindControl("rbFacturarXClientegv");
                    RadioButton rbfacturarAgencia = (RadioButton)e.Row.FindControl("rbFacturarXAgenciagv");
                    TextBox txtPrecioFinalgv = (TextBox)e.Row.FindControl("txtPrecioFinalgv");
                    

                    int iMostrar = Convert.ToInt16(gvReservas.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values["MARCAR"]);


                    decimal precioFinal = Convert.ToDecimal(gvReservas.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values["PRECIOFINAL"]);
                    string cadena_FacturarA = gvReservas.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values["FACTURARA"].ToString();

                    if (precioFinal == 0)
                    {
                        txtPrecioFinalgv.Text = "";
                    }


                    if (cadena_FacturarA !="")
                    {
                        if (cadena_FacturarA == "AGENCIA")
                        {
                            rbfacturarAgencia.Checked = true;
                        }
                        else if (cadena_FacturarA == "CLIENTE")
                        {
                            rbfacturarCliente.Checked = true;
                        }
                    }
                    if (chkEspacio != null)
                    {
                        chkEspacio.Visible = Convert.ToBoolean(iMostrar);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        bool HaySeleccionVendido()
        {
            bool ver = false;
            for (int i = 0; i < gvReservas.Rows.Count; i++)
            {
                CheckBox chkEspacio = (CheckBox)gvReservas.Rows[i].FindControl("chkEspacio");
                if (chkEspacio.Checked )
                {
                    ver = true;
                }
            }
            return ver;
        }
        void pintarReservasVenta()
        {
            for (int i = 0; i < gvReservas.Rows.Count; i++)
            {
              int idReserva = Convert.ToInt32(gvReservas.DataKeys[i]["reser_c_iid"]);
              if (reservaValida(idReserva))
              {
                  gvReservas.Rows[i].BackColor = System.Drawing.Color.FromArgb(207,196,196);
              }
            }
        }
        bool reservaValida(int idReserva)
        {
            bool ver = false;
            foreach (DIO_PUB_T_RESERVA objeto in reservasVendidas)
            {
                if (objeto.reser_c_iid == idReserva)
                {
                    ver = true;
                    break;
                }
            }
            return ver;
        }
        bool  f_validarDatos_Obligatorios_ReservaVenta()
        {
            bool ver= true;
            for (int i = 0; i < gvReservas.Rows.Count; i++)
            {
                int idReserva = Convert.ToInt32(gvReservas.DataKeys[i]["reser_c_iid"]);
                if (reservaValida(idReserva))
                {
                    RadioButton rbfacturarCliente = (RadioButton)gvReservas.Rows[i].FindControl("rbFacturarXClientegv");
                    RadioButton rbfacturarAgencia = (RadioButton)gvReservas.Rows[i].FindControl("rbFacturarXAgenciagv");
                    TextBox txtPrecioFinal = (TextBox)gvReservas.Rows[i].FindControl("txtPrecioFinalgv");

                    if (!f_PrecioFinalValido(txtPrecioFinal.Text.Trim()))
                    {
                        gvReservas.Rows[i].Cells[11].BackColor = System.Drawing.Color.FromArgb(208, 145, 145);
                        ver = false;
                    }
                    else
                    {
                        gvReservas.Rows[i].Cells[11].BackColor = System.Drawing.Color.FromArgb(207, 196, 196);
                    }
                    if (!f_FacturarPorValido(rbfacturarAgencia, rbfacturarCliente))
                    {
                        gvReservas.Rows[i].Cells[12].BackColor = System.Drawing.Color.FromArgb(208, 145, 145);
                        ver = false;
                    }
                    else
                    {
                        gvReservas.Rows[i].Cells[12].BackColor = System.Drawing.Color.FromArgb(207, 196, 196);
                    }
                }
            }
            return ver;
        }
        bool f_PrecioFinalValido(string txtPrecioFinal)
        {
            bool ver = true;
            try 
	        {	        
	        	if(txtPrecioFinal.Trim()== "")
                {
                    ver=false;
                    return ver;
                }
                else
                {
                    decimal temp=Convert.ToDecimal(txtPrecioFinal.Trim());

                    if (temp == 0)
                    {
                        ver = false;
                    }
                }
	        }
	        catch (Exception)
	        {
                ver=false;
	        } 
            return ver;
        }


        bool f_FacturarPorValido(RadioButton rbAgencia,RadioButton rbCliente)
        {
            return !rbAgencia.Checked && !rbCliente.Checked ? false: true;
        }
        
        protected void btnVendido_Click(object sender, EventArgs e)
        {
            try
            {
                if (HaySeleccionVendido())
                {
                    var sb = new StringBuilder();
                    ltvalidacionReservaVendido.Text = "";
                   
                    string mensaje = validarFechaMismoEspacio();
                    if (mensaje == "Se encontró precio final con formato incorrecto.")
                    {

                        lblmensajeAccion.Text = mensaje;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                    }
                    else
                    {
                        ViewState["vs_mensajeRealizarVenta"] = validarFechaMismoEspacio();
                        if (reservasVendidas.Count != 0)
                        {
                            //PINTAR RESERVAS VENDIDAS
                            pintarReservasVenta();

                            if (mensaje != "")
                            {
                                sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + mensaje + "</div>");
                                ltvalidacionReservaVendido.Text = sb.ToString();
                            }

                            if (f_validarDatos_Obligatorios_ReservaVenta())
                            {
                                //ABRE PANTALLA DE CONFIRMACION
                                if (mensaje != "") lblMensajeConfirmacion.Text = "¿Seguro de proceder a vender las reservas?, existe cruce de fechas con alguna de ellas, consúltelo en la parte inferior de la ventana.";
                                else lblMensajeConfirmacion.Text = "¿Seguro de proceder a vender las reservas?";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopConfirmacion();", true);
                            }
                            else
                            {
                                lblmensajeAccion.Text = "Debe ingresar el precio final del alquiler e indicar a quién se va a facturar.";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                            }
                        }
                        else// ninguna reserva para vender.
                        {
                            sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + mensaje + "</div>");
                            ltvalidacionReservaVendido.Text = sb.ToString();

                            lblmensajeAccion.Text = "Las reservas seleccionadas tienen cruce.";
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                        }
                    }
                }
                else
                {
                    lblmensajeAccion.Text = "No realizó ninguna selección";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                    this.btnpopAceptarAccion.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
         
        }

        string validarFechaMismoEspacio()
        {
            listreservasConCruce.Clear();
            string validacionTotal="";
            string validacionReserva = "";
            int idmasterAnterior = 0;
            bool reservaValida = false;
            reservasVendidas.Clear();

            //int 
            for (int i = 0; i < gvReservas.Rows.Count; i++)
            {
                CheckBox chkEspacio = (CheckBox)gvReservas.Rows[i].FindControl("chkEspacio");
                int idmaster= Convert.ToInt16(gvReservas.DataKeys[i].Values["reser_mast_c_iid"].ToString());

                if (chkEspacio.Checked && idmasterAnterior != idmaster)
                {
                    idmasterAnterior = idmaster;
                    reservaValida = true;
                }
                else
                {
                    if (idmasterAnterior == idmaster)
                    {
                        reservaValida = true;
                    }
                    else reservaValida = false;
                }


                if (reservaValida)
                {
                    int id_reserva = Convert.ToInt16(gvReservas.DataKeys[i].Values["reser_c_iid"]);
                    if (ReservaYaComparada(id_reserva)) continue;
                    else
                    {
                       string codigoEspacio = gvReservas.DataKeys[i].Values["pub_esp_c_vcod"].ToString();
                       Label lblEjecutivo1 = (Label)gvReservas.Rows[i].FindControl("lblEjecutivo");
                       string s_fecha_inicio = Convert.ToDateTime(gvReservas.DataKeys[i].Values["reser_c_dfech_inicio"]).ToShortDateString();
                       string s_fecha_fin = Convert.ToDateTime(gvReservas.DataKeys[i].Values["reser_c_dfech_fin"]).ToShortDateString();
                       int idespacio = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_esp_c_iid"].ToString());

                       RadioButton rbfacturarCliente = (RadioButton)gvReservas.Rows[i].FindControl("rbFacturarXClientegv");
                       RadioButton rbfacturarAgencia = (RadioButton)gvReservas.Rows[i].FindControl("rbFacturarXAgenciagv");
                       TextBox txtPrecioFinal = (TextBox)gvReservas.Rows[i].FindControl("txtPrecioFinalgv");


                       DateTime fechaInicio = Convert.ToDateTime(s_fecha_inicio);
                       DateTime fechaFin = Convert.ToDateTime(s_fecha_fin);

                       DIO_PUB_T_RESERVA objRes1 = new DIO_PUB_T_RESERVA();
                       objRes1.reser_mast_c_iid = idmaster;
                       objRes1.pub_esp_c_iid = idespacio;
                       objRes1.reser_c_dfech_inicio = fechaInicio;
                       objRes1.reser_c_dfech_fin = fechaFin;
                       objRes1.pub_esp_c_vcod = codigoEspacio;
                       objRes1.reser_c_iid = id_reserva;

                       if (!rbfacturarCliente.Checked && !rbfacturarAgencia.Checked) objRes1.reser_mast_c_fac_bagencia = null;
                       else if (rbfacturarAgencia.Checked)
                       {
                           objRes1.reser_mast_c_fac_bagencia = true;
                       }
                       else if (rbfacturarCliente.Checked)
                       {
                           objRes1.reser_mast_c_fac_bagencia = false;
                       }
                   
                      
                       validacionReserva = "";
                       validacionReserva = obtenerReservaMismoEspacioOtromaster(i, objRes1, lblEjecutivo1.Text);
                       validacionTotal += validacionReserva;
                       if (validacionReserva=="")
                       {

                           if (string.IsNullOrEmpty(txtPrecioFinal.Text.Trim()))
                           {
                               objRes1.reser_c_eprecio_alquiler = 0;
                           }
                           else
                           {
                               if (f_PrecioFinalValido(txtPrecioFinal.Text.Trim()))
                               {
                                   objRes1.reser_c_eprecio_alquiler = Convert.ToDecimal(txtPrecioFinal.Text.Trim());
                               }
                               else
                               {
                                   reservasVendidas.Clear();
                                   listreservasConCruce.Clear();
                                   ltvalidacionReservaVendido.Text = "";
                                   validacionTotal = "Se encontró precio final con formato incorrecto.";
                                   break;
                               }
                           }
                         reservasVendidas.Add(objRes1);//RESERVAS QUE PODRAN SER VENDIDAS.
                       }
                    }
                }
            }
            return validacionTotal;
        }
        bool ReservaYaComparada(int idreservaActual)
        {
            bool ver=false;
            if (listreservasConCruce.Count != 0)
            {
               foreach (int  idreserva in listreservasConCruce)
	           {
	           	 if(idreserva==idreservaActual)
                 {
                     ver=true;
                     break;
                 }
	           }
            } 
            return ver;
        }

        private string obtenerReservaMismoEspacioOtromaster(int rowIndex,DIO_PUB_T_RESERVA objRes1,string ejecutivoRes1)
        {
            string validacion="";

            string validacionReserva = "";

            DIO_PUB_T_RESERVA objRes2 = null;
            int idmasterAnterior = 0;
            bool reservaValida = false;
            for (int i = rowIndex+1; i < gvReservas.Rows.Count; i++)
            {
                CheckBox chkEspacio = (CheckBox)gvReservas.Rows[i].FindControl("chkEspacio");
                int idMasterActual = Convert.ToInt16(gvReservas.DataKeys[i].Values["reser_mast_c_iid"].ToString());
                int idespacioActual = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_esp_c_iid"].ToString());

                //PARA RECORRER SOLO LOS MASTER CON SUS RESPECTIVAS RESERVAS.
                if (chkEspacio.Checked && idmasterAnterior != idMasterActual)
                {
                    idmasterAnterior = idMasterActual;
                    reservaValida = true;
                }
                else
                {
                    if (idmasterAnterior == idMasterActual)
                    {
                        reservaValida = true;
                    }
                    else reservaValida = false;
                }

                if (reservaValida) 
                {
                   //logica de validacion
                    if (objRes1.pub_esp_c_iid == idespacioActual)
                    {
                        string s_fecha_inicio = Convert.ToDateTime(gvReservas.DataKeys[i].Values["reser_c_dfech_inicio"]).ToShortDateString();
                        string s_fecha_fin = Convert.ToDateTime(gvReservas.DataKeys[i].Values["reser_c_dfech_fin"]).ToShortDateString();
                        Label lblEjecutivo2= (Label)gvReservas.Rows[i].FindControl("lblEjecutivo");
                        DateTime fechaInicio = Convert.ToDateTime(s_fecha_inicio);
                        DateTime fechaFin = Convert.ToDateTime(s_fecha_fin);
                
                        objRes2 = new DIO_PUB_T_RESERVA();
                        objRes2.reser_mast_c_iid = idMasterActual;
                        objRes2.pub_esp_c_iid = idespacioActual;
                        objRes2.reser_c_dfech_inicio = fechaInicio;
                        objRes2.reser_c_dfech_fin = fechaFin;

                        validacionReserva = "";
                        validacionReserva = ObtenerCruceReservas(objRes1, objRes2, ejecutivoRes1, lblEjecutivo2.Text);
                        validacion += validacionReserva;
                        if (validacionReserva != "")
                        {
                            listreservasConCruce.Add(Convert.ToInt16(gvReservas.DataKeys[i].Values["reser_c_iid"]));//RESERVAS QUE TIENEN CRUCE.
                        }
                    }
                }
            }
            return validacion;
        }
        public string ObtenerCruceReservas(DIO_PUB_T_RESERVA objRes1, DIO_PUB_T_RESERVA objRes2, string ejecutivoRes1, string ejecutivoRes2)
        {
            string validarCruce = "";
            bool ver = false;
            if (objRes1.reser_c_dfech_inicio <= objRes2.reser_c_dfech_inicio && objRes2.reser_c_dfech_inicio <= objRes1.reser_c_dfech_fin)
            {
                ver = true;
            }
            else if (objRes1.reser_c_dfech_inicio <= objRes2.reser_c_dfech_fin && objRes2.reser_c_dfech_fin <= objRes1.reser_c_dfech_fin)
            {
                ver = true;
            }
            else if (objRes2.reser_c_dfech_inicio <= objRes1.reser_c_dfech_inicio && objRes1.reser_c_dfech_inicio <= objRes2.reser_c_dfech_fin)
            {
                ver = true;
            }
            else if (objRes2.reser_c_dfech_inicio <= objRes1.reser_c_dfech_fin && objRes1.reser_c_dfech_fin <= objRes2.reser_c_dfech_fin)
            {
                ver = true;
            }
            if (ver) validarCruce = "- Existen cruce de fechas para el espacio " + objRes1.pub_esp_c_vcod + " (Ejecutivos: " + ejecutivoRes1 + ", " + ejecutivoRes2 + ").<br/>";
            return validarCruce;
        }

        protected void btnidOkAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                objAprob = new ApproveSpaceSoldBL();

                foreach (DIO_PUB_T_RESERVA objeto in reservasVendidas)
                {
                    objAprob.f_reserva_venderBL(objeto);
                }
                 buscar();
                 lblmensajeAccion.Text = "Se vendieron las reservas correctamente.";
                 ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                 reservasVendidas.Clear();
                 ltvalidacionReservaVendido.Text = "";
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}