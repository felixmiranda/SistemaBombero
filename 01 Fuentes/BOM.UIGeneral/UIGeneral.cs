using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Text.RegularExpressions;
using BOM.BusinessLayer;
using BOM.EntityLayer;

using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;

namespace BOM.UIGeneral
{
    public class UIFunciones : System.Web.UI.Page
    {


        /// <summary>
        /// Descripción: Permite validar el valor null
        /// Autor: Jair Tasayco Bautista
        /// Fecha y Hora Creación: 21-02-2017
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <param name="po_obj"></param>
        /// <returns></returns>
        public static string f_StringIsNull(object po_obj)
        {
            if (po_obj == null)
                return "";
            else
                return po_obj.ToString();
        }
        /// <summary>
        /// Descripción: Muestra mensaje Correcto, warning,error
        /// Autor: Miguel Martinez Q.
        /// Fecha y Hora Creación: 27-02-2017
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <param name="po_obj"></param>
        /// <returns></returns>
        public static string f_mostrarMensajeAccion(string accion,string mensaje)
        {
            string html="";
            if (accion == "correcto")
            {

            }
            else if (accion == "alerta")
            {
            }
            else if (accion == "error")
            {
            }
            return html;
        }
        public static string GetNomMes(int iMes)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(iMes);
        }

        /// <summary>
        /// Descripción: Recibe el mensaje y retorna con la clase para dar color(Correcto,Error,Info)
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_Tipo"></param>
        /// <param name="ps_Mensaje"></param>
        /// <returns></returns>
        public string f_EnviarMensaje(string ps_Tipo, string ps_Mensaje)
        {
            string sMensaje = string.Empty;
            if (ps_Tipo == "Correcto")
            {
                sMensaje = "<div class=\"alert alert-success\">" +
                     "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" +
                     "<strong>Correcto! </strong> " + ps_Mensaje + "</div>";
            }
            else if (ps_Tipo == "Error")
            {
                sMensaje = "<div class=\"alert alert-danger\">" +
                     "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" +
                     "<strong>Advertencia! </strong> " + ps_Mensaje + "</div>";
            }
            else if (ps_Tipo == "Info")
            {
                sMensaje = "<div class=\"alert alert-info\">" +
                     "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" +
                     "<strong>Información! </strong> " + ps_Mensaje + "</div>";
            }
            return sMensaje;
        }
        /// <summary>
        /// Descripción: Retorna si el parametro es fecha
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="sFecha"></param>
        /// <returns></returns>
        public bool f_EsFecha(string ps_Fecha)
        {
            DateTime dt = DateTime.Parse(ps_Fecha, new CultureInfo("de-DE")); ;
            return DateTime.TryParse(ps_Fecha, out dt);
        }
        /// <summary>
        /// Descripción: Retorna si un parametro es numero
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool f_EsNumero(string ps_Numero)
        {
            double dNumber;
            return (double.TryParse(ps_Numero, out dNumber));
        }
        /// <summary>
        /// Descripción: Retorna el nombre del mes, enviando como parametro un numero
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="iMes"></param>
        /// <returns></returns>
        public string F_ObtieneNombreMes(int pi_Mes)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(pi_Mes);
        }
        /// <summary>
        /// Descripción: Obtiene el numero del mes, segun el parametro como mes
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_NombreMes"></param>
        /// <returns></returns>
        public static int f_ObtieneCodigoMes(string ps_NombreMes)
        {
            int iCodMes = 0;
            ps_NombreMes = ps_NombreMes.ToUpper().Trim();
            if (ps_NombreMes == "ENERO")
                iCodMes = 1;
            else if (ps_NombreMes == "FEBRERO")
                iCodMes = 2;
            else if (ps_NombreMes == "MARZO")
                iCodMes = 3;
            else if (ps_NombreMes == "ABRIL")
                iCodMes = 4;
            else if (ps_NombreMes == "MAYO")
                iCodMes = 5;
            else if (ps_NombreMes == "JUNIO")
                iCodMes = 6;
            else if (ps_NombreMes == "JULIO")
                iCodMes = 7;
            else if (ps_NombreMes == "AGOSTO")
                iCodMes = 8;
            else if (ps_NombreMes == "SEPTIEMBRE")
                iCodMes = 9;
            else if (ps_NombreMes == "OCTUBRE")
                iCodMes = 10;
            else if (ps_NombreMes == "NOVIEMBRE")
                iCodMes = 11;
            else if (ps_NombreMes == "DICIEMBRE")
                iCodMes = 12;

            return iCodMes;
        }
        /// <summary>
        /// Descripción: Obtiee el ultimo del del mes enviado como parametro
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="strMes"></param>
        /// <param name="strAnio"></param>
        /// <returns></returns>
        public string f_ObtieneUltimoDiaMes(string strMes, string strAnio)
        {
            return (DateTime.Parse("01" + "/" + strMes + "/" + strAnio).AddMonths(1).AddDays(-1).Day).ToString();
        }
        /// <summary>
        /// Descripción: Carga un dropdpwnlist con años 2015 => 2013,2014,2015,2016
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="cboAnioPeriodo"></param>
        /// <param name="opcionTodos"></param>
        public void f_CargarAnioPeriodo(DropDownList cboAnioPeriodo, bool opcionTodos)
        {
            DataTable dtAnios = new DataTable();
            dtAnios.Columns.Add("anio");
            dtAnios.Rows.Add(dtAnios.NewRow()["anio"] = DateTime.Now.Year - 2);
            dtAnios.Rows.Add(dtAnios.NewRow()["anio"] = DateTime.Now.Year - 1);
            dtAnios.Rows.Add(dtAnios.NewRow()["anio"] = DateTime.Now.Year);
            dtAnios.Rows.Add(dtAnios.NewRow()["anio"] = DateTime.Now.Year + 1);
            m_ListarDropDown(dtAnios, "anio", "anio", cboAnioPeriodo, opcionTodos ? UIConstantes.ConsItemTodosGuion : string.Empty, opcionTodos ? "0" : string.Empty);
        }
        /// <summary>
        /// Descripción: Inserta en el DropDownList los meses 12 
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_AniosTodos"></param>
        /// <param name="pddl_MesPeriodo"></param>
        /// <param name="pb_OpcionTodos"></param>
        public static void cargarMesPeriodo(string ps_AniosTodos, DropDownList pddl_MesPeriodo, bool pb_OpcionTodos)
        {
            if (ps_AniosTodos == "0")
            {
                m_ListarDropDown(null, string.Empty, string.Empty, pddl_MesPeriodo, UIConstantes.ConsItemTodosGuion, "0");
            }
            else
            {
                DataTable mes = new DataTable();
                mes.Columns.Add("idmes");
                mes.Columns.Add("mes");
                CultureInfo ci = new CultureInfo("Es-Pe");
                for (int i = 1; i <= 12; i++)
                {
                    mes.NewRow();
                    mes.Rows.Add(i <= 9 ? ("0" + i.ToString()) : i.ToString(), ci.DateTimeFormat.GetMonthName(i).ToUpper());
                }
                m_ListarDropDown(mes, "idmes", "mes", pddl_MesPeriodo, pb_OpcionTodos ? UIConstantes.ConsItemTodosGuion : string.Empty, pb_OpcionTodos ? "0" : string.Empty);
            }
        }
        /// <summary>
        /// Descripción: Corta el texto o la candena en el tamaño especificado
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 25/06/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_Texto"></param>
        /// <param name="pi_Cant"></param>
        /// <returns></returns>
        public string f_UtilCortarTexto(string ps_Texto, int pi_Cant)
        {
            string sTexto = string.Empty; ;
            if (ps_Texto != null)
            {
                sTexto = ps_Texto.Substring(0, (ps_Texto.Trim().Length > pi_Cant ? pi_Cant : ps_Texto.Trim().Length));
            }
            return sTexto;
        }
        /// <summary>
        /// Descripción: Metodo para escribir la ubicacion actual del menu
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 03/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        public void m_EscribirRutaActual(Label lblObjetoOperacion, string ps_Operacion, Label objObjeto, string ps_Ruta)
        {
            lblObjetoOperacion.Text = ps_Operacion;
            objObjeto.Text = ps_Ruta;
        }
        /// <summary>
        /// Descripción: Metodo para limapir el gridview 
        /// Autor: Jhon Edson Tello Lumbreras / 46153600
        /// Fecha y Hora Creación: 09/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="obj_GridView"></param>
        public void m_LimpiarGridView(GridView obj_GridView)
        {
            obj_GridView.DataSource = new List<Object>();
            obj_GridView.DataBind();
            obj_GridView.EmptyDataText = "Sin datos para mostrar.";
        }
        /// <summary>
        /// Descripción: Funcion para verificar si el parametro es un correo electornico
        /// Autor: Jhon Edson Tello Lumbreras / 46153600
        /// Fecha y Hora Creación: 12/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_Correo"></param>
        public bool f_EsCorreo(string ps_Correo)
        {
            //string sFormatoCorreoElecotrnico = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            //if (Regex.IsMatch(ps_Correo, sFormatoCorreoElecotrnico) == false)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(ps_Correo, sFormato))
            {
                if (Regex.Replace(ps_Correo, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Descripción: Funcion para mantener los datos en memoria de la bitacora
        /// Autor: Jhon Edson Tello Lumbreras / 46153600
        /// Fecha y Hora Creación: 12/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="sUserRed"></param>
        /// <param name="sRuta"></param>
        /// <param name="sRutaCompleta"></param>
        /// <returns></returns>
        public BitacoraBE f_ConfigurarBitacoraPag(string sUserRed, string sRuta, string sRutaCompleta)
        {
            BitacoraBE objBitacora = new BitacoraBE();
            string sRutaSitio = WebConfigurationManager.AppSettings["RutaSitioWeb"];

            string sRutaTMP = sRuta.ToUpper();
            int iPos = 0;
            iPos = sRutaTMP.IndexOf(sRutaSitio);
            if (iPos != -1)
            {
                iPos = iPos + sRutaSitio.Length;
            }
            else
            {
                iPos = 0;
            }
            sRuta = sRuta.Substring(iPos, sRuta.Length - iPos);

            //if (sRutaSitio.Trim()!="")
            //    sRuta = sRuta.Replace(sRutaSitio, "");

            SGA_SP_OBTENER_MENU_X_USUARIO_Result lista = new SeguridadBL().f_ObtenerMenuxUsuarioBL(sUserRed, sRuta);
            if (lista != null)
            {
                objBitacora.intMenu_c_iid = lista.menu_c_iid;
                objBitacora.strBita_c_vrutapagina = sRutaCompleta;
                objBitacora.strColab_c_cusu_red = sUserRed;
                objBitacora.strBita_c_vopcion = "";
                objBitacora.strColab_c_vnomb_completo = lista.NombreCompleto;
            }
            return objBitacora;
        }
        /// <summary>
        /// Descripción: Funcion para realizar las validaciones basicas a los controles textbox
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 17/09/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// sMensaje = objValidar.f_ValidarControles(txtPopTipoDeCambio, "numero", "Tipo de Cambio", 1);
        /// </summary>
        /// <param name="pobj_Control"></param>
        /// <param name="ps_TipoControl"></param>
        /// <param name="ps_NombreControl"></param>
        /// <param name="pi_Indicador"></param>
        /// <returns></returns>
        public string f_ValidarControles(TextBox pobj_Control, string ps_TipoControl, string ps_NombreControl, int pi_Indicador)
        {
            ViewState["sMensajeVacio"] = ViewState["sMensajeVacio"];
            ViewState["sMensajeInvalido"] = ViewState["sMensajeInvalido"];

            string sMensajeTotal = string.Empty;
            //VALIDO SI LOS CAMPOS SON VACIOS
            if (pobj_Control.Text == string.Empty)
            {
                ViewState["sMensajeVacio"] += "  - " + ps_NombreControl + "<br>";
                //if (pi_Indicador == 1 && ViewState["sMensajeVacio"] != null)
                //{
                //    ViewState["sMensajeVacio"] = "1. Los siguientes campos están vacíos : <br>" + ViewState["sMensajeVacio"].ToString() + "<br>";
                //}
            }

            //VALIDO SI LOS CAMPOS TIENE EL FORMATO INVALIDO
            if (ps_TipoControl.ToUpper() == "NUMERO".ToUpper() && pobj_Control.Text != string.Empty)
            {
                double dNumero;
                if (double.TryParse(pobj_Control.Text, out dNumero) == false)
                {
                    ViewState["sMensajeInvalido"] += "  -(Solo números) " + ps_NombreControl + "<br>";
                }
                else
                {
                    if (dNumero < 0)
                    {
                        ViewState["sMensajeInvalido"] += "  -(Número no negativo) " + ps_NombreControl + "<br>";
                    }
                }

            }
            else if (ps_TipoControl.ToUpper() == "Texto".ToUpper() && pobj_Control.Text != string.Empty)
            {
                foreach (var item in pobj_Control.Text)
                {
                    if (char.IsLetter(item) == false && char.IsSeparator(item) == false)
                    {
                        ViewState["sMensajeInvalido"] += "  -(Solo texto) " + ps_NombreControl + "<br>";
                        break;
                    }
                }
            }
            else if (ps_TipoControl.ToUpper() == "Correo".ToUpper() && pobj_Control.Text != string.Empty)
            {
                string sFormatoCorreoElecotrnico = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(pobj_Control.Text, sFormatoCorreoElecotrnico) == false)
                {
                    ViewState["sMensajeInvalido"] += "  -(Correo inválido) " + ps_NombreControl + "<br>";
                }
            }
            else if (ps_TipoControl.ToUpper() == "Fecha".ToUpper() && pobj_Control.Text != string.Empty)
            {
                DateTime dFecha;
                if (DateTime.TryParse(pobj_Control.Text, out dFecha) == false)
                {
                    ViewState["sMensajeInvalido"] += "  -(Fecha inválida) " + ps_NombreControl + "<br>";
                }
            }

            if (pi_Indicador == 1)
            {
                if (ViewState["sMensajeVacio"] != null)
                {
                    ViewState["sMensajeVacio"] = "1. Los siguientes campos están vacíos : <br>" + ViewState["sMensajeVacio"].ToString();
                }
                if (ViewState["sMensajeInvalido"] != null)
                {
                    ViewState["sMensajeInvalido"] = "2. Los siguientes campos tienen formatos inválidos : <br>" + ViewState["sMensajeInvalido"].ToString();
                }
            }

            //UNO LOS MENSAJES PARA SER MOSTRADOS
            if (pi_Indicador == 1)
            {
                if (ViewState["sMensajeVacio"] != null)
                {
                    sMensajeTotal += ViewState["sMensajeVacio"].ToString();
                    ViewState["sMensajeVacio"] = null;
                }
                if (ViewState["sMensajeInvalido"] != null)
                {
                    sMensajeTotal += ViewState["sMensajeInvalido"].ToString();
                    ViewState["sMensajeInvalido"] = null;
                }
            }

            return sMensajeTotal;
        }
        /// <summary>
        /// Descripción: Funcion para convertir un texto en negrita en HTML
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 17/11/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_Texto"></param>
        /// <returns></returns>
        public string f_TextoNegrita(string ps_Texto)
        {
            return "<strong>" + ps_Texto + "</strong>";
        }
        /// <summary>
        /// Descripción: Funcion para contar las filas del excel a cargar
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 18/03/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        int GetLastUsedRow(ExcelWorksheet sheet)
        {
            var row = sheet.Dimension.End.Row;
            while (row >= 1)
            {
                var range = sheet.Cells[row, 1, row, sheet.Dimension.End.Column];
                if (range.Any(c => !string.IsNullOrEmpty(c.Text)))
                {
                    break;
                }
                row--;
            }
            return row;
        }
        /// <summary>
        /// Descripción: Funcion para cargar el excel al grdiview
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 18/03/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="pobjgv"></param>
        /// <param name="opbjfup"></param>
        /// <returns></returns>
        public DataTable CargarExcelGridview(GridView pobjgv, FileUpload opbjfup)
        {
            var tbl = new DataTable();
            try
            {
                if (opbjfup.HasFile && Path.GetExtension(opbjfup.FileName) == ".xlsx")
                {
                    pobjgv.DataSource = null;
                    pobjgv.DataBind();
                    using (var excel = new ExcelPackage(opbjfup.PostedFile.InputStream))
                    {

                        var ws = excel.Workbook.Worksheets.First();
                        var hasHeader = true;
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        {
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : String.Format("Column {0}", firstRowCell.Start.Column));
                        }

                        int startRow = hasHeader ? 2 : 1;
                        int lastRow = GetLastUsedRow(ws);
                        for (int rowNum = startRow; rowNum <= lastRow; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];

                            DataRow row = tbl.NewRow();
                            foreach (var cell in wsRow)
                            {
                                string value = cell.Text.Trim();
                                string field = cell.Address.Substring(0, 1);
                                row[cell.Start.Column - 1] = (string.IsNullOrEmpty(value)) ? null : value;
                            }
                            tbl.Rows.Add(row);
                        }
                        ViewState["dataCarga"] = tbl;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return tbl;
        }
        /// <summary>
        /// Descripción: Funcion para insertar los datos de la bitacora
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 23/03/2015
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="pobjAuditoria"></param>
        /// <returns></returns>
        public string f_InsertarBitacora(int ps_Sistema, int ps_TipoMov, int ps_Menu, string ps_Opcion, string ps_RutaPagina, string ps_ProcExcec, string ps_transac, string ps_IP, string ps_CodUsuario, string ps_NomCompleto)
        {
            string sMensaje = string.Empty;
            AUD_T_BITACORA objBita = new AUD_T_BITACORA();
            AUDDBEntities contexto = new AUDDBEntities();
            try
            {
                objBita.sist_c_iid = ps_Sistema;
                objBita.bita_tip_mov_c_iid = ps_TipoMov;
                objBita.menu_c_iid = ps_Menu;
                objBita.bita_c_vopcion = ps_Opcion;
                objBita.bita_c_vrutapagina = ps_RutaPagina;
                objBita.bita_c_vsqlproceso = ps_ProcExcec;
                objBita.bita_c_vquery_transaccion = ps_transac;
                objBita.bita_c_vnum_ip = ps_IP;
                objBita.colab_c_cusu_red = ps_CodUsuario;
                objBita.colab_c_vnomb_completo = ps_NomCompleto;
                objBita.bita_c_zfec_reg = DateTime.Now;

                contexto.AddToAUD_T_BITACORA(objBita);
                contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                sMensaje = ex.Message.ToString();
            }

            return sMensaje;
        }
        /// <summary>
        /// Descripción: Inserta datos al controls DropDownList
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="pobj_Lst"></param>
        /// <param name="ps_Valor"></param>
        /// <param name="ps_texto"></param>
        /// <param name="ddlControl"></param>
        /// <param name="ps_textoVacio"></param>
        /// <param name="ps_ValorVacio"></param>
        public static void m_ListarDropDown(object pobj_Lst, string ps_Valor, string ps_texto, DropDownList ddlControl, string ps_textoVacio, string ps_ValorVacio)
        {
            ddlControl.Items.Clear();
            ddlControl.DataSource = pobj_Lst;
            ddlControl.DataValueField = ps_Valor;
            ddlControl.DataTextField = ps_texto;
            if (ps_textoVacio.Length != 0 && ps_ValorVacio.Length != 0)
            {
                ddlControl.Items.Add(new ListItem(ps_textoVacio, ps_ValorVacio));
            }
            if (pobj_Lst != null)
            {
                ddlControl.AppendDataBoundItems = true;
                ddlControl.DataBind();
                ddlControl.AppendDataBoundItems = false;
            }
        }
        /// <summary>
        /// Descripción: Funcion que recibe un datatable y exporta a excel
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 28/03/2016
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="pobj_Trabajador"></param>
        /// <param name="ps_Nombre"></param>
        public void m_CrearExcel(DataTable pobj_dt, string ps_Nombre)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(pobj_dt);

                Response.Clear();
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
        /// <summary>
        /// Descripción: Funcion que recibe una Lista y retorna un datatable
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 28/03/2016
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pl_items"></param>
        /// <param name="ps_NombreWork"></param>
        /// <returns></returns>
        public DataTable f_ListDatatable<T>(List<T> pl_items, string ps_NombreWork)
        {
            DataTable dtConvert = new DataTable(ps_NombreWork);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dtConvert.Columns.Add(prop.Name);
            }
            foreach (T item in pl_items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dtConvert.Rows.Add(values);
            }
            return dtConvert;
        }

        /// <summary>
        /// Descripción: Funcion para centrar cualqueir texto enviado como parametro
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 04/04/2016
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="sMensaje"></param>
        /// <returns></returns>
        public string f_CentrarElemento(string sMensaje)
        {
            string sTexto = string.Empty;

            sTexto = "<div style=\"text-align: center;\">" + sMensaje + "</div>";
            return sTexto;
        }
        /// <summary>
        /// Descripción: Funcion para descargar documentos desde alguna ruta compartida en un servidor local
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: Fecha
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_Ruta"></param>
        /// <param name="ps_NombreDoc"></param>
        /// <param name="ps_Extension"></param>
        public void f_DescargarDocumentoDesdeRuta(string ps_NombreDoc, string ps_Extension)
        {
            string file = WebConfigurationManager.AppSettings["RutaSustento"].ToString(); ;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ps_NombreDoc + ps_Extension);
            Response.Flush();
            Response.TransmitFile(MapPath(file) + ps_NombreDoc + ps_Extension);
            Response.End();
        }
        /// <summary>
        /// Descripción: Funcion para enviar correos
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 06/04/2016
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="_CorreoOrigen"></param>
        /// <param name="_clave"></param>
        /// <param name="_correoDestino"></param>
        /// <param name="_asunto"></param>
        /// <param name="_nombre"></param>
        /// <param name="_body"></param>
        /// <param name="_smtp"></param>
        /// <param name="_puerto"></param>
        /// <returns></returns>
        public bool f_EnviarCorreo(string _correoDestino, string _asunto, string _nombre, string _body)
        {
            string sCorreoOrigen = WebConfigurationManager.AppSettings["pCorreoOrigen"];
            string sContrasena = WebConfigurationManager.AppSettings["pContraseña"];
            string sSmtp = WebConfigurationManager.AppSettings["pSmtp"];
            int iPuerto = Convert.ToInt32(WebConfigurationManager.AppSettings["pPuerto"]);

            MailMessage _correo = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            _correo.From = new MailAddress(sCorreoOrigen, _nombre);
            _correo.To.Add(_correoDestino);
            //_correo.CC.Add();
            _correo.Subject = _asunto;
            _correo.Body = _body;
            _correo.IsBodyHtml = true;
            _correo.Priority = MailPriority.Normal;

            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(sCorreoOrigen, sContrasena);
            smtp.Host = sSmtp;
            smtp.Port = iPuerto;

            try
            {
                smtp.Send(_correo);
                _correo.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        /// Descripción: carga archivos por medio de FTP
        /// Autor: Jair Tasayco Bautista / RP0689
        /// Fecha y Hora Creación: 18/04/2016
        /// Modificado: --
        /// Fecha y hora Modificación: --
        /// </summary>
        /// <param name="ps_RutaDestino"></param>
        /// <param name="ps_NombreArchivoDestino"></param>
        /// <param name="pobj_file"></param>
        public void f_SubirArchivoFTP(string ps_NuevoNombre, FileUpload pobj_file)
        {
            //Sube archivos siempre y cuando se tenga acceso de una carpeta a otra en el servidor web
            //Sube archivos si es que se sube desde una aplicacion alojada localmente a una carpeta en uns servidor web externo
            try
            {
                string ftp = WebConfigurationManager.AppSettings["fLoadRuta"].ToString();
                string sRutaSustento = WebConfigurationManager.AppSettings["RutaSustento"].ToString();
                string sUsuario = WebConfigurationManager.AppSettings["fLoadUser"];
                string sContrasena = WebConfigurationManager.AppSettings["fLoadPass"];

                System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(ftp + sRutaSustento + ps_NuevoNombre);
                clsRequest.Credentials = new System.Net.NetworkCredential(sUsuario, sContrasena);
                clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                byte[] bFile = pobj_file.FileBytes;
                System.IO.Stream clsStream = clsRequest.GetRequestStream();
                clsStream.Write(bFile, 0, bFile.Length);
                clsStream.Close();
                clsStream.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Descripción: Funcion apra descargar archivos desde ftp
        /// Autor: Jhon Edson Tello Lumbreras / RPEXT038
        /// Fecha y Hora Creación: 18/04/2016
        /// Modificado: Modificado
        /// Fecha y hora Modificación: FechaModificacion
        /// </summary>
        /// <param name="ps_RutaDestino"></param>
        /// <param name="ps_ArchivoDestino"></param>
        /// <param name="ps_ExtensionArchivoDestino"></param>
        public void f_DescargarArchivoFTP(string ps_ArchivoDestino, string ps_ExtensionArchivoDestino)
        {
            string ftp = WebConfigurationManager.AppSettings["fLoadRuta"].ToString();
            string sRutaSustento = WebConfigurationManager.AppSettings["RutaSustento"].ToString();
            string sUsuario = WebConfigurationManager.AppSettings["fLoadUser"];
            string sContrasena = WebConfigurationManager.AppSettings["fLoadPass"];
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + sRutaSustento + ps_ArchivoDestino + ps_ExtensionArchivoDestino);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(sUsuario, sContrasena);
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using (MemoryStream stream = new MemoryStream())
                {
                    response.GetResponseStream().CopyTo(stream);
                    Response.AddHeader("content-disposition", "attachment;filename=" + ps_ArchivoDestino + ps_ExtensionArchivoDestino);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
        }
        public bool m_validarAcceso(string ps_cuentaLoguin, int pi_sistema)
        {
            bool resul = false;

            //string sCuentaLoguin = objUsuario.usua_c_cdoc_id;
            //int iSistema = Convert.ToInt32(IEnum.Sistema.Proveedores);
            SeguridadBL objSeguridadBL = new SeguridadBL();


            var objListaPadres = objSeguridadBL.ObtenerMenuPadres(ps_cuentaLoguin, pi_sistema);
            var sb = new StringBuilder();

            var lstMenues = new[] { new { rutaAspx = "" } }.ToList();

            lstMenues.Add(new { rutaAspx = "/Interfaces/Default/Default.aspx" });
            lstMenues.Add(new { rutaAspx = "/Interfaces/Inicio/frmInicio.aspx" });
            lstMenues.Add(new { rutaAspx = "/Interfaces/Restringido/frm404.aspx" });
            lstMenues.Add(new { rutaAspx = "/Interfaces/Restringido/frmErrGeneral.aspx" });
            //lstMenues.Add(new { rutaAspx = "/Interfaces/Home/Inicio.aspx" });
            lstMenues.Add(new { rutaAspx = "/Interfaces/Restringido/frmAccesoRestringido.aspx" });
            foreach (var padre in objListaPadres)
            {
                if (padre.menu_c_vpag_asp != null)
                {
                    lstMenues.Add(new { rutaAspx = padre.menu_c_vpag_asp });
                }
                var objListaHijos = objSeguridadBL.ObtenerMenuHijos(ps_cuentaLoguin, padre.menu_c_iid, pi_sistema);
                foreach (var hijo in objListaHijos)
                {
                    lstMenues.Add(new { rutaAspx = hijo.menu_c_vpag_asp });
                }
            }
            //string sRutaSitio = "";// WebConfigurationManager.AppSettings["RutaSitioWeb"];
            string strUrl = string.Empty;
            //if (sRutaSitio.Trim() != "")
            //{
            //    strUrl = HttpContext.Current.Request.Url.AbsolutePath.Replace(sRutaSitio, "");
            //}
            strUrl = HttpContext.Current.Request.Url.AbsolutePath;//.Replace(sRutaSitio, "");
            var resultado = from menu in lstMenues
                            where (menu.rutaAspx.Contains(strUrl))
                            select menu;

            if (resultado.Count() > 0)
            {
                resul = true;
            }
            else
            {
                resul = false;
            }
            return resul;
        }

        #region METODOS PARA LOGS 
        /// <summary>
        /// Descripción: Escribe en el log el detalle de los errores que ocurren en el sistema
        /// Autor: Hector Vives (ETECH)
        /// Fecha y Hora Creación: 08-09-2016
        /// </summary>
        /// <param name="ps_Aplicacion"></param>
        /// <param name="ps_UsuRed"></param>
        /// <param name="ps_Pc"></param>
        /// <param name="ex"></param>
        public static void m_TraceError(string ps_Aplicacion, string ps_UsuRed, string ps_Pc, Exception ex)
        {
            try
            {
                string sRutaLog = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["rutaLog"]);
                string sNombreLog = ConfigurationManager.AppSettings["nombreLog"];
                string fechaHoy = DateTime.Now.ToString("yyyy-MM-dd");

                sRutaLog = sRutaLog + sNombreLog + "-" + fechaHoy + ".log";

                //set up a filestream
                FileStream fs = new FileStream(sRutaLog, FileMode.OpenOrCreate, FileAccess.Write);

                //set up a streamwriter for adding text
                StreamWriter sw = new StreamWriter(fs);

                //find the end of the underlying filestream
                sw.BaseStream.Seek(0, SeekOrigin.End);

                //add the text
                sw.WriteLine(f_GenerarCuerpoLog(ps_Aplicacion, ps_UsuRed, ps_Pc, ex));

                sw.Flush();

                //close the writer
                sw.Close();
            }
            catch (Exception)
            {


            }
        }

        /// <summary>
        /// Descripción: Genera una cadena que contiene el detalle del error
        /// Autor: Hector Vives (ETECH)
        /// Fecha y Hora Creación: 08-09-2016
        /// </summary>
        /// <param name="ps_Aplicacion"></param>
        /// <param name="ps_UsuRed"></param>
        /// <param name="ps_Pc"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string f_GenerarCuerpoLog(string ps_Aplicacion, string ps_UsuRed, string ps_Pc, Exception ex)
        {
            StringBuilder sbCuerpo = new StringBuilder();
            sbCuerpo.Append("FechaHora: " + DateTime.Now + " \n");
            sbCuerpo.Append("Sistema: " + ps_Aplicacion + " \n");
            sbCuerpo.Append("Usuario: " + ps_UsuRed + " \n");
            sbCuerpo.Append("NombrePC: " + ps_Pc + " \n");
            if (ex != null)
            {
                sbCuerpo.Append("MensajeError: " + ex.Message + " \n");
                sbCuerpo.Append("DetalleError: " + ex.StackTrace + " \n");
            }
            sbCuerpo.Append("------------------------------------------------------------------------------------------------------");
            sbCuerpo.Append("------------------------------------------------------------------------------------------------------");
            return sbCuerpo.ToString();
        }
        #endregion

    }
}
