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
using System.Data;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Collections;
using System.Configuration;
namespace BOM.UserLayer.Interfaces.Reserve
{
    public partial class frmReserveSpaceAdvertising : System.Web.UI.Page
    {
        #region VARIABLES GLOBALES
        private ReserveSpaceAdvertisingBL _blReserveSpace = null;

        private SGA_T_USUARIO objUsuario
        {
            get { return (SGA_T_USUARIO)ViewState["vsObjUsuario"]; }
            set { ViewState["vsObjUsuario"] = value; }
        }
        private IList<DIO_SP_REQUISITO_PUBLICIDAD_LISTAR_Result> vs_ConfRequisitoHTML
        {
            get { return (List<DIO_SP_REQUISITO_PUBLICIDAD_LISTAR_Result>)ViewState["vsConfRequisitoHTML"]; }
            set { ViewState["vsConfRequisitoHTML"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
          if (!Page.IsPostBack)
          {
              UISeguridad obj = new UISeguridad();
              lblPaso1.Text = "Paso 1: BÚSQUEDA";
              lblPaso2.Text = "Paso 2: RESERVA";
              lblPaso3.Text = "Paso 3: ASOCIAR CLIENTE";
              objUsuario = (SGA_T_USUARIO)Session["SGA_T_USUARIO"];
              f_CargarConfiguracionRequisitosHTML();
              ViewState["vs_ejec_c_cdoc_id"] = "";
              ViewState["vs_id_reserva_master"] = "0";

              if (Request.QueryString["codMaster"] != null )
              {
                  ViewState["vs_id_reserva_master"] = obj.f_Desencriptar(Request.QueryString["codMaster"].ToString());
                  _blReserveSpace=new ReserveSpaceAdvertisingBL();
                  gvReservas.DataSource = _blReserveSpace.f_listar_reservas_pendientes_xidmasterBL(Convert.ToInt32(ViewState["vs_id_reserva_master"].ToString()));
                  gvReservas.DataBind();
                  
                  lblPaso1.Font.Bold = false;
                  lblPaso2.Font.Bold = true;

                  ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_Mostrar();", true);
                  seleccionarTodosEspaciosBusqueda();
                  btnRegresa.Visible = false;
                  ViewState["vs_fechaDesde"] = Request.QueryString["fDesde"].ToString();
                  ViewState["vs_fechaHasta"] = Request.QueryString["fHasta"].ToString();
                  ViewState["vs_ejec_c_cdoc_id"] = obj.f_Desencriptar(Request.QueryString["codEjec"].ToString());
              }
              else
              {
                  ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_Ready_Fuction();", true);
                  f_cargarComboProducto();
                  f_cargarComboInmueble();
                  f_cargarComboElementoActivacion(1);
                  txtMedidaDesdeFiltro.Attributes["disabled"] = "disabled";
                  txtMedidaHastaFiltro.Attributes["disabled"] = "disabled";
                  ////////////////////
                  lblPaso1.Font.Bold = true;
              }
          }
        }

        #region CARGAR PAGINA
        void f_CargarConfiguracionRequisitosHTML()
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            vs_ConfRequisitoHTML = _blReserveSpace.f_ListarConfiguracionRequisitosBL();
        }
        void f_cargarComboProducto()
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<ADV_T_PUB_PRODUCTO> lista = _blReserveSpace.f_listar_pub_productosBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "pub_prod_c_iid", "pub_prod_c_vnomb", ddlProductoFiltro, "", "");
            }

        }
        void f_cargarComboElementoActivacion(int producto)
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<ADV_T_PUB_ELEMENTO_ACTIVACION> lista = _blReserveSpace.f_listar_elementoActivacionXProductoBL(producto);

            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "pub_elem_act_c_iid", "pub_elem_act_c_vnomb", checkboxListElemento, "", "");
            }
        }
        void f_cargarComboInmueble()
        {
            InmuebleBL blInmueble = new InmuebleBL();
            List<ADV_T_INMUEBLE> lista = blInmueble.ListarInmueblesRealPlazaBL();
            List<ADV_T_INMUEBLE> lista_tmp = new List<ADV_T_INMUEBLE>();

            foreach (var item in lista)
            {
                ADV_T_INMUEBLE obj = new ADV_T_INMUEBLE();
                obj.inm_c_icod = item.inm_c_icod;
                obj.inm_c_vnomb = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.inm_c_vnomb.ToLower());
                lista_tmp.Add(obj);
            }

            if (lista_tmp.Count > 0)
            {

                lista_tmp.Sort(delegate(ADV_T_INMUEBLE i1, ADV_T_INMUEBLE i2) { return i1.inm_c_vnomb.CompareTo(i2.inm_c_vnomb); });
                lista_tmp.Insert(0, new ADV_T_INMUEBLE() { inm_c_icod = 0, inm_c_vnomb = "Todos" });
                //lista_tmp.Add(new ADV_T_INMUEBLE() { inm_c_icod = 0, inm_c_vnomb = "Todos" });
                dtlInmueble.DataSource = lista_tmp;
                dtlInmueble.DataBind();
            }
        }
        #endregion

        #region BUSQUEDA DE DISPONIBILIDAD ESPACIOS
        #region "SERVICIO WEB"
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] razonsocialClienteAutocompletado(string prefixText, int count, string contextKey)
        {
            IApproveSpaceSoldBL objAprob = new ApproveSpaceSoldBL();

            List<ADV_T_CLIENTE> lista = objAprob.f_ListarClientesBL();
            return (from x in lista
                    where x.cli_c_vraz_soc.Contains(prefixText.ToUpper())
                    select x.cli_c_vraz_soc).Take(count).ToArray();
        }
        protected void ddlProductoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

            f_cargarComboElementoActivacion(Convert.ToInt16(ddlProductoFiltro.SelectedValue));
            //upFiltro.Update();
            if (Convert.ToInt16(ddlProductoFiltro.SelectedValue) == 1)//INDOR
            {
                txtMedidaDesdeFiltro.Text = "";
                txtMedidaHastaFiltro.Text = "";
                txtMedidaDesdeFiltro.Attributes["disabled"] = "disabled";
                txtMedidaHastaFiltro.Attributes["disabled"] = "disabled";
                lblActivacionElemento.Text = "Elemento";
            }
            else if (Convert.ToInt16(ddlProductoFiltro.SelectedValue) == 2)//BTL
            {

                txtMedidaDesdeFiltro.Attributes.Remove("disabled");
                txtMedidaHastaFiltro.Attributes.Remove("disabled");
                lblActivacionElemento.Text = "Tipo de Activación";
            }
        }
        int m_ListarEspacioPublicitarioWS(GridView pobj_GridView, string ps_InmuebleSelec, string ps_ElementoSelec, string ps_AreaDesde, string ps_AreaHasta, int pi_IdProducto)
        {
            string s_BaseURL = WebConfigurationManager.AppSettings["RutaURLSWEspacioPublicitario"].ToString() +
                "&json={\"ps_InmuebleSelec\":\"" + ps_InmuebleSelec + "\",\"ps_ElementoSelec\":\"" + ps_ElementoSelec +
                "\", \"ps_AreaDesde\":\"" + ps_AreaDesde + "\",\"ps_AreaHasta\":\"" + ps_AreaHasta + "\",\"pi_IdProducto\":" + pi_IdProducto + "}";

            WebClient n = new WebClient();
            n.Encoding = System.Text.Encoding.UTF8;
            var json = n.DownloadString(s_BaseURL);

            JObject o = JObject.Parse(json);

            string ps_result = "metodBomListarEspacioPublicitarioResult";
            string ps_DataSet = "DsBomEspacioPublicitarioFiltro";
            string ps_DataTable = "DtBomEspacioPublicitarioFiltro";

            var obj = new
            {
                RESER_C_IID="",
                PUB_ESP_C_IID = "",
                PUB_ELEM_ACT_C_IID = "",
                INM_C_VNOMB = "",
                PUB_PROD_C_VNOMB = "",
                PUB_ELEM_ACT_C_VNOMB = "",
                PUB_ESP_C_VCOD = "",
                PUB_ESP_C_EMONTO_TARIFA_BASE = "",
                PUB_ESP_C_EMONTO_TARIFA_TOP = "",
                PUB_PROD_C_IID = "",
                INM_C_ICOD = "",
                PUB_ESP_C_EAREA="",
                PUB_ESP_C_VMEDIDA=""

            };
            var lista = new[] { obj }.ToList();
            lista.Clear();
            if (o[ps_result]["diffgr:diffgram"][ps_DataSet] == null)
            {
                lblmensajeAccion.Text = "No se encontró registros en la búsqueda";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                return 0;
            }
            try
            {
                foreach (var item in o[ps_result]["diffgr:diffgram"][ps_DataSet][ps_DataTable])
                {
                    lista.Add(new
                    {
                        RESER_C_IID = UIFunciones.f_StringIsNull(item["RESER_C_IID"]),
                        PUB_ESP_C_IID = UIFunciones.f_StringIsNull(item["PUB_ESP_C_IID"]),
                        PUB_ELEM_ACT_C_IID = UIFunciones.f_StringIsNull(item["PUB_ELEM_ACT_C_IID"]),
                        INM_C_VNOMB = UIFunciones.f_StringIsNull(item["INM_C_VNOMB"]),
                        PUB_PROD_C_VNOMB = UIFunciones.f_StringIsNull(item["PUB_PROD_C_VNOMB"]),
                        PUB_ELEM_ACT_C_VNOMB = UIFunciones.f_StringIsNull(item["PUB_ELEM_ACT_C_VNOMB"]),
                        PUB_ESP_C_VCOD = UIFunciones.f_StringIsNull(item["PUB_ESP_C_VCOD"]),
                        PUB_ESP_C_EMONTO_TARIFA_BASE = UIFunciones.f_StringIsNull(item["PUB_ESP_C_EMONTO_TARIFA_BASE"]),
                        PUB_ESP_C_EMONTO_TARIFA_TOP = UIFunciones.f_StringIsNull(item["PUB_ESP_C_EMONTO_TARIFA_TOP"]),
                        PUB_PROD_C_IID = UIFunciones.f_StringIsNull(item["PUB_PROD_C_IID"]),
                        INM_C_ICOD = UIFunciones.f_StringIsNull(item["INM_C_ICOD"]),
                        PUB_ESP_C_EAREA = UIFunciones.f_StringIsNull(item["PUB_ESP_C_EAREA"]),
                        PUB_ESP_C_VMEDIDA = UIFunciones.f_StringIsNull(item["PUB_ESP_C_VMEDIDA"])
                    });
                }
            }
            catch (Exception)
            {
                try
                {
                    foreach (var item in o)
                    {
                        if (item.Key == ps_result)
                        {
                            lista.Add(new
                            {
                                RESER_C_IID = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["RESER_C_IID"])),
                                PUB_ESP_C_IID = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_IID"])),
                                PUB_ELEM_ACT_C_IID = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_IID"])),
                                INM_C_VNOMB = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["INM_C_VNOMB"])),
                                PUB_PROD_C_VNOMB = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_PROD_C_VNOMB"])),
                                PUB_ELEM_ACT_C_VNOMB = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_VNOMB"])),
                                PUB_ESP_C_VCOD = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VCOD"])),
                                PUB_ESP_C_EMONTO_TARIFA_BASE = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_EMONTO_TARIFA_BASE"])),
                                PUB_ESP_C_EMONTO_TARIFA_TOP = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_EMONTO_TARIFA_TOP"])),
                                PUB_PROD_C_IID = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_PROD_C_IID"])),
                                INM_C_ICOD = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["INM_C_ICOD"])),
                                PUB_ESP_C_EAREA = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_EAREA"])),
                                PUB_ESP_C_VMEDIDA = HttpUtility.HtmlDecode(UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VMEDIDA"]))
                            });

                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    lblmensajeAccion.Text = "Ocurrió un problema para obtener los datos de la búsqueda";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                    return 0;
                }
            }

            //obtener reservas en donde tenga una fecha disponible en el rango de fechas insertado 

            string cadena_espacios_sw = "";
            bool primerRegistro = true; // variable para que no pinte la comilla al inicio
            foreach (var item in lista)
            {
                if (primerRegistro)
                {
                    cadena_espacios_sw += item.PUB_ESP_C_IID;
                    primerRegistro = false;
                }
                else cadena_espacios_sw += "," + item.PUB_ESP_C_IID;
            }
            IList<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result> lst_no_disponibles = new List<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result>();
            
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            //lst_no_disponibles = _blReserveSpace.f_obtenerEspaciosNoDisponiblesBL(cadena_espacios_sw, 
            //    Convert.ToDateTime(txtfechaDesdeFiltro.Text.Trim()), Convert.ToDateTime(txtfechaHastaFiltro.Text.Trim()));
            var listTemp = new[] { obj }.ToList();
            listTemp.Clear();
            foreach (var item in lista)
            {
                if (!EspacioDisponible(Convert.ToInt16(item.PUB_ESP_C_IID), lst_no_disponibles))
                {
                    listTemp.Add(item);
                }
            }
            foreach (var item in listTemp)
            {
                lista.Remove(item);
            }
            if (lista.Count != 0)
            {
                pobj_GridView.DataSource = lista;
                pobj_GridView.DataBind();
                return 1;
            }
            else
            {
                lblmensajeAccion.Text = "No se encontró espacios disponibles para el rango de fechas ingresadas.";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                return 0;
            }
        }
        bool EspacioDisponible(int idespacio, IList<SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result> lst_no_disponibles)
        {
            bool ver = true;
            foreach (SP_PUB_ESPACIOS_NO_DISPONIBLE_LISTAR_Result obj in lst_no_disponibles)
            {
                if (obj.pub_esp_c_iid == idespacio)
                {
                    ver = false;
                    break;
                }
            }
            return ver;
        }
        protected void Buscar()
        {
            string validarBusqueda = validaBusqueda();
            if (validarBusqueda == "")
            {

                _blReserveSpace = new ReserveSpaceAdvertisingBL();

                string _inmueblesSeleccionados = f_obtenerSeleccionInmuebles();
                string _elementosSeleccionados = f_obtenerSeleccionElementos();
                string _razonSocial = txtRazonSocialFiltro.Text.Trim();
                string _areaDesde = txtMedidaDesdeFiltro.Text.Trim();
                string _areaHasta = txtMedidaHastaFiltro.Text.Trim();
                int _idProducto = Convert.ToInt16(ddlProductoFiltro.SelectedItem.Value);
                ViewState["vs_fechaDesde"] = txtfechaDesdeFiltro.Text.Trim();
                ViewState["vs_fechaHasta"] = txtfechaHastaFiltro.Text.Trim();

                temporalBL objbl = new temporalBL();

                int iResul = m_ListarEspacioPublicitarioWS(gvReservas, _inmueblesSeleccionados, _elementosSeleccionados, _areaDesde, _areaHasta, _idProducto);
                if (iResul == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_Mostrar();", true);
                }
                string ps_razonSocial = txtRazonSocialFiltro.Text.Trim();
            }
            else
            {
                //MENSAJE PARA QUE INGRESE LOS FILTROS NECESARIOS  'validarBusqueda' .!!!!!!!!!!
                //litHtmlAccion.Text=UIGeneral.UIFunciones.f_mostrarMensajeAccion("alerta", validarBusqueda);
                lblmensajeAccion.Text = validarBusqueda;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
            }
        }
        private string f_obtenerSeleccionInmuebles()
        {
            string cadena = "";
            bool primerRegistro = true; // variable para que no pinte la comilla al inicio
            foreach (DataListItem item in dtlInmueble.Items)
            {
                CheckBox chkInmueble = item.FindControl("chkInmueble") as CheckBox;
                Label lblInmueble = item.FindControl("lblInmueble") as Label;
                if (chkInmueble.Checked)
                {
                    if (lblInmueble.Text != "0")
                    {
                        if (primerRegistro)
                        {
                            cadena += lblInmueble.Text;
                            primerRegistro = false;
                        }
                        else cadena += "," + lblInmueble.Text;
                    }
                }
            }
            return cadena;
        }
        private string f_obtenerSeleccionElementos()
        {
            string cadena = "";
            bool primerRegistro = true; // variable para que no pinte la comilla al inicio
            foreach (ListItem item in checkboxListElemento.Items)
            {
                if (item.Selected)
                {
                    if (primerRegistro)
                    {
                        cadena += item.Value;
                        primerRegistro = false;
                    }
                    else cadena += "," + item.Value;
                }

            }
            return cadena;
        }
        bool inmuebleSeleccionado()
        {
            bool ver = false;
            foreach (DataListItem item in dtlInmueble.Items)
            {
                CheckBox chkInmueble = item.FindControl("chkInmueble") as CheckBox;
                if (chkInmueble.Checked)
                {
                    ver = true;
                    break;
                }
            }
            return ver;
        }
        #endregion
        #endregion

        bool areaValida(string entrada)
        {
            try
            {
                decimal temp = Convert.ToDecimal(entrada);
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool fechaValida(string entrada)
        {
            try
            {
                DateTime temp = Convert.ToDateTime(entrada);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string validaBusqueda()
        {
            bool fechasCorrectas = true;
            bool areasCorrectas = true;
            string salida = "";
            //validar vacios
            //producto no se valida, por defecto esta un producto .. (se quita "seleccione")
            if (!inmuebleSeleccionado())
            {
                salida = "- Debe seleccionar almenos un inmueble.<br/>";
            }
            if (ddlProductoFiltro.SelectedValue == "2")//BTL
            {
                if (txtMedidaDesdeFiltro.Text.Trim() != "" && txtMedidaHastaFiltro.Text.Trim() == "")
                {
                    areasCorrectas = false;
                    salida += "- Ingrese el campo área (hasta).<br/>";
                }
                if (txtMedidaDesdeFiltro.Text.Trim() == "" && txtMedidaHastaFiltro.Text.Trim() != "")
                {
                    areasCorrectas = false;
                    salida += "- Ingrese el campo área (desde).<br/>";
                }
                if (txtMedidaDesdeFiltro.Text.Trim() != "")
                {
                    if (!areaValida(txtMedidaDesdeFiltro.Text.Trim()))
                    {
                        areasCorrectas = false;
                        salida += "- El campo área (desde) debe ser entero.<br/>";
                    }
                }
                if (txtMedidaHastaFiltro.Text.Trim() != "")
                {
                    if (!areaValida(txtMedidaHastaFiltro.Text.Trim()))
                    {
                        areasCorrectas = false;
                        salida += "- El campo área (hasta) debe ser entero.<br/>";
                    }
                }
                if (txtMedidaDesdeFiltro.Text.Trim() != "" && txtMedidaHastaFiltro.Text.Trim() != "")
                {
                    if (areasCorrectas)
                    {
                        // validar mayor en fechas 
                        int areaDesde = Convert.ToInt16(txtMedidaDesdeFiltro.Text.Trim());
                        int areaHasta = Convert.ToInt16(txtMedidaHastaFiltro.Text.Trim());
                        if (areaDesde > areaHasta) //fecha desde es mayor que fecha hasta
                        {
                            salida += "- El área DESDE no debe ser mayor al área HASTA.</br>";
                        }
                    }
                }
            }
            if (txtfechaDesdeFiltro.Text.Trim() == "")
            {
                salida += "- Ingrese la fecha DESDE.<br/>";
                fechasCorrectas = false;
            }

            else if (!fechaValida(txtfechaDesdeFiltro.Text.Trim()))
            {
                salida += "- Fecha DESDE inválida.<br/>";
                fechasCorrectas = false;
            }

            if (txtfechaHastaFiltro.Text.Trim() == "")
            {
                salida += "- Ingrese la fecha HASTA.<br/>";
                fechasCorrectas = false;
            }
            else if (!fechaValida(txtfechaHastaFiltro.Text.Trim()))
            {
                salida += "- Fecha HASTA inválida.<br/>";
                fechasCorrectas = false;
            }
            if (fechasCorrectas)
            {
                // validar mayor en fechas 
                string fechaDesde = Convert.ToDateTime(txtfechaDesdeFiltro.Text.Trim()).ToShortDateString();
                string fechaHasta = Convert.ToDateTime(txtfechaHastaFiltro.Text.Trim()).ToShortDateString();
                DateTime dfechaDesde = Convert.ToDateTime(fechaDesde);
                DateTime dfechaHasta = Convert.ToDateTime(fechaHasta);

                if (dfechaDesde > dfechaHasta) //fecha desde es mayor que fecha hasta
                {
                    salida += "- La fecha DESDE no debe ser mayor a la fecha HASTA.</br>";
                }
            }

            return salida;
        }

        protected void chkInmueble_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Text == "Todos")
            {
                foreach (DataListItem item in dtlInmueble.Items)
                {
                    CheckBox chkInmueble = item.FindControl("chkInmueble") as CheckBox;
                    if (chkInmueble != sender)
                        chkInmueble.Checked = (sender as CheckBox).Checked;
                }
            }
            else
            {
                foreach (DataListItem item in dtlInmueble.Items)
                {
                    CheckBox chkInmueble = item.FindControl("chkInmueble") as CheckBox;
                    if (chkInmueble.Text == "Todos")
                        if (chkInmueble.Checked && !(sender as CheckBox).Checked)
                            chkInmueble.Checked = (sender as CheckBox).Checked;
                }
            }
        }
        int m_ListarEspacioxIDWS(int pi_idespacio)
        {
            string s_BaseURL = WebConfigurationManager.AppSettings["RutaURLSWEspacioxID"].ToString() + "&json={\"pi_IdEspacio\":" + pi_idespacio + "}";

            WebClient n = new WebClient();
            n.Encoding = System.Text.Encoding.UTF8;
            var json = n.DownloadString(s_BaseURL);

            JObject o = JObject.Parse(json);

            string ps_result = "metodBomEspacioxIDResult";
            string ps_DataSet = "BomEspacio";
            string ps_DataTable = "Espacio";

            var obj = new
            {
                pub_prod_c_vnomb = "",
                pub_esp_c_vcod = "",
                pub_elem_act_c_vnomb = "",
                pub_esp_c_vmedida = "",
                pub_esp_c_vubicacion = "",
                pub_esp_c_vmaterial = "",
                pub_esp_c_vdesc = "",
                pu_esp_c_earea = "",
                pub_esp_c_vrestriccion = "",
                pub_esp_imagenes = ""
            };
            var lista = new[] { obj }.ToList();
            if (o[ps_result]["diffgr:diffgram"][ps_DataSet] == null)
            {
                lblmensajeAccion.Text = "No se encontró datos a mostrar";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                return 0;
            }


            lista.Clear();
            if (o[ps_result]["diffgr:diffgram"][ps_DataSet][ps_DataTable].Count() >= 1)
            {
                foreach (var item in o)
                {
                    if (item.Key == ps_result)
                    {
                        lista.Add(new
                        {
                            pub_prod_c_vnomb = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_PROD_C_VNOMB"]),
                            pub_esp_c_vcod = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VCOD"]),
                            pub_elem_act_c_vnomb = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_VNOMB"]),
                            pub_esp_c_vmedida = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VMEDIDA"]),
                            pub_esp_c_vubicacion = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VUBICACION"]),
                            pub_esp_c_vmaterial = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VMATERIAL"]),
                            pub_esp_c_vdesc = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VDESC"]),
                            pu_esp_c_earea = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_EAREA"]),
                            pub_esp_c_vrestriccion = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ESP_C_VRESTRICCION"]),
                            pub_esp_imagenes = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["IMAGENES"])
                            
                        });

                        break;
                    }
                }
                lblproductoPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_prod_c_vnomb.ToString());
                lblcodespacioPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_esp_c_vcod.ToString());
                lblelementoPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_elem_act_c_vnomb.ToString());
                lblMaterialPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_esp_c_vmaterial.ToString());
                lblDescripcionPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_esp_c_vdesc.ToString());
                lblrestriccionPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_esp_c_vrestriccion.ToString());
                lblmedidaPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_esp_c_vmedida.ToString());
                lblUbicacionPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pub_esp_c_vubicacion.ToString());
                lbleareaPopinfo.InnerText = HttpUtility.HtmlDecode(lista[0].pu_esp_c_earea.ToString()) + " m2";

                //string rutaImagenFTP = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["rutaImagen"]);
                string rutaImagenFTP = ConfigurationManager.AppSettings["rutaImagen"];

                ArrayList ListaImagen = new ArrayList();
                string[] Archivos = System.IO.Directory.GetFiles(Server.MapPath("~/Images"), "*.*");
                foreach (string archivo in Archivos)
                {
                    ListaImagen.Add("~/Images/" + System.IO.Path.GetFileName(archivo));
                }
                repeaterImage.DataSource = ListaImagen;
                repeaterImage.DataBind();
                
                if (lista[0].pub_prod_c_vnomb.ToString() == "INDOOR")
                    lbltexto_elementoPopInfo.InnerText = "Elemento:";
                else if (lista[0].pub_prod_c_vnomb.ToString() == "BTL") lbltexto_elementoPopInfo.InnerText = "Tipo de Activación:";
            }
            return 1;
        }

        protected void gvReservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "verInfoEspacio")
                {
                    //SERVICIO WEB
                    int _idespacio = Convert.ToInt16(gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pub_esp_c_iid"]);
                    int iResul = m_ListarEspacioxIDWS(_idespacio);
                    if (iResul == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPop();", true);
                    }
                }
                else if (e.CommandName == "verInfoDisponibilidad")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopOcupacion();", true);
                    //SERVICIO WEB
                    lblproductoPopOcupacion.InnerText = gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pub_prod_c_vnomb"].ToString();
                    lblcodPopOcupacion.InnerText = gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pub_esp_c_vcod"].ToString();
                    lblFechaPopOcupacion.InnerText = "desde " + ViewState["vs_fechaDesde"] + " hasta " + ViewState["vs_fechaHasta"];

                    int _idespacio = Convert.ToInt16(gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pub_esp_c_iid"]);
                    int _idelemento = Convert.ToInt16(gvReservas.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pub_elem_act_c_iid"].ToString());

                    List<SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR_Result> lista = new List<SP_PUB_RESERVA_DETALLE_XIDESPACIO_LISTAR_Result>();
                    _blReserveSpace = new ReserveSpaceAdvertisingBL();
                    lista = _blReserveSpace.f_listar_detalle_ocupacionXelementoEspacioBL(_idespacio, _idelemento,
                        Convert.ToDateTime(ViewState["vs_fechaDesde"]), Convert.ToDateTime(ViewState["vs_fechaHasta"]));

                    gvDetalleOcupacion.DataSource = lista;
                    gvDetalleOcupacion.DataBind();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnBuscarPaso1_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        protected void btnRegresa_Click(object sender, EventArgs e)
        {
            lblPaso1.Font.Bold = true;
            lblPaso2.Font.Bold = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_RegresaConsulta();", true);
        }
        #region  RESERVA DE ESPACIO!!!!
        protected void ddlElementoReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlElementoReserva = (DropDownList)sender;
            DataListItem datalistPadre = (DataListItem)ddlElementoReserva.NamingContainer;

            Label lblDetalleEntrega = (Label)datalistPadre.FindControl("lblDetalleEntrega");
            Label lblDetalleApertura = (Label)datalistPadre.FindControl("lblDetalleApertura");
            m_MostrarRequisitoHTML(Convert.ToInt32(ddlElementoReserva.SelectedItem.Value), lblDetalleEntrega, lblDetalleApertura);
        }
        void m_MostrarRequisitoHTML(int pi_pub_elem_act_c_iid, Label pobj_Entrega, Label pobj_Apertura)
        {
            pobj_Entrega.Text = "--";
            pobj_Apertura.Text = "--";

            var Req_Entrega = (from x in vs_ConfRequisitoHTML
                               where x.par_c_iid_Padre == Convert.ToInt32(IEnum.Requisitos_Publicidad.Requisito_Entrega_Padre)
                               && x.CODIGO_ELEMENTO_ACTIVACION.Contains("|" + pi_pub_elem_act_c_iid + "|")
                               select x).FirstOrDefault();

            if (Req_Entrega != null)
            {
                pobj_Entrega.Text = Req_Entrega.HTML;
            }

            var Req_Apertura = (from x in vs_ConfRequisitoHTML
                                where x.par_c_iid_Padre == Convert.ToInt32(IEnum.Requisitos_Publicidad.Requisito_Apertura_Padre)
                                && x.CODIGO_ELEMENTO_ACTIVACION.Contains("|" + pi_pub_elem_act_c_iid + "|")
                                select x).FirstOrDefault();

            if (Req_Apertura != null)
            {
                pobj_Apertura.Text = Req_Apertura.HTML;
            }
        }
        void eliminar_espacios_guardados(int id_reserva_master) 
        {
            try
            {
                //elimina las reservas de espacios que se hayan guardado y NO SE HAYAN VUELTO A SELECCIONAR.
                string espacios = "";
                bool primerRegistro = true; // variable para que no pinte la comilla al inicio

                for (int i = 0; i < gvReservas.Rows.Count; i++)
                {
                    CheckBox controlCheck = (CheckBox)gvReservas.Rows[i].FindControl("checkEspacio");

                    string lblidespacio = gvReservas.DataKeys[i].Values["pub_esp_c_iid"].ToString();
                    int idreserva = Convert.ToInt32(gvReservas.DataKeys[i].Values["reser_c_iid"].ToString());

                    if (controlCheck != null)
                    {
                        if (controlCheck.Checked)
                        {
                            if (primerRegistro)
                            {
                                espacios += lblidespacio;
                                primerRegistro = false;
                            }
                            else
                            {
                                espacios += "," + lblidespacio;
                            }
                        }
                    }
                }
                _blReserveSpace = new ReserveSpaceAdvertisingBL();
                _blReserveSpace.f_eliminar_espacios_guardadosBL(espacios, id_reserva_master);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result getReserva_memoria(int idespacio,IList<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result> lista_reservas_enmemoria)
        {
            DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result objeto=null;
            foreach(DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result obj in lista_reservas_enmemoria)
            {
                if (obj.pub_esp_c_iid == idespacio)
                {
                    objeto = new DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result();
                    objeto = obj;
                    break;
                }
            }
            return objeto;
        }
        bool haySeleccionEspacio()
        {
            bool ver = false;
            for (int i = 0; i < gvReservas.Rows.Count; i++)
            {
                CheckBox controlCheck = (CheckBox)gvReservas.Rows[i].FindControl("checkEspacio");
                if (controlCheck != null)
                {
                    if (controlCheck.Checked)
                    {
                        ver = true;
                        break;
                    }
                }
            }
            return ver;
        }
        protected void btnIrReserva_Click(object sender, EventArgs e)
        {
            try
            {
                if (haySeleccionEspacio())
                {
                    lblPaso1.Font.Bold = false;
                    lblPaso2.Font.Bold = true;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_mostrarPaso2();", true);
                    IList<AssociateReservation> lista = new List<AssociateReservation>();
                    AssociateReservation obj;
                    int cont_tab = 0;

                    _blReserveSpace = new ReserveSpaceAdvertisingBL();
                    lblfechavencimientoReserva.InnerText = _blReserveSpace.f_diaUtilVencimientoBL(5);

                    IList<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result> lista_reservas_enmemoria = new List<DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result>();
                    //ver que haya seleccion para no realizar la consulta inceseariamente. pendiente....-----
                    cargarComboEjecutivosBTL();
                    if (ViewState["vs_ejec_c_cdoc_id"].ToString() != "")
                    {
                        ddlEjecutivoReserva.SelectedValue = ViewState["vs_ejec_c_cdoc_id"].ToString();
                    }
                   
                    if (Convert.ToInt16(ViewState["vs_id_reserva_master"].ToString()) != 0) // ya se realizó el registro!, consultar los elementos seleccionados.
                    {
                        eliminar_espacios_guardados(Convert.ToInt16(ViewState["vs_id_reserva_master"].ToString()));
                        _blReserveSpace = new ReserveSpaceAdvertisingBL();
                        lista_reservas_enmemoria = _blReserveSpace.f_listar_reservas_enmemoria_xidmasterBL(Convert.ToInt16(ViewState["vs_id_reserva_master"].ToString()));
                    }

                    if (lista_reservas_enmemoria.Count != 0)
                    {
                        for (int i = 0; i < gvReservas.Rows.Count; i++)
                        {
                            CheckBox controlCheck = (CheckBox)gvReservas.Rows[i].FindControl("checkEspacio");
                            if (controlCheck != null)
                            {
                                if (controlCheck.Checked)// si se selecciono,pintar dinamicamente.
                                {
                                    obj = new AssociateReservation();
                                    if (cont_tab == 0)
                                    {
                                        obj.s_class_num_tab = "panel-collapse collapse in";
                                    }
                                    else
                                    {
                                        obj.s_class_num_tab = "panel-collapse collapse";
                                    }

                                    DIO_SP_PUB_RESERVA_ENMEMORIA_XIDMASTER_LISTAR_Result objeto = getReserva_memoria(Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_esp_c_iid"].ToString()), lista_reservas_enmemoria);
                                    if (objeto != null)
                                    {
                                        obj.i_reser_c_iid = objeto.reser_c_iid;
                                        obj.s_nombre_tab = objeto.inm_c_vnomb + " > " + " " + objeto.pub_esp_c_vcod;
                                        obj.s_cod_espacio = objeto.pub_esp_c_vcod;
                                        obj.i_id_producto = objeto.pub_prod_c_iid;
                                        obj.i_id_inmueble = objeto.inm_c_icod;
                                        obj.i_id_espacio = objeto.pub_esp_c_iid;
                                        obj.i_id_elemento_activacion = objeto.pub_elem_act_c_iid;
                                        obj.s_nombre_producto = objeto.pub_prod_c_vnomb;
                                        obj.s_tarifa_fria = objeto.pub_esp_c_emonto_tarifa_top.ToString(); //tarifa top
                                        obj.s_tarifa_base = objeto.pub_esp_c_emonto_tarifa_base.ToString();
                                    }
                                    else
                                    {
                                        obj.i_reser_c_iid = 0;
                                        obj.s_nombre_tab = gvReservas.DataKeys[i].Values["inm_c_vnomb"].ToString() + " > " + " " + gvReservas.DataKeys[i].Values["pub_esp_c_vcod"].ToString();
                                        obj.s_cod_espacio = gvReservas.DataKeys[i].Values["pub_esp_c_vcod"].ToString();
                                        obj.i_id_producto = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_prod_c_iid"].ToString());
                                        obj.i_id_inmueble = Convert.ToInt16(gvReservas.DataKeys[i].Values["inm_c_icod"].ToString());
                                        obj.i_id_espacio = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_esp_c_iid"].ToString());
                                        obj.i_id_elemento_activacion = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_elem_act_c_iid"].ToString());
                                        obj.s_nombre_producto = gvReservas.DataKeys[i].Values["pub_prod_c_vnomb"].ToString();
                                        obj.s_tarifa_fria = gvReservas.DataKeys[i].Values["pub_esp_c_emonto_tarifa_top"].ToString();
                                        obj.s_tarifa_base = gvReservas.DataKeys[i].Values["pub_esp_c_emonto_tarifa_base"].ToString();
                                        obj.s_esp_earea = Convert.ToDecimal(gvReservas.DataKeys[i].Values["pub_esp_c_earea"].ToString());
                                        obj.s_esp_vmedida = gvReservas.DataKeys[i].Values["pub_esp_c_vmedida"].ToString();
                                    }

                                    if (obj.i_id_producto == 1)//INDOOR
                                    {
                                        obj.s_texto_tipo_elemento = "Elemento"; 
                                    }
                                    else if (obj.i_id_producto == 2)//BTL
                                    {
                                        obj.s_texto_tipo_elemento = "Tipo de Activación";
                                    }
                                    obj.s_requisito_producto_entrega = "Requisito del tipo " + gvReservas.DataKeys[i].Values["pub_prod_c_vnomb"].ToString() + " para ENTREGA"; //Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_elem_act_c_iid"].ToString());
                                    obj.s_requisito_producto_apertura = "Requisito del tipo " + gvReservas.DataKeys[i].Values["pub_prod_c_vnomb"].ToString() + " para APERTURA"; //Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_elem_act_c_iid"].ToString());
                                    lista.Add(obj);
                                    cont_tab++;
                                }
                            }
                        }
                        dtlistEspacios.DataSource = lista;
                        dtlistEspacios.DataBind();
                        //upReservas.Update();
                    }
                    else
                    {
                        for (int i = 0; i < gvReservas.Rows.Count; i++)
                        {
                            CheckBox controlCheck = (CheckBox)gvReservas.Rows[i].FindControl("checkEspacio");
                            if (controlCheck != null)
                            {
                                if (controlCheck.Checked)// si se selecciono,pintar dinamicamente.
                                {
                                    obj = new AssociateReservation();
                                    if (cont_tab == 0)
                                    {
                                        obj.s_class_num_tab = "panel-collapse collapse in";
                                    }
                                    else
                                    {
                                        obj.s_class_num_tab = "panel-collapse collapse";
                                    }
                                    obj.i_reser_c_iid = 0;
                                    obj.s_nombre_tab = gvReservas.DataKeys[i].Values["inm_c_vnomb"].ToString() + " > " + " " + gvReservas.DataKeys[i].Values["pub_esp_c_vcod"].ToString();
                                    obj.s_cod_espacio = gvReservas.DataKeys[i].Values["pub_esp_c_vcod"].ToString();
                                    obj.i_id_producto = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_prod_c_iid"].ToString());
                                    obj.i_id_inmueble = Convert.ToInt16(gvReservas.DataKeys[i].Values["inm_c_icod"].ToString());
                                    obj.i_id_espacio = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_esp_c_iid"].ToString());
                                    obj.i_id_elemento_activacion = Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_elem_act_c_iid"].ToString());
                                    obj.s_nombre_producto = gvReservas.DataKeys[i].Values["pub_prod_c_vnomb"].ToString();
                                    obj.s_tarifa_fria = gvReservas.DataKeys[i].Values["pub_esp_c_emonto_tarifa_top"].ToString();
                                    obj.s_tarifa_base = gvReservas.DataKeys[i].Values["pub_esp_c_emonto_tarifa_base"].ToString();
                                    obj.s_esp_earea = Convert.ToDecimal(gvReservas.DataKeys[i].Values["pub_esp_c_earea"].ToString());
                                    obj.s_esp_vmedida = gvReservas.DataKeys[i].Values["pub_esp_c_vmedida"].ToString();

                                    if (obj.i_id_producto == 1)//INDOOR
                                    {
                                        obj.s_texto_tipo_elemento = "Elemento";
                                    }
                                    else if (obj.i_id_producto == 2)//BTL
                                    {
                                        obj.s_texto_tipo_elemento = "Tipo de Activación";
                                    }

                                    obj.s_requisito_producto_entrega = "Requisito del tipo " + gvReservas.DataKeys[i].Values["pub_prod_c_vnomb"].ToString() + " para ENTREGA"; //Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_elem_act_c_iid"].ToString());
                                    obj.s_requisito_producto_apertura = "Requisito del tipo " + gvReservas.DataKeys[i].Values["pub_prod_c_vnomb"].ToString() + " para APERTURA"; //Convert.ToInt16(gvReservas.DataKeys[i].Values["pub_elem_act_c_iid"].ToString());
                                    
                                    lista.Add(obj);
                                    cont_tab++;
                                }
                            }
                        }
                        dtlistEspacios.DataSource = lista;
                        dtlistEspacios.DataBind();

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                    }
                }
                else
                {
                    lblmensajeAccion.Text = "Debe seleccionar un espacio publicitario.";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }

        void cargarComboEjecutivosBTL()//registro - edicion
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            IList<DIO_SP_EJECUTIVO_BTL_LISTAR_Result> lista = _blReserveSpace.f_listar_ejecutivosBTLBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "usua_c_cdoc_id", "nombCompletoEjecutivo", ddlEjecutivoReserva, "SELECCIONE", "0");
                int iPerfil = Convert.ToInt32(Session["S_COD_PERFIL"]);

                if (ViewState["vs_ejec_c_cdoc_id"] == "") // si viene del registro
                {
                    if (iPerfil == Convert.ToInt32(IEnum.Perfiles.EjecutivoVta_BTL) || iPerfil == Convert.ToInt32(IEnum.Perfiles.JefeVta_BTL))
                    {
                        ddlEjecutivoReserva.SelectedValue = objUsuario.usua_c_cdoc_id;
                        ddlEjecutivoReserva.Enabled = false;
                    }
                }
                else // si viene del formulario Mis Reservas.
                {
                    ddlEjecutivoReserva.SelectedValue = ViewState["vs_ejec_c_cdoc_id"].ToString();
                    ddlEjecutivoReserva.Enabled = false;
                }
            }
        }
        protected void rbContinuoReserva_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbContinuoReserva = (RadioButton)sender;
            DataListItem datalistPadre = (DataListItem)rbContinuoReserva.NamingContainer;
            RadioButton rbIntermitenteReserva = (RadioButton)datalistPadre.FindControl("rbIntermitenteReserva");
            TextBox txtfechaDesdeReserva = (TextBox)datalistPadre.FindControl("txtfechaDesdeReserva");
            TextBox txtfechaHastaReserva = (TextBox)datalistPadre.FindControl("txtfechaHastaReserva");
            Calendar calendarFechasReserva = (Calendar)datalistPadre.FindControl("calendarFechasReserva");
            GridView gvFechasReserva = (GridView)datalistPadre.FindControl("gvFechasReserva");
            Label lblidespacioReserva = (Label) datalistPadre.FindControl("lblidespacioReserva");
         
            Dictionary<int, List<DateTime>> dicFechas;
            if (ViewState["TempDicFecha"] != null)
                dicFechas = (Dictionary<int, List<DateTime>>)ViewState["TempDicFecha"];
            else
                dicFechas = new Dictionary<int, List<DateTime>>();
            if (dicFechas == null)
                dicFechas = new Dictionary<int, List<DateTime>>();

            dicFechas[Convert.ToInt32(lblidespacioReserva.Text)] = new List<DateTime>();
            gvFechasReserva.DataSource = new List<DateTime>();
            gvFechasReserva.DataBind();
            ViewState["TempDicFecha"] = dicFechas;
        
            rbIntermitenteReserva.Checked = false;
            txtfechaDesdeReserva.Enabled = true;
            txtfechaHastaReserva.Enabled = true;
            calendarFechasReserva.Visible = false;
            gvFechasReserva.Visible = false;

        }
        protected void rbIntermitenteReserva_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbIntermitenteReserva = (RadioButton)sender;
            DataListItem datalistPadre = (DataListItem)rbIntermitenteReserva.NamingContainer;
            RadioButton rbContinuoReserva = (RadioButton)datalistPadre.FindControl("rbContinuoReserva");
            TextBox txtfechaDesdeReserva = (TextBox)datalistPadre.FindControl("txtfechaDesdeReserva");
            TextBox txtfechaHastaReserva = (TextBox)datalistPadre.FindControl("txtfechaHastaReserva");
            Calendar calendarFechasReserva = (Calendar)datalistPadre.FindControl("calendarFechasReserva");
            GridView gvFechasReserva = (GridView)datalistPadre.FindControl("gvFechasReserva");
          
            txtfechaDesdeReserva.Text = "";
            txtfechaHastaReserva.Text = "";

            rbContinuoReserva.Checked = false;
            txtfechaDesdeReserva.Enabled = false;
            txtfechaHastaReserva.Enabled = false;
            calendarFechasReserva.Visible = true;
            gvFechasReserva.Visible = true;
        }
        protected void dtlistEspacios_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DropDownList ddlTipoAsignacion = (DropDownList)e.Item.FindControl("ddlTipoAsignacion");
            RadioButton rbIntermitenteReserva = e.Item.FindControl("rbIntermitenteReserva") as RadioButton;
            RadioButton rbContinuoReserva = e.Item.FindControl("rbContinuoReserva") as RadioButton;

            TextBox txtfechaDesdeReserva = e.Item.FindControl("txtfechaDesdeReserva") as TextBox;
            TextBox txtfechaHastaReserva = e.Item.FindControl("txtfechaHastaReserva") as TextBox;

            TextBox txtDimensionReservaGv = e.Item.FindControl("txtDimensionReserva") as TextBox;
            Label lblmedidaReserva = (Label)e.Item.FindControl("lblmedidaReserva");
            txtDimensionReservaGv.Text = lblmedidaReserva.Text;

            txtfechaDesdeReserva.Attributes["ReadOnly"] = "readonly";
            txtfechaHastaReserva.Attributes["ReadOnly"] = "readonly";


            f_cargarComboTipoAsignacionReserva(ddlTipoAsignacion);

            DropDownList ddlElementoReserva = (DropDownList)e.Item.FindControl("ddlElementoReserva");
            Label lblidespacioReserva = (Label)e.Item.FindControl("lblidespacioReserva");
            int idespacio = Convert.ToInt16(lblidespacioReserva.Text);

            Label lblidelementoReserva = (Label)e.Item.FindControl("lblidelementoReserva");
            int idelemento = Convert.ToInt16(lblidelementoReserva.Text);

            Label lblidproductoReserva = (Label)e.Item.FindControl("lblidproductoReserva");
            int idproducto = Convert.ToInt16(lblidproductoReserva.Text);

            Label lblRequeridoDimensionReserva = (Label)e.Item.FindControl("lblRequeridoDimensionReserva");

            if (idproducto == 1) lblRequeridoDimensionReserva.Text = "";
            else lblRequeridoDimensionReserva.Text = "(*)";

            m_CargarElementosxEspacioWS(ddlElementoReserva, idespacio, idproducto);
            ddlElementoReserva.SelectedValue = Convert.ToString(idelemento);

            Label lblidreser_iid = (Label)e.Item.FindControl("lblidreser_iid");
            int idreserva = Convert.ToInt16(lblidreser_iid.Text);

            Label lblMsjEstadoReserva = (Label)e.Item.FindControl("lblMsjEstadoReserva");
            

            //==================JAIR==================================
            Label lblDetalleEntrega = (Label)e.Item.FindControl("lblDetalleEntrega");
            Label lblDetalleApertura = (Label)e.Item.FindControl("lblDetalleApertura");
            m_MostrarRequisitoHTML(Convert.ToInt32(ddlElementoReserva.SelectedItem.Value), lblDetalleEntrega, lblDetalleApertura);
            
            //=====================JAIR===============================

            if (idreserva != 0) //reserva en memoria, cargar los datos en la tabla.
            {
                lblMsjEstadoReserva.Text = "Reservado";
                _blReserveSpace = new ReserveSpaceAdvertisingBL();
                DIO_PUB_T_RESERVA objreserva = _blReserveSpace.f_reserva_seleccionar_xidBL(idreserva);

                TextBox txtDescActivacionReserva = e.Item.FindControl("txtDescActivacionReserva") as TextBox;
                TextBox txtDimensionReserva = e.Item.FindControl("txtDimensionReserva") as TextBox;
                TextBox txtComentarioReserva = e.Item.FindControl("txtComentarioReserva") as TextBox;
                TextBox txtPrecioFinReserva = e.Item.FindControl("txtPrecioFinReserva") as TextBox;

                ddlTipoAsignacion.SelectedValue = objreserva.tip_asig_c_iid.ToString();

                txtDescActivacionReserva.Text = objreserva.reser_c_vdesc_activacion;
                txtDimensionReserva.Text = objreserva.reser_c_vdim_final;
                txtComentarioReserva.Text = objreserva.reser_c_vcomen_activacion;
                txtPrecioFinReserva.Text = objreserva.reser_c_eprecio_alquiler.ToString();

                if (objreserva.tipo_periodo_c_iid == 1)//periodo continuo
                {
                    rbContinuoReserva.Checked = true;
                    rbContinuoReserva_CheckedChanged(rbContinuoReserva, null);
                    txtfechaDesdeReserva.Text = Convert.ToDateTime(objreserva.reser_c_dfech_inicio).ToShortDateString();
                    txtfechaHastaReserva.Text = Convert.ToDateTime(objreserva.reser_c_dfech_fin).ToShortDateString();
                }
                else if (objreserva.tipo_periodo_c_iid == 2)
                {
                    rbIntermitenteReserva.Checked = true;
                    rbIntermitenteReserva_CheckedChanged(rbIntermitenteReserva, null);
                    GridView gvReserva = e.Item.FindControl("gvFechasReserva") as GridView;
                    //cargar las fechas del periodo intermitente!!!!
                    _blReserveSpace = new ReserveSpaceAdvertisingBL();
                    List<DIO_SP_PUB_RESERVA_DET_LISTAR_Result> listdetalle = new List<DIO_SP_PUB_RESERVA_DET_LISTAR_Result>();
                    listdetalle = _blReserveSpace.f_listar_detallexReservaBL(idreserva);
                    Dictionary<int, List<DateTime>> dicFechas = new Dictionary<int, List<DateTime>>();
                    dicFechas.Add(idespacio, new List<DateTime>());
                    List<DateTime> lisFechas = dicFechas[idespacio];
                    foreach (var item in listdetalle)
                    {
                        lisFechas.Add((DateTime)item.Fecha);
                    }
                    gvReserva.DataSource = lisFechas;
                    gvReserva.DataBind();
                    ViewState["TempDicFecha"] = dicFechas;
                }
            }
            else
            {
                lblMsjEstadoReserva.Text = "Pendiente de reserva";
                if (rbIntermitenteReserva.Checked) rbIntermitenteReserva_CheckedChanged(rbIntermitenteReserva, null);
                else rbContinuoReserva_CheckedChanged(rbContinuoReserva, null);

            }
        }

        void f_cargarComboTipoAsignacionReserva(DropDownList ddlTipoAsignacion)
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<DIO_PUB_T_TIPO_ASIGNACION> lista = _blReserveSpace.f_listar_pub_tipo_asignacionBL();
            if (lista.Count > 0)
            {
                UIPage.Fill(lista, "tip_asig_c_iid", "tip_asig_c_vnomb", ddlTipoAsignacion, "SELECCIONE", "0");
            }
        }

        void m_CargarElementosxEspacioWS(DropDownList ddlElementoReserva, int id_espacio, int id_producto)
        {
            string s_BaseURL = WebConfigurationManager.AppSettings["RutaURLSWElementoxEspacio"].ToString() + "&json={\"pi_IdEspacio\":" + id_espacio + "}";

            WebClient n = new WebClient();
            n.Encoding = System.Text.Encoding.UTF8;
            var json = n.DownloadString(s_BaseURL);

            JObject o = JObject.Parse(json);

            string ps_result = "metodBomListarElementoxEspacioResult";
            string ps_DataSet = "DsBomElemento";
            string ps_DataTable = "DtBomElemento";

            var obj = new
            {
                PUB_ELEM_ACT_C_IID = "",
                PUB_ELEM_ACT_C_VNOMB = ""
            };
            var lista = new[] { obj }.ToList();
            lista.Clear();
            if (o[ps_result]["diffgr:diffgram"][ps_DataSet] == null)
            {
                lblmensajeAccion.Text = "No se pudo cargar la página.";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                return ;
            }
            if (id_producto == 1)
            {
                foreach (var item in o)
                {
                    if (item.Key == ps_result)
                    {
                        lista.Add(new
                        {
                            PUB_ELEM_ACT_C_IID = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_IID"]),
                            PUB_ELEM_ACT_C_VNOMB = UIFunciones.f_StringIsNull(item.Value["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_VNOMB"])
                        });
                        break;
                    }
                }
            }
            else if (id_producto == 2)
            {
                foreach (var item in o[ps_result]["diffgr:diffgram"][ps_DataSet][ps_DataTable])
                {
                    if (item.Count() == 1)
                        lista.Add(new
                        {
                            PUB_ELEM_ACT_C_IID = UIFunciones.f_StringIsNull(o[ps_result]["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_IID"]),
                            PUB_ELEM_ACT_C_VNOMB = UIFunciones.f_StringIsNull(o[ps_result]["diffgr:diffgram"][ps_DataSet][ps_DataTable]["PUB_ELEM_ACT_C_VNOMB"])
                        });
                    else
                        lista.Add(new
                        {
                            PUB_ELEM_ACT_C_IID = UIFunciones.f_StringIsNull(item["PUB_ELEM_ACT_C_IID"]),
                            PUB_ELEM_ACT_C_VNOMB = UIFunciones.f_StringIsNull(item["PUB_ELEM_ACT_C_VNOMB"])
                        });
                }
            }
            UIPage.Fill(lista, "PUB_ELEM_ACT_C_IID", "PUB_ELEM_ACT_C_VNOMB", ddlElementoReserva, "SELECCIONE", "0");
        }

        protected void gvFechas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                GridView gvFechasReserva = (GridView)sender;

                DataListItem datalistPadre = (DataListItem)gvFechasReserva.NamingContainer;
                Label lblidespacioReserva = datalistPadre.FindControl("lblidespacioReserva") as Label;

                int iIndex = Convert.ToInt32(e.CommandArgument);
                m_EliminarFechaTabla(gvFechasReserva, Convert.ToInt32(lblidespacioReserva.Text), iIndex);
            }
        }
        private void m_EliminarFechaTabla(GridView gvFechasReserva, int pi_despacioReserva, int pi_Index)
        {
            Dictionary<int, List<DateTime>> dicFechas;
            if (ViewState["TempDicFecha"] != null)
                dicFechas = (Dictionary<int, List<DateTime>>)ViewState["TempDicFecha"];
            else
                dicFechas = new Dictionary<int, List<DateTime>>();
            if (dicFechas == null)
                dicFechas = new Dictionary<int, List<DateTime>>();
            List<DateTime> lisFechas = dicFechas[pi_despacioReserva];

            lisFechas.Remove(lisFechas[pi_Index]);
            gvFechasReserva.DataSource = lisFechas;
            gvFechasReserva.DataBind();
            ViewState["TempDicFecha"] = dicFechas;
        }
        private void m_AgregarFecha(GridView gvFechasReserva, int pi_despacioReserva, DateTime pdt_Fecha)
        {
            Dictionary<int, List<DateTime>> dicFechas;
            if (ViewState["TempDicFecha"] != null)
                dicFechas = (Dictionary<int, List<DateTime>>)ViewState["TempDicFecha"];
            else
                dicFechas = new Dictionary<int, List<DateTime>>();
            if (dicFechas == null)
                dicFechas = new Dictionary<int, List<DateTime>>();
            List<DateTime> lisFechas;
            if (dicFechas.ContainsKey(pi_despacioReserva))
                lisFechas = dicFechas[pi_despacioReserva];
            else
            {
                dicFechas.Add(pi_despacioReserva, new List<DateTime>());
                lisFechas = dicFechas[pi_despacioReserva];
            }
            if (!lisFechas.Contains(pdt_Fecha))
                if (pdt_Fecha > DateTime.Now)
                    lisFechas.Add(pdt_Fecha);
            lisFechas.Sort();
            gvFechasReserva.DataSource = lisFechas;
            gvFechasReserva.DataBind();
            ViewState["TempDicFecha"] = dicFechas;
        }

        protected void calendarFechasReserva_SelectionChanged(object sender, EventArgs e)
        {
            Calendar calendarReserva = (Calendar)sender;
            DataListItem datalistPadre = (DataListItem)calendarReserva.NamingContainer;
            GridView gvReserva = (GridView)datalistPadre.FindControl("gvFechasReserva");
            Label lblidespacioReserva = datalistPadre.FindControl("lblidespacioReserva") as Label;
            m_AgregarFecha(gvReserva, Convert.ToInt32(lblidespacioReserva.Text), calendarReserva.SelectedDate);
            calendarReserva.SelectedDate = new DateTime();
        }
        protected void btnGuardarReserva_Click(object sender, EventArgs e)
        {
            guardarReservas();
        }
        string validarDatosReservaTabs()
        {
            string mensajeFinal = "";
            string mensajeTab = "";
            if (ddlEjecutivoReserva.SelectedValue == "0")
            {
                mensajeFinal = "- Seleccione el ejecutivo BTL.<br/>";
            }
            foreach (DataListItem control in dtlistEspacios.Controls)
            {
                mensajeTab = "";
                //
                Label nombTabReserva = control.FindControl("lblnombTabReserva") as Label;
                //
                DropDownList ddlElementoReserva = control.FindControl("ddlElementoReserva") as DropDownList;
                TextBox txtDescActivacionReserva = control.FindControl("txtDescActivacionReserva") as TextBox;
                DropDownList ddlTipoAsignacion = control.FindControl("ddlTipoAsignacion") as DropDownList;
                TextBox txtDimensionReserva = control.FindControl("txtDimensionReserva") as TextBox;
                TextBox txtPrecioFinReserva = control.FindControl("txtPrecioFinReserva") as TextBox;

                RadioButton rbIntermitenteReserva = control.FindControl("rbIntermitenteReserva") as RadioButton;
                RadioButton rbContinuoReserva = control.FindControl("rbContinuoReserva") as RadioButton;

                TextBox txtfechaDesdeReserva = control.FindControl("txtfechaDesdeReserva") as TextBox;
                TextBox txtfechaHastaReserva = control.FindControl("txtfechaHastaReserva") as TextBox;

                GridView gvfechasReserva = control.FindControl("gvFechasReserva") as GridView;

                Label lblidproductoReserva = control.FindControl("lblidproductoReserva") as Label;

                mensajeTab = f_validarControlesxTab(Convert.ToInt32(lblidproductoReserva.Text), ddlElementoReserva, txtDescActivacionReserva, ddlTipoAsignacion,
                              txtDimensionReserva, txtPrecioFinReserva, rbIntermitenteReserva, rbContinuoReserva,
                              txtfechaDesdeReserva, txtfechaHastaReserva, gvfechasReserva);
               
                if (mensajeTab != "")
                {
                    mensajeFinal += "* " + nombTabReserva.Text + "<br/>" + mensajeTab + "<br/>";
                }
            }

            return mensajeFinal;
        }

        string f_validarControlesxTab(int idproducto,DropDownList ddlElementoReserva, TextBox txtDescActivacionReserva,
          DropDownList ddlTipoAsignacion, TextBox txtDimensionReserva, TextBox txtPrecioFinReserva,
          RadioButton rbIntermitenteReserva, RadioButton rbContinuoReserva, TextBox txtfechaDesdeReserva,
          TextBox txtfechaHastaReserva, GridView gvfechasReserva)
        {
            bool rangofechasCorrecto = true;
            string msjvalidacionTab = "";
            if (Convert.ToInt16(ddlElementoReserva.SelectedValue) == 0)
            {
                msjvalidacionTab += "  - Seleccione el tipo de activación.<br/>";
            }
            if (txtDescActivacionReserva.Text.Trim() == "")
            {
                msjvalidacionTab += "  - Ingrese la descripción de ativación o elemento.<br/>";
            }
            if (Convert.ToInt16(ddlTipoAsignacion.SelectedValue) == 0)
            {
                msjvalidacionTab += "  - Seleccione el tipo de asignación.<br/>";
            }
            if (idproducto == 2)//si es BTL validar que no sea opcional
            {
                if (txtDimensionReserva.Text.Trim() == "")
                {
                    msjvalidacionTab += "  - Ingrese la dimensión final del espacio BTL.<br/>";
                }
            }

            if (txtPrecioFinReserva.Text.Trim() != "")
            {
                if (!montoDecimalValido(txtPrecioFinReserva.Text.Trim()))
                    msjvalidacionTab += "  - El precio final de alquiler tiene formato incorrecto.<br/>";
            }

            if (!rbContinuoReserva.Checked && !rbIntermitenteReserva.Checked)
            {
                msjvalidacionTab += "- Seleccione un periodo.<br/>";
            }
            else
            {
                if (rbContinuoReserva.Checked)
                {
                    if (txtfechaDesdeReserva.Text.Trim() == "")
                    {
                        msjvalidacionTab += "- Ingrese la fecha DESDE del periodo continuo.<br/>";
                        rangofechasCorrecto = false;
                    }

                    else if (!fechaValida(txtfechaDesdeReserva.Text.Trim()))
                    {
                        msjvalidacionTab += "- Fecha DESDE del periodo continuo inválida.<br/>";
                        rangofechasCorrecto = false;
                    }

                    if (txtfechaHastaReserva.Text.Trim() == "")
                    {
                        msjvalidacionTab += "- Ingrese la fecha HASTA del periodo continuo.<br/>";
                        rangofechasCorrecto = false;
                    }
                    else if (!fechaValida(txtfechaHastaReserva.Text.Trim()))
                    {
                        msjvalidacionTab += "- Fecha HASTA del periodo continuo inválida.<br/>";
                        rangofechasCorrecto = false;
                    }
                    if (rangofechasCorrecto)
                    {
                        // validar mayor en fechas 
                        string fechaActual = DateTime.Today.ToShortDateString();
                        string fechaDesde = Convert.ToDateTime(txtfechaDesdeReserva.Text.Trim()).ToShortDateString();
                        string fechaHasta = Convert.ToDateTime(txtfechaHastaReserva.Text.Trim()).ToShortDateString();
                        DateTime dfechaDesde = Convert.ToDateTime(fechaDesde);
                        DateTime dfechaHasta = Convert.ToDateTime(fechaHasta);
                        DateTime dfechaActual = Convert.ToDateTime(fechaActual);
                        if (dfechaDesde < dfechaActual)
                            msjvalidacionTab += "- La fecha DESDE no debe ser menor a la fecha actual.</br>";
                        if (dfechaDesde > dfechaHasta) //fecha desde es mayor que fecha hasta
                        {
                            msjvalidacionTab += "- La fecha DESDE no debe ser mayor a la fecha HASTA.</br>";
                        }
                    }
                }
                else if (rbIntermitenteReserva.Checked)
                {
                    if ((gvfechasReserva.Rows.Count == 0))
                    {
                        msjvalidacionTab += "- Seleccione fecha(s) para la reserva en el periodo intermitente.</br>";
                    }
                }
            }
            return msjvalidacionTab;
        }
        private bool montoDecimalValido(string campo)
        {
            bool ver = true;
            try
            {
                decimal date = Convert.ToDecimal(campo);
                return ver;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void guardarReservas(bool btnasociacion = false)
        {
            try
            {
                litvalidacionReservas.Text = "";
                ViewState["vs_accion"] = "Registro";
                string validarDatosReserva = validarDatosReservaTabs();
                DIO_PUB_T_RESERVA objReserva;
                DIO_PUB_T_RESERVA_MASTER objReservaM;
                var sb = new StringBuilder();
                int idReservaMaster = 0;

                if (validarDatosReserva == "")
                {
                    if (Convert.ToInt16(ViewState["vs_id_reserva_master"].ToString()) == 0)
                    {
                        objReservaM = new DIO_PUB_T_RESERVA_MASTER();
                        //guardar reserva MASTER.
                        _blReserveSpace = new ReserveSpaceAdvertisingBL();
                        objReservaM.ejec_c_cdoc_id = ddlEjecutivoReserva.SelectedItem.Value;
                        objReservaM.bita_c_vnomb_completo_reg = objUsuario.usua_c_cape_nombres;
                        objReservaM.bita_c_vusu_red_reg = objUsuario.usua_c_cusu_red;
                        idReservaMaster = _blReserveSpace.f_guardar_reserva_masterBL(objReservaM);

                        ViewState["vs_id_reserva_master"] = idReservaMaster;
                        ViewState["vs_ejec_c_cdoc_id"] = objReservaM.ejec_c_cdoc_id;
                    }
                    else
                    {
                        idReservaMaster=Convert.ToInt16(ViewState["vs_id_reserva_master"].ToString());
                    }

                    foreach (DataListItem control in dtlistEspacios.Controls)
                    {
                        objReserva = new DIO_PUB_T_RESERVA();
                        objReserva.reser_c_dfech_vencimiento = Convert.ToDateTime(lblfechavencimientoReserva.InnerText);

                        Label lblidreser_iid = control.FindControl("lblidreser_iid") as Label;
                        Label lblcodEspacioReserva = control.FindControl("lblcodEspacioReserva") as Label;

                        if (Convert.ToInt16(lblidreser_iid.Text) == 0) // se habla de registro!
                        {

                            Label lblTarifaFriaReserva = control.FindControl("lblfriaReserva") as Label;
                            Label lblCostoProduccReserva = control.FindControl("lblbaseReserva") as Label;
                            Label lblareaReserva = control.FindControl("lblareaReserva") as Label;
                            Label lblmedidaReserva = control.FindControl("lblmedidaReserva") as Label;

                            objReserva.pub_esp_c_earea = Convert.ToDecimal(lblareaReserva.Text);
                            objReserva.pub_esp_c_vmedida = lblmedidaReserva.Text;
                            objReserva.pub_esp_c_emonto_tarifa_top = Convert.ToDecimal(lblTarifaFriaReserva.Text);
                            objReserva.pub_esp_c_emonto_tarifa_base = Convert.ToDecimal(lblCostoProduccReserva.Text);


                            Label lblidespacioReserva = control.FindControl("lblidespacioReserva") as Label;
                            Label lblidinmuebleReserva = control.FindControl("lblidinmuebleReserva") as Label;
                            Label lblidproductoReserva = control.FindControl("lblidproductoReserva") as Label;
                            Label lblnombreproductoReserva = control.FindControl("lblProductoReserva") as Label;

                            objReserva.pub_esp_c_iid = Convert.ToInt16(lblidespacioReserva.Text);
                            objReserva.inm_c_icod = Convert.ToInt16(lblidinmuebleReserva.Text);
                            objReserva.pub_prod_c_iid = Convert.ToInt16(lblidproductoReserva.Text);
                            objReserva.pub_esp_c_vcod = lblcodEspacioReserva.Text;
                            objReserva.pub_prod_c_vnomb = lblnombreproductoReserva.Text;

                            RadioButton rbIntermitenteReserva = control.FindControl("rbIntermitenteReserva") as RadioButton;
                            RadioButton rbContinuoReserva = control.FindControl("rbContinuoReserva") as RadioButton;
                            if (rbContinuoReserva.Checked)
                            {
                                objReserva.tipo_periodo_c_iid = 1;//CONTINUO
                            }
                            else if (rbIntermitenteReserva.Checked)
                            {
                                objReserva.tipo_periodo_c_iid = 2;//INTERMITENTE
                            }

                            DropDownList ddlElementoReserva = control.FindControl("ddlElementoReserva") as DropDownList;
                            DropDownList ddlTipoAsignacion = control.FindControl("ddlTipoAsignacion") as DropDownList;

                            objReserva.pub_elem_act_c_iid = Convert.ToInt16(ddlElementoReserva.SelectedItem.Value);
                            objReserva.tip_asig_c_iid = Convert.ToInt16(ddlTipoAsignacion.SelectedItem.Value);

                            TextBox txtDescActivacionReserva = control.FindControl("txtDescActivacionReserva") as TextBox;
                            TextBox txtDimensionReserva = control.FindControl("txtDimensionReserva") as TextBox;
                            TextBox txtComentarioReserva = control.FindControl("txtComentarioReserva") as TextBox;
                            TextBox txtPrecioFinReserva = control.FindControl("txtPrecioFinReserva") as TextBox;

                            objReserva.reser_c_vdesc_activacion = txtDescActivacionReserva.Text.Trim();
                            objReserva.reser_c_vdim_final = txtDimensionReserva.Text.Trim();
                            objReserva.reser_c_vcomen_activacion = txtComentarioReserva.Text.Trim();


                            if (string.IsNullOrEmpty(txtPrecioFinReserva.Text.Trim()))
                            {
                                objReserva.reser_c_eprecio_alquiler = 0;
                            }
                            else objReserva.reser_c_eprecio_alquiler = Convert.ToDecimal(txtPrecioFinReserva.Text.Trim());


                            _blReserveSpace = new ReserveSpaceAdvertisingBL();
                            //guardar cabecera.
                            objReserva.reser_mast_c_iid = idReservaMaster;
                            objReserva.bita_c_vnomb_completo_reg = objUsuario.usua_c_cape_nombres;
                            objReserva.bita_c_vusu_red_reg = objUsuario.usua_c_cusu_red;

                            int idReserva = _blReserveSpace.f_guardar_reserva_cabeceraBL(objReserva);

                            lblidreser_iid.Text = idReserva.ToString();
                            string fechasValidadas = "";
                            if (objReserva.tipo_periodo_c_iid == 1) //PERIODO CONTINUO
                            {
                                TextBox txtfechaDesdeReserva = control.FindControl("txtfechaDesdeReserva") as TextBox;
                                TextBox txtfechaHastaReserva = control.FindControl("txtfechaHastaReserva") as TextBox;
                                fechasValidadas = guardarDetalleReservaContinuo(txtfechaDesdeReserva, txtfechaHastaReserva, idReserva, objReserva.tipo_periodo_c_iid, objReserva.pub_esp_c_iid);
                                if (!string.IsNullOrEmpty(fechasValidadas))
                                {
                                    if (_blReserveSpace.f_eliminar_Reserva_CopadaTotalmenteBL(idReserva))
                                    {
                                        sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>No se procedió con el registro, las fechas indicadas para el espacio " + lblcodEspacioReserva.Text + ": estan copadas en su totalidad</strong><br/></div>");
                                        lblidreser_iid.Text = "0";
                                    }
                                    else
                                    sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>Fechas no registradas para " + lblcodEspacioReserva.Text + ":</strong><br/>" + fechasValidadas + "</div>");
                                }
                             }
                            else if (objReserva.tipo_periodo_c_iid == 2)//PERIODO INTERMITENTE
                            {
                                GridView gvReserva = control.FindControl("gvFechasReserva") as GridView;
                                fechasValidadas = guardarDetalleReservaIntermitente(gvReserva, idReserva, objReserva.tipo_periodo_c_iid, objReserva.pub_esp_c_iid);
                                if (!string.IsNullOrEmpty(fechasValidadas))
                                {
                                    if (_blReserveSpace.f_eliminar_Reserva_CopadaTotalmenteBL(idReserva))
                                    {
                                        sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>No se procedió con el registro, las fechas indicadas para el espacio " + lblcodEspacioReserva.Text + ": estan copadas en su totalidad</strong><br/></div>");
                                        lblidreser_iid.Text = "0";
                                    }
                                    else
                                    sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>Fechas no registradas para " + lblcodEspacioReserva.Text + ":</strong><br/>" + fechasValidadas + "</div>");
                                }
                            }
                        }
                        else {// se habla de modificacion
                            Label lblidespacioReserva = control.FindControl("lblidespacioReserva") as Label;
                            RadioButton rbIntermitenteReserva = control.FindControl("rbIntermitenteReserva") as RadioButton;
                            RadioButton rbContinuoReserva = control.FindControl("rbContinuoReserva") as RadioButton;
                            if (rbContinuoReserva.Checked)
                            {
                                objReserva.tipo_periodo_c_iid = 1;//CONTINUO
                            }
                            else if (rbIntermitenteReserva.Checked)
                            {
                                objReserva.tipo_periodo_c_iid = 2;//INTERMITENTE
                            }
                            DropDownList ddlElementoReserva = control.FindControl("ddlElementoReserva") as DropDownList;
                            DropDownList ddlTipoAsignacion = control.FindControl("ddlTipoAsignacion") as DropDownList;
                            objReserva.pub_esp_c_iid = Convert.ToInt16(lblidespacioReserva.Text);
                            objReserva.pub_elem_act_c_iid = Convert.ToInt16(ddlElementoReserva.SelectedItem.Value);
                            objReserva.tip_asig_c_iid = Convert.ToInt16(ddlTipoAsignacion.SelectedItem.Value);

                            TextBox txtDescActivacionReserva = control.FindControl("txtDescActivacionReserva") as TextBox;
                            TextBox txtDimensionReserva = control.FindControl("txtDimensionReserva") as TextBox;
                            TextBox txtComentarioReserva = control.FindControl("txtComentarioReserva") as TextBox;
                            TextBox txtPrecioFinReserva = control.FindControl("txtPrecioFinReserva") as TextBox;

                            objReserva.reser_c_vdesc_activacion = txtDescActivacionReserva.Text.Trim();
                            objReserva.reser_c_vdim_final = txtDimensionReserva.Text.Trim();
                            objReserva.reser_c_vcomen_activacion = txtComentarioReserva.Text.Trim();

                             if (string.IsNullOrEmpty(txtPrecioFinReserva.Text.Trim()))
                            {
                                objReserva.reser_c_eprecio_alquiler = 0;
                            }
                            else objReserva.reser_c_eprecio_alquiler = Convert.ToDecimal(txtPrecioFinReserva.Text.Trim());


                            int idReserva = Convert.ToInt16(lblidreser_iid.Text);
                            objReserva.reser_c_iid = idReserva;

                            _blReserveSpace = new ReserveSpaceAdvertisingBL();

                            //guardar cabecera y desactivar su detalle.
                            _blReserveSpace.f_actualizar_reserva_cabeceraBL(objReserva);

                            //MODIFICAR PRIORIDAD
                            _blReserveSpace.f_modificar_Prioridad_ReservaBL(idReserva);

                            //volver a registrar el detalle de las reservas.
                            string fechasValidadas = "";
                            if (objReserva.tipo_periodo_c_iid == 1) //PERIODO CONTINUO
                            {
                                TextBox txtfechaDesdeReserva = control.FindControl("txtfechaDesdeReserva") as TextBox;
                                TextBox txtfechaHastaReserva = control.FindControl("txtfechaHastaReserva") as TextBox;
                                fechasValidadas = guardarDetalleReservaContinuo(txtfechaDesdeReserva, txtfechaHastaReserva, idReserva, objReserva.tipo_periodo_c_iid, objReserva.pub_esp_c_iid);
                                if (!string.IsNullOrEmpty(fechasValidadas))
                                {
                                    if (_blReserveSpace.f_eliminar_Reserva_CopadaTotalmenteBL(idReserva))
                                    {
                                        sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>No se procedió con el registro, las fechas indicadas para el espacio " + lblcodEspacioReserva.Text + ": estan copadas en su totalidad</strong><br/></div>");
                                        lblidreser_iid.Text = "0";
                                    }
                                    else
                                    sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>Fechas no registradas para " + lblcodEspacioReserva.Text + ":</strong><br/>" + fechasValidadas + "</div>");
                                }
                            }
                            else if (objReserva.tipo_periodo_c_iid == 2)//PERIODO INTERMITENTE
                            {
                                GridView gvReserva = control.FindControl("gvFechasReserva") as GridView;
                                fechasValidadas = guardarDetalleReservaIntermitente(gvReserva, idReserva, objReserva.tipo_periodo_c_iid, objReserva.pub_esp_c_iid);
                                if (!string.IsNullOrEmpty(fechasValidadas))
                                {
                                    if (_blReserveSpace.f_eliminar_Reserva_CopadaTotalmenteBL(idReserva))
                                    {
                                        sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>No se procedió con el registro, las fechas indicadas para el espacio " + lblcodEspacioReserva.Text + ": estan copadas en su totalidad</strong><br/></div>");
                                        lblidreser_iid.Text = "0";
                                    }
                                    else
                                    sb.Append("<div class='alert alert-danger' style='word-wrap:break-word;'>" + "<strong>Fechas no registradas para " + lblcodEspacioReserva.Text + ":</strong><br/>" + fechasValidadas + "</div>");
                                }
                            }
                        }
                    }

                    if (sb.ToString().Length == 0)
                    {
                        if (btnasociacion == false)
                        {
                            if (_blReserveSpace.f_eliminar_Reserva_Master_CopadaTotalmenteBL(idReservaMaster))
                            {
                                lblmensajeAccion.Text = "No se registró ninguna reserva, las fechas indicadas estan copadas en su totalidad";
                                ViewState["vs_id_reserva_master"] = 0;
                            }
                            else
                            {
                                lblmensajeAccion.Text = "Se realizó el registro de las reservas para los espacios satisfactoriamente.";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                            }
                        }
                    }
                    else
                    {
                        if (btnasociacion == false)
                        {
                            if (_blReserveSpace.f_eliminar_Reserva_Master_CopadaTotalmenteBL(idReservaMaster))
                            {
                                lblmensajeAccion.Text = "No se registró ninguna reserva, las fechas indicadas estan copadas en su totalidad";
                                ViewState["vs_id_reserva_master"] = 0;
                            }
                            else
                            {
                                lblmensajeAccion.Text = "Se realizó el registro de las reservas para los espacios satisfactoriamente, sin embargo, hubo fechas que no pudieron ser reservadas, consulte el detalle en el pie de pagina.";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                            }
                        }
                        else
                        {
                            if (_blReserveSpace.f_eliminar_Reserva_Master_CopadaTotalmenteBL(idReservaMaster))
                            {
                                ViewState["vs_id_reserva_master"] = 0;
                            }

                            ViewState["lt_reservas"] = sb.ToString();
                        }
                        litvalidacionReservas.Text = sb.ToString();
                    }
                }
                else
                {
                    lblmensajeAccion.Text = validarDatosReserva;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                }
            }
            catch (Exception ex)
            {
                lblmensajeAccion.Text = "Ocurrió error al registrar las reservas.";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                UIFunciones.m_TraceError(NameCapas.K_APPNAME, objUsuario.usua_c_cusu_red, Environment.MachineName, ex);
            }
        }

        void modificarPrioridad()
        {
            //
        }
        string guardarDetalleReservaContinuo(TextBox txtfechaDesdeReserva, TextBox txtfechaHastaReserva, int idReserva, int tipoPeriodo, int idespacio)
        {
            DIO_PUB_T_RESERVA_DET objDet = new DIO_PUB_T_RESERVA_DET();
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            return _blReserveSpace.f_guardar_reserva_detalleBL(idReserva, idespacio, tipoPeriodo, txtfechaDesdeReserva.Text, txtfechaHastaReserva.Text, "",
                objUsuario.usua_c_cape_nombres, objUsuario.usua_c_cusu_red, ddlEjecutivoReserva.SelectedItem.Value);
        }
        string guardarDetalleReservaIntermitente(GridView gvReserva, int idReserva, int tipoPeriodo, int idespacio)
        {
            string grupoFechasIntermitente = f_obtenerSeleccionFechas(gvReserva);//fechas intermitente.
            //el numero de orden se obtiene por script, en la validacion antes del guardado se hace la consulta si es que 
            //el espacio tiene 3 ordenes.
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            return _blReserveSpace.f_guardar_reserva_detalleBL(idReserva, idespacio, tipoPeriodo, "", "", grupoFechasIntermitente,
                 objUsuario.usua_c_cape_nombres, objUsuario.usua_c_cusu_red, ddlEjecutivoReserva.SelectedItem.Value);
        }

        private string f_obtenerSeleccionFechas(GridView gvReserva)
        {
            string cadena = "";
            bool primerRegistro = true; // variable para que no pinte la coma al inicio
            for (int i = 0; i < gvReserva.Rows.Count; i++)
            {
                if (primerRegistro)
                {
                    cadena += Convert.ToDateTime(gvReserva.DataKeys[i]["Date"]).ToShortDateString();
                    primerRegistro = false;
                }
                else cadena += "," + Convert.ToDateTime(gvReserva.DataKeys[i]["Date"]).ToShortDateString();
            }
            return cadena;
        }
        protected void btnregresResultadoBusqueda_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_RegresarResultadoBusqueda();", true);
        }
        protected void btnAsociarCliente_Click(object sender, EventArgs e)
        {
            guardarReservas(true);
            if (Convert.ToInt16(ViewState["vs_id_reserva_master"])!=0)
            {
                cargarReservarAsociacion(Convert.ToInt16(ViewState["vs_id_reserva_master"].ToString()));
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AsociarCliente();", true);
                if (ViewState["lt_reservas"] != null)
                {
                    ltvalidacionReservaAsoc.Text = ViewState["lt_reservas"].ToString();
                }
                rbclienteAsociacion.Checked = false;
                rbAgenciaAsociacion.Checked = false;
                txtAgenciaAsociacion.Text = "";
                txtClienteAsociacion.Text = "";
                txtMarcaAsociacion.Text = "";
                seleccionarTodosAsociacion();

                lblPaso2.Font.Bold = false;
                lblPaso3.Font.Bold = true;
            }
        }
        void seleccionarTodosAsociacion()
        {
            for (int i = 0; i < gvReservasAsociacion.Rows.Count; i++)
            {
                CheckBox controlCheck = (CheckBox)gvReservasAsociacion.Rows[i].FindControl("checkAsociacion");
                if (controlCheck != null)
                {
                    controlCheck.Checked = true;
                }
            }
        }
        void seleccionarTodosEspaciosBusqueda()
        {
            for (int i = 0; i < gvReservas.Rows.Count; i++)
            {
                CheckBox controlCheck = (CheckBox)gvReservas.Rows[i].FindControl("checkEspacio");
                if (controlCheck != null)
                {
                    controlCheck.Checked = true;
                }
            }
        }

        protected void btnRegresarPaso2_Click(object sender, EventArgs e)
        {
            lblPaso2.Font.Bold = true;
            lblPaso3.Font.Bold = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_RegresaPaso2();", true);
        }
        protected void btnCerrarReserva_Click(object sender, EventArgs e)
        {
            CerrarReservas();
        }
        #endregion 
        #region ASOCIACION DE CLIENTE
        void cargarReservarAsociacion(int reser_mast_c_iid)
        {
            _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<SP_PUB_RESERVA_XIDMASTER_LISTAR_Result> lista = _blReserveSpace.f_listar_reservas_xidmasterBL(reser_mast_c_iid);
            gvReservasAsociacion.DataSource = lista;
            gvReservasAsociacion.DataBind();
        }

        #region "SERVICIO WEB"
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] metodAutocompletarCliente(string prefixText, int count, string contextKey)
        {
            IApproveSpaceSoldBL objAprob = new ApproveSpaceSoldBL();

            List<ADV_T_CLIENTE> lista = objAprob.f_ListarClientesBL();
            return (from x in lista
                    where x.cli_c_vraz_soc.Contains(prefixText.ToUpper())
                    select x.cli_c_vraz_soc).Take(count).ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] metodAutocompletarAgencia(string prefixText, int count, string contextKey)
        {
            ReserveSpaceAdvertisingBL _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<ADV_T_CLIENTE> lista = _blReserveSpace.f_ListarAgenciaBL(); //lista las agencias!!!
            return (from x in lista
                    where x.cli_c_vraz_soc.Contains(prefixText.ToUpper())
                    select x.cli_c_vraz_soc).Take(count).ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] metodAutocompletarMarca(string prefixText, int count, string contextKey)
        {
            ReserveSpaceAdvertisingBL _blReserveSpace = new ReserveSpaceAdvertisingBL();
            List<DIO_PUB_T_MARCA> lista = _blReserveSpace.f_ListarMarcaBL(); //lista las agencias!!!
            return (from x in lista
                    where x.marc_c_vnomb.Contains(prefixText.ToUpper())
                    select x.marc_c_vnomb).Take(count).ToArray();
        } 
        protected void btnAsociar_Click(object sender, EventArgs e)
        {
          try 
	      {	        
	      	string validacion = validarAsociacion();
              if (validacion == "")
              {
                  AsociarReservas();
              }
              else
              {
                  lblmensajeAccion.Text = validacion;
                  ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
              }
	      }
	      catch (Exception)
	      {
	      	throw;
	      }
        }
        void AsociarReservas()
        {
            string vraz_social_cliente = txtClienteAsociacion.Text.Trim();
            string vraz_social_agencia = txtAgenciaAsociacion.Text.Trim();
            string marca = txtMarcaAsociacion.Text.Trim();

            DIO_PUB_T_RESERVA res;
            ADV_T_CLIENTE objcliente;
            ADV_T_CLIENTE objagencia;
            DIO_PUB_T_MARCA objmarca;

           for (int i = 0; i < gvReservasAsociacion.Rows.Count; i++)
           {
               CheckBox controlCheck = (CheckBox)gvReservasAsociacion.Rows[i].FindControl("checkAsociacion");
               if (controlCheck != null)
               {
                   if (controlCheck.Checked)// guardar asociacion (actualizar en la tabla reserva)
                   {
                       res = new DIO_PUB_T_RESERVA();

                       objcliente = new ADV_T_CLIENTE();
                       objmarca = new DIO_PUB_T_MARCA();

                       int idreserva = Convert.ToInt32(gvReservasAsociacion.DataKeys[i].Values["reser_c_iid"].ToString());
                       _blReserveSpace = new ReserveSpaceAdvertisingBL();

                       res.reser_c_iid = idreserva;

                       if (txtAgenciaAsociacion.Text.Trim() != "")
                       {
                           objagencia = new ADV_T_CLIENTE();
                           objagencia = _blReserveSpace.f_seleccionar_agenciaBL(vraz_social_agencia);
                           res.agen_c_vdoc_id = objagencia.cli_c_vdoc_id;
                       }
                       else
                       {
                           res.agen_c_vdoc_id = null;
                       }

                       if (rbAgenciaAsociacion.Checked)
                       {
                           res.reser_mast_c_fac_bagencia = true;
                       }
                       else if (rbclienteAsociacion.Checked)
                       {
                           res.reser_mast_c_fac_bagencia = false;
                       }
                       objcliente = _blReserveSpace.f_seleccionar_clienteBL(vraz_social_cliente);
                       objmarca = _blReserveSpace.f_seleccionar_o_registrar_marcaBL(marca,"SISTEMA","SISTEMA");
                       res.cli_c_vdoc_id = objcliente.cli_c_vdoc_id;
                       res.marc_c_icod = objmarca.marc_c_icod;
                       //ACTUALIZAR CAMPOS 
                       _blReserveSpace.f_asociar_clienteBL(res);
                   }
               }
           }
           rbAgenciaAsociacion.Checked = false;
           rbclienteAsociacion.Checked = false;
           cargarReservarAsociacion(Convert.ToInt16(ViewState["vs_id_reserva_master"]));
        }
        void CerrarReservas()
        {
            try
            {
                _blReserveSpace = new ReserveSpaceAdvertisingBL();
                int result = _blReserveSpace.f_cerrar_ReservaBL(Convert.ToInt16(ViewState["vs_id_reserva_master"]));
                if (result == 1)//cierre satisfactorio
                {
                    lblmensajeAccion.Text = "Se realizó el cierre correctamente.";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                }
                else
                {
                    lblmensajeAccion.Text = "Hay reservas pendientes de asociación.";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                }
            }
            catch (Exception)
            {
                lblmensajeAccion.Text = "Hubo un error al realizar el cierre.";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "javascript:f_AbrirPopAccion();", true);
                throw;
            }
        }
        bool hayseleccionAsociacion()
        {
            bool ver = false;
            for (int i = 0; i < gvReservasAsociacion.Rows.Count; i++)
            {
                CheckBox controlCheck = (CheckBox)gvReservasAsociacion.Rows[i].FindControl("checkAsociacion");
              if (controlCheck != null)
              {
                  if (controlCheck.Checked)// guardar asociacion (actualizar en la tabla reserva)
                  {
                      ver=true;
                  }
              }
            }
            return ver;
        }
        string validarAsociacion()
        {
            string mensaje = "";

            ADV_T_CLIENTE objcliente=null;
            ADV_T_CLIENTE objagencia =null;
            _blReserveSpace = new ReserveSpaceAdvertisingBL();


            if (!hayseleccionAsociacion())
            {
                mensaje += "- No se ha seleccionado ningun espacio.<br/>";
            }
            objcliente = _blReserveSpace.f_seleccionar_clienteBL(txtClienteAsociacion.Text.Trim());
            if (objcliente == null)
            {
                mensaje += "- El cliente no existe.<br/>";
            }

            if (rbAgenciaAsociacion.Checked)
            {
                if (txtAgenciaAsociacion.Text.Trim() == "")
                {
                    mensaje += "- Debe de ingresar la agencia para poder facturar a nombre de ella.<br/>";
                }
                else
                {
                    objagencia = _blReserveSpace.f_seleccionar_agenciaBL(txtAgenciaAsociacion.Text.Trim());
                    if (objagencia == null)
                    {
                        mensaje += "- La agencia no existe.<br/>";
                    }
                }
            }
            if (txtClienteAsociacion.Text.Trim() == "")
            {
                mensaje += "- Debe de ingresar el cliente.<br/>";
            }
            if (txtClienteAsociacion.Text.Trim() == "")
            {
                mensaje += "- Debe de ingresar la marca.<br/>";
            }
            return mensaje;
        }
        #endregion

        protected void gvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string tarifa_fria = gvReservas.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values["pub_esp_c_emonto_tarifa_top"].ToString(); //top
                //string tarifa_base = gvReservas.DataKeys[Convert.ToInt32(e.Row.RowIndex)].Values["pub_esp_c_emonto_tarifa_base"].ToString(); //costo produccion
                //e.Row.Cells[8].Text = "S/ " + tarifa_base;
                //e.Row.Cells[9].Text = "S/ " + tarifa_fria;
            }
        }
        #endregion
    }

}