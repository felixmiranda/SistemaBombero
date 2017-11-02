<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal/Site.Master"
    AutoEventWireup="true" CodeBehind="frmReserveSpaceAdvertising.aspx.cs" Inherits="BOM.UserLayer.Interfaces.Reserve.frmReserveSpaceAdvertising" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../ControlUsuario/CUCargando.ascx" TagName="CUCargando" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/styles/controls.css?20170222" rel="stylesheet" type="text/css" />
    <%--    <link href="../../Styles/styles/acordion.css?20170222" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" language="javascript">
        function f_Ready_Fuction() {

            $('#divFiltro').show('slow');
            $('#resultadoConsulta').hide();
            $('#divPaso2').hide(); //RESERVA
            $('#divAsociarcliente').hide();

            //ACORDION--------------
            $('.collapse.in').prev('.panel-heading').addClass('active');
            $('#accordion, #bs-collapse')
                .on('show.bs.collapse', function (a) {
                    $(a.target).prev('.panel-heading').addClass('active');
                })
                .on('hide.bs.collapse', function (a) {
                    $(a.target).prev('.panel-heading').removeClass('active');
                });
            $('.blockkey_movil').attr('readonly', 'readonly');
        }
        function NoJavaScript(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 13) {
                return false;
            } else if (charCode === 62) {
                return false;
            }

            return true;
        }
        function f_Mostrar() {
            $('#divFiltro').hide();
            $('#resultadoConsulta').show('slow');
            $('#divPaso2').hide(); //ACORDION
            $('#divAsociarcliente').hide();
        }
        function f_RegresaConsulta() {
            $('#resultadoConsulta').hide();
            $('#divFiltro').show('slow');
        }
        function f_RegresaPaso2() {
            $('#divAsociarcliente').hide();
            $('#divPaso2').show('slow');
        }

        function f_RegresarResultadoBusqueda() {
            $('#divPaso2').hide();
            $('#resultadoConsulta').show('slow');
        }
        function f_AsociarCliente() {
            $('#divPaso2').hide();
            $('#divAsociarcliente').show('slow');
            return true;
        }

        function f_mostrarPaso2() {
            $('#resultadoConsulta').hide();
            $('#divPaso2').show('slow');
        }
        function f_AbrirPop() {
            //$("#popupinfo").modal();
            $("#popupinfo").modal({
                "backdrop": true,
                "keyboard": true,
                "show": true
            });
        }
        function f_AbrirPopAccion() {
            //$("#popupinfo").modal();
            $("#popmsjAccion").modal({
                "backdrop": true,
                "keyboard": true,
                "show": true
            });
        }
        function f_AbrirPopOcupacion() {
            //            $("#popOcupacion").modal();
            $("#popOcupacion").modal({
                "backdrop": true,
                "keyboard": true,
                "show": true
            });
        }


        $(document).ready(function (texto) {
            //$("#micapa").html("Nuevo contenido de la capa");
            $('#texto').children(1).html = texto;
            $("body").addClass("modal-open");
        });

        function BeginRequestHandler(sender, args) {
            $('#CUMensajeCargando').modal('show');
        }
        function EndRequestHandler(sender, args) {
            $('#CUMensajeCargando').modal('hide');
            //$("body").addClass("modal-open");
        }

    </script>
    <style type="text/css">
        .mCalendar
        {
            width: 100%;
            background-color: #ffffff !important;
            margin: 5px 0 10px 0;
            border: solid 1px #e9daea;
            border-collapse: collapse;
        }
        .mCalendar td
        {
            font-size: 12px !important;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .mCalendar th
        {
            padding: 4px 3px;
            color: #fff;
            background: #237b96 !important;
            border-left: solid 1px #237b96;
            font-size: 11.5px;
            text-align: center;
        }
        
        .mCalendar .alt
        {
            background: #f5eff6 !important;
        }
        .mCalendar .pgr
        {
            background: #237b96 !important;
        }
        .mCalendar .pgr table
        {
            margin: 0px 0 !important;
        }
        .mCalendar .pgr td
        {
            border-width: 0;
            padding: 0 0px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .mCalendar .pgr a
        {
            color: #c487c7;
            text-decoration: none;
        }
        .mCalendar .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        
        .csCalendarEstilo
        {
            font-weight: bold;
            color: Black;
            font-size: 12px;
            text-transform: uppercase;
            background-color: White;
            border-radius: 15px;
        }
        .csOtroDia
        {
            opacity: 0.00;
        }
        .darborde
        {
            border: solid 1px;
        }
        .scrollingControlContainer
        {
            overflow-x: hidden;
            overflow-y: scroll;
        }
        .HeaderOcupacion
        {
            text-align: center;
        }
        
        .scrollingCheckBoxList
        {
            margin: 10px 10px 10px 10px;
            margin-top: 10px;
            height: 200px;
        }
        .scrolling-table-container
        {
            height: 378px;
            overflow-y: scroll;
            overflow-x: hidden;
        }
        
        tr td label
        {
            width: 90%;
            font-size: 12;
        }
        tr td input
        {
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>

        //On Page Load.
        init.push(function () {
            SetDatePicker();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
                }
            });
        };
        function SetDatePicker() {
            var options = {
                format: 'dd/mm/yyyy'
            }
            $('.mydatepickerclass').datepicker(options);
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    </script>
    <div class="page-header">
        <h1>
            <span class="text-light-gray">PUBLICIDAD / </span>Reserva de espacio publicitario</h1>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel form-horizontal">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="panel-heading">
                            <span class="panel-title">
                                <asp:Label ID="lblPaso1" runat="server" Text="Label"></asp:Label>
                                <label>
                                    ></label>
                                <asp:Label class="subtitulo1" ID="lblPaso2" runat="server" Text="Label"></asp:Label>
                                <label class="subtitulo1">
                                    ></label>
                                <asp:Label class="subtitulo1" ID="lblPaso3" runat="server" Text="Label"></asp:Label>
                            </span>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRegresa" />
                        <asp:AsyncPostBackTrigger ControlID="btnIrReserva" />
                        <asp:AsyncPostBackTrigger ControlID="btnAsociarCliente" />
                        <asp:AsyncPostBackTrigger ControlID="btnRegresarPaso2" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="panel-body">
                    <div id="divFiltro">
                        <div class="row">
                            <div class="col-sm-4">
                                <label>
                                    Producto <span class="requerido">(*)</span></label>
                                <asp:DropDownList class="dropdown btn btn-default dropdown-toggle" Style="margin: 10px;"
                                    ID="ddlProductoFiltro" runat="server" OnSelectedIndexChanged="ddlProductoFiltro_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4" style="text-align: right;padding-top: 26px;padding-right: 15px;">
                                <asp:CheckBox ID="chkSoloDisponible" runat="server" Text="Solo disponibles"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <fieldset class="fieldset">
                                    <legend class="txt">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                            <ContentTemplate>
                                                <asp:Label ID="lblActivacionElemento" runat="server" Text="Elemento"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlProductoFiltro" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </legend>
                                    <asp:UpdatePanel runat="server" ID="upListElemento">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                <asp:CheckBoxList ID="checkboxListElemento" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                                                    Style="width: 100%">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlProductoFiltro" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <%--      </div>--%>
                                </fieldset>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <fieldset class="fieldset">
                                    <legend class="txt">Inmueble <span class="requerido">(*)</span></legend>
                                    <asp:Panel ID="Panel2" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                        <asp:UpdatePanel runat="server" ID="upListInmueble" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DataList ID="dtlInmueble" runat="server" RepeatColumns="2" Style="width: 100%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkInmueble" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inm_c_vnomb") %>'
                                                            AutoPostBack="true" OnCheckedChanged="chkInmueble_CheckedChanged" />
                                                        <asp:Label ID="lblInmueble" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "inm_c_icod") %>'
                                                            Style="display: none" />
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <%--<asp:CheckBoxList ID="checkboxListInmueble" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                                            Style="width: 100%" CellSpacing="1" CellPadding="5" TextAlign="Right">
                                        </asp:CheckBoxList>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </fieldset>
                            </div>
                        </div>
                        <script>
                            init.push(function () {
                                var options = {
                                    format: 'dd/mm/yyyy'
                                }
                                $('#<%=txtfechaDesdeFiltro.ClientID%>').datepicker(options);
                                $('#<%=txtfechaHastaFiltro.ClientID%>').datepicker(options);


                            });
                        </script>
                        <div class="row" style="padding-top: 15px">
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-6 col-md-6">
                                        <fieldset class="fieldset">
                                            <legend class="txt">Fecha <span class="requerido">(*)</span></legend>
                                            <div class="col-xs-12 col-sm-6 col-md-6">
                                                <asp:TextBox ID="txtfechaDesdeFiltro" runat="server" CssClass="form-control blockkey_movil"
                                                    Placeholder="Desde" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-xs-12 col-sm-6 col-md-6">
                                                <asp:TextBox ID="txtfechaHastaFiltro" runat="server" CssClass="form-control blockkey_movil"
                                                    Placeholder="Hasta" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-xs-12 col-sm-6 col-md-6">
                                        <fieldset class="fieldset">
                                            <legend class="txt">Área</legend>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                <ContentTemplate>
                                                    <div class="col-xs-12 col-sm-6 col-md-6">
                                                        <asp:TextBox ID="txtMedidaDesdeFiltro" class="form-control" runat="server" Placeholder="Desde"
                                                            onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtMedidaDesdeFiltro" runat="server"
                                                            Enabled="True" TargetControlID="txtMedidaDesdeFiltro" ValidChars="0123456789">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-6 col-md-6">
                                                        <asp:TextBox ID="txtMedidaHastaFiltro" class="form-control" runat="server" Placeholder="Hasta"
                                                            onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtMedidaHastaFiltro" runat="server"
                                                            Enabled="True" TargetControlID="txtMedidaHastaFiltro" ValidChars="0123456789">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlProductoFiltro" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6" style="padding-top: 10px !important">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <label class="control-label">
                                            Razón Social del Cliente:</label>
                                        <asp:TextBox ID="txtRazonSocialFiltro" MaxLength="200" class="form-control" runat="server"
                                            onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                            Enabled="True" ServicePath="" ServiceMethod="razonsocialClienteAutocompletado"
                                            TargetControlID="txtRazonSocialFiltro" UseContextKey="True" FirstRowSelected="True"
                                            MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" CompletionInterval="100">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 form-group">
                                <div style="float: right; clear: both;">
                                    <asp:Button ID="btnBuscarPaso1" class="btn btn-primary" runat="server" Text="BUSCAR >"
                                        OnClick="btnBuscarPaso1_Click" Style="width: 200px;"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="resultadoConsulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row" style="overflow: auto;">
                                    <asp:GridView ID="gvReservas" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="tbl_cabecerareal" EmptyDataText="No se han encontrado reservas en la base de datos."
                                        DataKeyNames="reser_c_iid,pub_esp_c_iid,pub_elem_act_c_iid,inm_c_icod,pub_prod_c_iid,inm_c_vnomb,pub_prod_c_vnomb,pub_elem_act_c_vnomb,pub_esp_c_vcod,pub_esp_c_emonto_tarifa_base,pub_esp_c_emonto_tarifa_top,pub_esp_c_earea,pub_esp_c_vmedida"
                                        OnRowCommand="gvReservas_RowCommand" OnRowDataBound="gvReservas_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="idEspacio" ItemStyle-CssClass="row_acciones" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("pub_esp_c_iid") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="idelemento" ItemStyle-CssClass="row_acciones" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label50" runat="server" Text='<%# Bind("pub_elem_act_c_iid") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="checkEspacio" runat="server" onkeypress="return NoJavaScript(event)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="INMUEBLE" ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("inm_c_vnomb") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PRODUCTO" ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnomProductogv" runat="server" Text='<%# Bind("pub_prod_c_vnomb") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ELEMENTO/TIPO ACT." ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblelemgv" runat="server" Text='<%# Bind("pub_elem_act_c_vnomb") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COD. ESPACIO" ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    <table align="center" border="0">
                                                        <tr>
                                                            <td style="border-style: none">
                                                                <asp:LinkButton ID="lkCodigoEspacio" Style="text-decoration: underline;" runat="server"
                                                                    CommandName="verInfoEspacio" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    Text='<%# Bind("pub_esp_c_vcod") %>'></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DETALLE OCUPACIÓN" ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    <table align="center" border="0">
                                                        <tr>
                                                            <td style="border-style: none">
                                                                <asp:LinkButton ID="lkDisponibilidad" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    CommandName="verInfoDisponibilidad" Style="text-decoration: underline;">ver disponibilidad</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COSTO DE PRODUCCIÓN" ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    S/
                                                    <asp:Label ID="lblCostoProducConsulta" runat="server" Text='<%# Bind("pub_esp_c_emonto_tarifa_base") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TARIFA FRÍA" ItemStyle-CssClass="row_acciones">
                                                <ItemTemplate>
                                                    S/
                                                    <asp:Label ID="lblCostoTarifaFriaConsulta" runat="server" Text='<%# Bind("pub_esp_c_emonto_tarifa_top") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="row_acciones" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tbl_cabecerareal" />
                                    </asp:GridView>
                                </div>
                                <div style="float: left;">
                                    <asp:Button ID="btnRegresa" class="btn btn-primary" runat="server" Text="< BUSCAR"
                                        OnClick="btnRegresa_Click" Style="width: 200px;"></asp:Button>
                                </div>
                                <div style="float: right;">
                                    <asp:Button ID="btnIrReserva" class="btn btn-primary" runat="server" Text="IR A RESERVA >"
                                        OnClick="btnIrReserva_Click" Style="width: 200px;"></asp:Button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBuscarPaso1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div id="divPaso2">
                        <asp:UpdatePanel runat="server" ID="upReservas" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <div class="col-xs-12 col-sm-6 col-md-6">
                                            <div style="float: left; width: 100px; line-height: 27px">
                                                Ejecutivo <span class="requerido">(*)</span>
                                            </div>
                                            <div style="float: left">
                                                <asp:DropDownList class="dropdown btn btn-default dropdown-toggle" CssClass="form-control"
                                                    ID="ddlEjecutivoReserva" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-6">
                                            <div style="float: right;">
                                                <label class="control-label">
                                                    Fecha de vencimiento de la reserva:
                                                </label>
                                                &nbsp;
                                                <label class="control-label" id="lblfechavencimientoReserva" runat="server">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Agregar codigo en esta parte FMR--%>
                                 <div class="row" style="padding-top: 15px">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                            <asp:CheckBox ID="chkAplicarPeriodoTodos" runat="server" 
                                                Text="Aplicar Periodo a todos los espacios" style="text-align: left !important" 
                                                AutoPostBack="True" oncheckedchanged="chkAplicarPeriodoTodos_CheckedChanged" />
                                            <%--<label class="control-label" style="text-align: left !important">
                                                Periodo</label>--%>
                                            <br />
                                        </div>
                                        <div class="col-xs-12 col-sm-5 col-md-5" style="padding-top: 15px">
                                            <asp:RadioButton ID="rbContinuoReservaPrincipal" runat="server" Width="130px" Text="Continuo"
                                                OnCheckedChanged="rbContinuoReservaPrincipal_CheckedChanged" 
                                                AutoPostBack="true" Checked="True" Enabled="False" 
                                                GroupName="PeriodoPrincipal" />
                                            <div style="clear: both;">
                                                <div style="width: 50%; float: left; text-align: left;">
                                                    <asp:TextBox ID="txtfechaDesdeReservaPrincipal" runat="server" CssClass="form-control mydatepickerclass"
                                                        Placeholder="Desde" onkeypress="return NoJavaScript(event)" 
                                                        Enabled="False"></asp:TextBox>
                                                </div>
                                                <div style="width: 50%; float: left; text-align: left; padding-left: 15px">
                                                    <asp:TextBox ID="txtfechaHastaReservaPrincipal" runat="server" CssClass="form-control mydatepickerclass"
                                                        Placeholder="Hasta" onkeypress="return NoJavaScript(event)" 
                                                        Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-7 col-md-7" style="padding-top: 15px">
                                            <asp:RadioButton ID="rbIntermitenteReservaPrincipal" runat="server" 
                                                Width="130px" Text="Intermitente"
                                                OnCheckedChanged="rbIntermitenteReservaPrincipal_CheckedChanged" 
                                                AutoPostBack="true" Visible="True" Enabled="False" 
                                                GroupName="PeriodoPrincipal" />
                                            <div style="clear: both;">
                                                <div style="width: 50%; float: left; text-align: left; padding-left: 15px;">
                                                    <asp:Calendar ID="calendarFechasReservaPrincipal" runat="server" 
                                                        CssClass="mCalendar" OtherMonthDayStyle-CssClass="csOtroDia"
                                                            TitleStyle-CssClass="csCalendarEstilo" OnSelectionChanged="calendarFechasReservaPrincipal_SelectionChanged"
                                                        Visible="False" ></asp:Calendar>
                                                </div>
                                                <div style="width: 50%; float: left; text-align: left; padding-left: 15px; overflow: auto;">
                                                    <asp:GridView ID="gvFechasReservaPrincipal" runat="server" AutoGenerateColumns="false" DataKeyNames="Date"
                                                        CellSpacing="2" CellPadding="4" Width="200px" GridLines="None" BorderWidth="1px"
                                                        ShowHeader="False" OnRowCommand="gvFechasPrincipal_RowCommand" Visible="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="Date" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtnEliminarPrincipal" runat="server" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                        ToolTip="Eliminar">X</asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Bold="true" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div> 
                                    </div>  
                                </div>
                                <%--FIN DE CODIGO FMR--%>
                                <br />
                                <div class="row">
                                    <asp:DataList ID="dtlistEspacios" runat="server" Style="width: 100%" OnItemDataBound="dtlistEspacios_ItemDataBound">
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="col-xs-12 col-sm-12 col-md-12">
                                                        <asp:Panel ID="pnl_cabecera" runat="server" Style="cursor: pointer">
                                                            <div class="panel-heading" style="background: #237b96; color: white">
                                                                <asp:Image ID="img_mas" runat="server" ImageUrl="~/Images/collapsed.png" />
                                                                <%--                                                                        <asp:Label runat="server" ID="lblidreser_mast_iid" Style="width: 0px; height: 0px;
                                                                            visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "i_reser_mast_c_iid")%>'></asp:Label>--%>
                                                                <asp:Label runat="server" ID="lblidreser_iid" Style="width: 0px; height: 0px; visibility: collapse;
                                                                    float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "i_reser_c_iid")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblidespacioReserva" Style="width: 0px; height: 0px;
                                                                    visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "i_id_espacio")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblidinmuebleReserva" Style="width: 0px; height: 0px;
                                                                    visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "i_id_inmueble")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblidproductoReserva" Style="width: 0px; height: 0px;
                                                                    visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "i_id_producto")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblidelementoReserva" Style="width: 0px; height: 0px;
                                                                    visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "i_id_elemento_activacion")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblcodEspacioReserva" Style="width: 0px; height: 0px;
                                                                    visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "s_cod_espacio")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblnombTabReserva" Style="width: 0px; height: 0px;
                                                                    visibility: collapse; float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "s_cod_espacio")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblareaReserva" Style="width: 0px; height: 0px; visibility: collapse;
                                                                    float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "s_esp_earea")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblmedidaReserva" Style="width: 0px; height: 0px; visibility: collapse;
                                                                    float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "s_esp_vmedida")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblfriaReserva" Style="width: 0px; height: 0px; visibility: collapse;
                                                                    float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "s_tarifa_fria")%>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblbaseReserva" Style="width: 0px; height: 0px; visibility: collapse;
                                                                    float: left;" Text='<%#DataBinder.Eval(Container.DataItem, "s_tarifa_base")%>'></asp:Label>
                                                                <%# DataBinder.Eval(Container.DataItem, "s_nombre_tab")%>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_detalle" runat="server" Style="border: 1px solid #237b96; margin-left: 0px;
                                                            margin-right: 0px; margin-bottom: 10px; height: auto !important; overflow: hidden">
                                                            <div>
                                                                <div class="panel-body" style="padding-left: 0px !important; padding-right: 0px !important">
                                                                    <div class="row">
                                                                        <div class="col-xs-12 col-sm-12 col-md-12" style="height: auto;">
                                                                            <div class="col-xs-12 col-sm-7 col-md-4">
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 50%; float: left; text-align: left !important;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Producto</label>
                                                                                        <asp:Label runat="server" ID="lblProductoReserva" class="control-label" Style="width: 100%;"
                                                                                            Text='<%#DataBinder.Eval(Container.DataItem, "s_nombre_producto")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div style="width: 50%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            <%#DataBinder.Eval(Container.DataItem, "s_texto_tipo_elemento")%>
                                                                                            <span class="requerido">(*)</span></label>
                                                                                        <br />
                                                                                        <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>--%>
                                                                                        <asp:DropDownList class="dropdown btn btn-default dropdown-toggle" CssClass="form-control"
                                                                                            Style="width: 100%;" ID="ddlElementoReserva" runat="server" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="ddlElementoReserva_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <%-- </ContentTemplate>
                                                                                        </asp:UpdatePanel>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-5 col-md-4">
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 100%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Descripción de activación o elemento </label>
                                                                                        <br />
                                                                                        <asp:TextBox ID="txtDescActivacionReserva" MaxLength="500" CssClass="form-control"
                                                                                            Style="width: 100%; resize: none" runat="server" TextMode="MultiLine" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-12 col-md-4">
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 50%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Tarifa fría</label>
                                                                                        <br />
                                                                                        <label class="control-label" id="lblTarifaFriaReserva" style="width: 100%; text-align: left !important"
                                                                                            runat="server">
                                                                                            S/&nbsp;<%# DataBinder.Eval(Container.DataItem, "s_tarifa_fria")%></label></div>
                                                                                    <div style="width: 50%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Costo de producción</label>
                                                                                        <br />
                                                                                        <label class="control-label" id="lblCostoProduccReserva" style="width: 100%; text-align: left !important"
                                                                                            runat="server">
                                                                                            S/&nbsp;<%# DataBinder.Eval(Container.DataItem, "s_tarifa_base")%></label></div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 15px">
                                                                        <div class="col-xs-12 col-sm-12 col-md-12" style="height: auto;">
                                                                            <div class="col-xs-12 col-sm-7 col-md-4">
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 50%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Tipo de Asignación </label>
                                                                                        <br />
                                                                                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>--%>
                                                                                        <asp:DropDownList class="dropdown btn btn-default dropdown-toggle" CssClass="form-control"
                                                                                            ID="ddlTipoAsignacion" Style="width: 90%;" runat="server">
                                                                                        </asp:DropDownList>
                                                                                        <%-- </ContentTemplate>
                                                                                        </asp:UpdatePanel>--%>
                                                                                    </div>
                                                                                    <div style="width: 50%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Dimensión final del espacio <span class="requerido">
                                                                                                <asp:Label id="lblRequeridoDimensionReserva" runat="server" /> </span></label>
                                                                                        <br />
                                                                                        <asp:TextBox ID="txtDimensionReserva" CssClass="form-control" Style="width: 100%;"
                                                                                            runat="server" MaxLength="500" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-5 col-md-4">
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 100%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Comentarios de la asignación</label>
                                                                                        <br />
                                                                                        <asp:TextBox ID="txtComentarioReserva" CssClass="form-control" Style="width: 100%;
                                                                                            resize: none" runat="server" MaxLength="500" TextMode="MultiLine" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-12 col-md-4">
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 70%; float: left; text-align: left;">
                                                                                        <label class="control-label" style="text-align: left !important">
                                                                                            Precio final del alquiler S/</label>
                                                                                        <br />
                                                                                        <asp:TextBox ID="txtPrecioFinReserva" class="form-control" MaxLength="19" runat="server"
                                                                                            Style="width: 100%;" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtPrecioFinReserva" runat="server"
                                                                                            Enabled="True" TargetControlID="txtPrecioFinReserva" ValidChars=".0123456789">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 15px">
                                                                        <div class="col-xs-12 col-sm-12 col-md-12" style="height: auto;">
                                                                            <div class="col-xs-12 col-sm-6 col-md-6">
                                                                                <div style="clear: both;">
                                                                                    <label class="control-label" id="lblinfoentregaProducto" style="width: 100%; text-align: left !important;
                                                                                        padding-bottom: 15px" runat="server">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "s_requisito_producto_entrega")%></label>
                                                                                    <%--  <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>--%>
                                                                                    <asp:Label runat="server" ID="lblDetalleEntrega" class="control-label" Style="width: 100%;
                                                                                        text-align: left" Text=""></asp:Label>
                                                                                    <%-- </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="ddlElementoReserva" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-6 col-md-6">
                                                                                <div style="clear: both;">
                                                                                    <label class="control-label" id="lblinfoaperturaProducto" style="width: 100%; text-align: left !important;
                                                                                        padding-bottom: 15px" runat="server">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "s_requisito_producto_apertura")%></label>
                                                                                    <%--<asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>--%>
                                                                                    <asp:Label runat="server" ID="lblDetalleApertura" class="control-label" Style="width: 100%;
                                                                                        text-align: left" Text=""></asp:Label>
                                                                                    <%-- </ContentTemplate>
                                                                                         <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="ddlElementoReserva" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 15px">
                                                                        <div class="col-xs-12 col-sm-12 col-md-12">
                                                                            <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                <ContentTemplate>--%>
                                                                            <div class="col-xs-12 col-sm-12 col-md-12">
                                                                                <label class="control-label" style="text-align: left !important">
                                                                                    Periodo <span class="requerido">(*)</span></label>
                                                                                <br />
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-5 col-md-5" style="padding-top: 15px">
                                                                                <asp:RadioButton ID="rbContinuoReserva" runat="server" Width="130px" Text="Continuo"
                                                                                    OnCheckedChanged="rbContinuoReserva_CheckedChanged" AutoPostBack="true" Checked="True" />
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 50%; float: left; text-align: left;">
                                                                                        <asp:TextBox ID="txtfechaDesdeReserva" runat="server" CssClass="form-control mydatepickerclass"
                                                                                            Placeholder="Desde" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="width: 50%; float: left; text-align: left; padding-left: 15px">
                                                                                        <asp:TextBox ID="txtfechaHastaReserva" runat="server" CssClass="form-control mydatepickerclass"
                                                                                            Placeholder="Hasta" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12 col-sm-7 col-md-7" style="padding-top: 15px">
                                                                                <asp:RadioButton ID="rbIntermitenteReserva" runat="server" Width="130px" Text="Intermitente"
                                                                                    OnCheckedChanged="rbIntermitenteReserva_CheckedChanged" AutoPostBack="true" Visible="True" />
                                                                                <div style="clear: both;">
                                                                                    <div style="width: 50%; float: left; text-align: left; padding-left: 15px;">
                                                                                        <asp:Calendar ID="calendarFechasReserva" runat="server" CssClass="mCalendar" OtherMonthDayStyle-CssClass="csOtroDia"
                                                                                             TitleStyle-CssClass="csCalendarEstilo" OnSelectionChanged="calendarFechasReserva_SelectionChanged"
                                                                                            Visible="False"></asp:Calendar>
                                                                                    </div>
                                                                                    <div style="width: 50%; float: left; text-align: left; padding-left: 15px; overflow: auto;">
                                                                                        <asp:GridView ID="gvFechasReserva" runat="server" AutoGenerateColumns="false" DataKeyNames="Date"
                                                                                            CellSpacing="2" CellPadding="4" Width="200px" GridLines="None" BorderWidth="1px"
                                                                                            ShowHeader="False" OnRowCommand="gvFechas_RowCommand" Visible="False">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Date" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="20px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkbtnEliminar" runat="server" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                                                            ToolTip="Eliminar">X</asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Font-Bold="true" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                            </div> 
                                                                        </div>  
                                                                    </div>
                                                                    <br />
                                                                    <div style="clear: both;margin-left: 10px;">
                                                                        <asp:Label class="control-label" ID="lblMsjEstadoReserva"
                                                                            runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                                            TargetControlID="pnl_detalle" CollapsedSize="0" Collapsed="True" ExpandControlID="pnl_cabecera"
                                                            CollapseControlID="pnl_cabecera" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                                                            ImageControlID="img_mas" ExpandedImage="~/Images/collapsed.png" CollapsedImage="~/Images/collapsed2.png"
                                                            ExpandDirection="Vertical">
                                                        </ajaxToolkit:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <asp:Button ID="btnGuardarReserva" class="btn btn-primary" Style="float: right; width: 200px"
                                            runat="server" Text="GUARDAR" OnClick="btnGuardarReserva_Click"></asp:Button>
                                    </div>
                                </div>
                                <div class="row" style="padding-top: 10px">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <div style="float: left; clear: both;">
                                            <asp:Button ID="btnregresResultadoBusqueda" class="btn btn-primary" Style="float: left;
                                                width: 200px" runat="server" Text="< RESULTADO BÚSQUEDA" OnClick="btnregresResultadoBusqueda_Click">
                                            </asp:Button>
                                        </div>
                                        <div style="float: right;">
                                            <asp:Button ID="btnAsociarCliente" class="btn btn-primary" runat="server" Style="width: 200px"
                                                Text="ASOCIAR CLIENTE >" OnClick="btnAsociarCliente_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <asp:Literal runat="server" ID="litvalidacionReservas"></asp:Literal>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlProductoFiltro" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardarReserva" />
                                <asp:AsyncPostBackTrigger ControlID="btnIrReserva" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div id="divAsociarcliente">
                        <asp:UpdatePanel ID="upAsociarCliente" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <br />
                                <div class="row" style="overflow: auto;">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <asp:GridView ID="gvReservasAsociacion" runat="server" CssClass="table table-bordered"
                                            AutoGenerateColumns="False" HeaderStyle-CssClass="tbl_cabecerareal" EmptyDataText="No se han encontrado reservas en la base de datos."
                                            DataKeyNames="reser_c_iid,inm_c_vnomb,pub_prod_c_vnomb,pub_elem_act_c_vnomb,pub_esp_c_vcod">
                                            <Columns>
                                                <asp:TemplateField HeaderText="reser_c_iid" ItemStyle-CssClass="row_acciones" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblreservaidAsociacion" runat="server" Text='<%# Bind("reser_c_iid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="checkAsociacion" runat="server" onkeypress="return NoJavaScript(event)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INMUEBLE" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinmuebleAsociacion" runat="server" Text='<%# Bind("inm_c_vnomb") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PRODUCTO" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproductoAsociacion" runat="server" Text='<%# Bind("pub_prod_c_vnomb") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TIPO DE ELEMENTO / ACTIVACIÓN" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblelementoAsociacion" runat="server" Text='<%# Bind("pub_elem_act_c_vnomb") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COD. ESPACIO" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcodespacioAsociacion" runat="server" Text='<%# Bind("pub_esp_c_vcod") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AGENCIA" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagenciaAsociacion" runat="server" Text='<%# Bind("agencia") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CLIENTE ASOCIADO" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclienteAsociacion" runat="server" Text='<%# Bind("cliente") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MARCA" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmarcaAsociacion" runat="server" Text='<%# Bind("marca") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FATURAR A" ItemStyle-CssClass="row_acciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblflatFacturarAsociacion" runat="server" Text='<%# Bind("facturarA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="row_acciones" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="tbl_cabecerareal" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="table-primary">
                                    <div class="table-header" style="margin-bottom: 10px; color: #555 !important">
                                        <div class="row">
                                            <label>
                                                Los registros seleccionados se asociarán a:</label>
                                            <br />
                                        </div>
                                        <div class="row" style="padding-top: 10px">
                                            <div class="col-xs-12 col-sm-12 col-md-4">
                                                <label>
                                                    Agencia</label>
                                                <br />
                                                <div style="float: left; width: 30%; line-height: 25px">
                                                    Razón Social</div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="txtAgenciaAsociacion" MaxLength="200" runat="server" CssClass="form-control"
                                                        Style="float: left;" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarAgencia" TargetControlID="txtAgenciaAsociacion"
                                                        UseContextKey="True" FirstRowSelected="True" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"
                                                        CompletionInterval="100">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-4">
                                                <label>
                                                    Cliente</label>
                                                <br />
                                                <div style="float: left; width: 30%; line-height: 25px">
                                                    Razón Social <span class="requerido">(*)</span></div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="txtClienteAsociacion" MaxLength="200" runat="server" CssClass="form-control"
                                                        Style="float: left;" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender_txtClienteAsociacion"
                                                        runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarCliente"
                                                        TargetControlID="txtClienteAsociacion" UseContextKey="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" CompletionInterval="100">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-4">
                                                <label>
                                                </label>
                                                <br />
                                                <div style="float: left; width: 30%; line-height: 35px">
                                                    Marca <span class="requerido">(*)</span></div>
                                                <div style="float: left; width: 70%; padding-top: 5px">
                                                    <asp:TextBox ID="txtMarcaAsociacion" runat="server" MaxLength="200" CssClass="form-control"
                                                        Style="float: left;" onkeypress="return NoJavaScript(event)"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarMarca" TargetControlID="txtMarcaAsociacion"
                                                        UseContextKey="True" FirstRowSelected="True" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"
                                                        CompletionInterval="100">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top: 10px">
                                            <div class="col-xs-12 col-sm-12 col-md-6">
                                                <label>
                                                    Facturar a nombre de:
                                                </label>
                                                <br />
                                                <asp:RadioButton ID="rbclienteAsociacion" runat="server" Text="Cliente" GroupName="gnAsociar" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rbAgenciaAsociacion" runat="server" Text="Agencia" GroupName="gnAsociar" />
                                            </div>
                                            <div style="float: right; padding-right: 10px; padding-top: 15px">
                                                <asp:Button ID="btnAsociar" class="btn btn-primary" Style="float: right; width: 200px;"
                                                    runat="server" Text="ASOCIAR" OnClick="btnAsociar_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <asp:Literal runat="server" ID="ltvalidacionReservaAsoc"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12">
                                        <asp:Button ID="btnRegresarPaso2" class="btn btn-primary" Style="float: left; width: 200px;"
                                            runat="server" Text="< RESERVAR" OnClick="btnRegresarPaso2_Click"></asp:Button>
                                        <asp:Button ID="btnCerrarReserva" class="btn btn-primary" Style="float: right; width: 200px;"
                                            runat="server" Text="CERRAR RESERVA" OnClick="btnCerrarReserva_Click"></asp:Button>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvReservasAsociacion" />
                                <asp:AsyncPostBackTrigger ControlID="btnAsociarCliente" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-12">
        </div>
    </div>
    <%--==========================================================================================--%>
    <!-- popup info espacio-->
    <asp:UpdatePanel ID="uppopInfoEspacio" runat="server">
        <ContentTemplate>
            <div id="popupinfo" class="modal fade">
                <div class="modal-dialog" style="width: 70%; margin-top: -80px">
                    <div class="modal-content" style="margin: 0 auto; margin-top: 20%;">
                        <div class="panel-heading" style="background: #207c9f; color: white">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            Reserva - Detalle de espacio publicitario
                        </div>
                        <!-- dialog body -->
                        <!-- dialog buttons -->
                        <div class="panel-body" style="margin-top: -10px;">
                            <div style="clear: both;">
                                <div style="width: 50%; float: left; text-align: left;">
                                    <label class="control-label">
                                        Producto:
                                    </label>
                                    &nbsp;
                                    <label id="lblproductoPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                                <div style="width: 50%; float: left; text-align: left;">
                                    <label class="control-label">
                                        Cod. Espacio:
                                    </label>
                                    &nbsp;
                                    <label id="lblcodespacioPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                            </div>
                            <div style="clear: both; padding-top: 15px">
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label" runat="server" id="lbltexto_elementoPopInfo">
                                        </label>
                                    </div>
                                    <label id="lblelementoPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Medidas:
                                        </label>
                                    </div>
                                    <label id="lblmedidaPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                            </div>
                            <div style="clear: both; padding-top: 15px">
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Ubicación:
                                        </label>
                                    </div>
                                    <label id="lblUbicacionPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Material:
                                        </label>
                                    </div>
                                    <label id="lblMaterialPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                            </div>
                            <div style="clear: both; padding-top: 15px">
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Descripción:
                                        </label>
                                    </div>
                                    <label id="lblDescripcionPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Restricción:
                                        </label>
                                    </div>
                                    <label id="lblrestriccionPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                            </div>
                            <div style="clear: both; padding-top: 15px">
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Área:
                                        </label>
                                    </div>
                                    <label id="lbleareaPopinfo" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                            </div>
                            <div style="clear: both; padding-top: 15px">
                                <div style="width: 50%; float: left; text-align: left;">
                                    <div class="control-label">
                                        <label class="control-label">
                                            Imagenes:
                                        </label>
                                    </div>
                                    <%--<label id="Label3" runat="server" style="font-weight: normal">
                                    </label>--%>
                                    <div id="rondellCarousel">
                                        <asp:Repeater ID="repeaterImage" runat="server">
                                            <ItemTemplate>
                                                    <a data-fancybox="gallery" href="<%# Container.DataItem.ToString().Replace(".min","") %>">
                                                    <img src="<%# Container.DataItem %>" />
                                               </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <%--  <button type="button" class="btn btn-primary">OK</button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- fin popup info espacio -->
    <!-- popup detalle ocupacion -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="popOcupacion" class="modal fade">
                <div class="modal-dialog" style="width: 70%; margin-top: -80px">
                    <div class="modal-content" style="margin: 0 auto; margin-top: 20%;">
                        <div class="panel-heading" style="background: #207c9f; color: white">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            Reserva - Detalle de ocupación espacio publicitario
                        </div>
                        <!-- dialog body -->
                        <!-- dialog buttons -->
                        <div class="panel-body" style="margin-top: -10px;">
                            <div style="clear: both;">
                                <div style="width: 33%; float: left; text-align: left;">
                                    <label class="control-label">
                                        Producto:
                                    </label>
                                    &nbsp;
                                    <label class="" id="lblproductoPopOcupacion" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                                <div style="width: 33%; float: left; text-align: left;">
                                    <label class="control-label">
                                        Cod. Espacio:
                                    </label>
                                    &nbsp;
                                    <label class="control-label" id="lblcodPopOcupacion" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                                <div style="width: 33%; float: left; text-align: left;">
                                    <label class="control-label">
                                        Fecha:
                                    </label>
                                    &nbsp;
                                    <label class="control-label" id="lblFechaPopOcupacion" runat="server" style="font-weight: normal">
                                    </label>
                                </div>
                            </div>
                            <div style="clear: both; margin-top: 20px;">
                                <div style="width: 100%;">
                                    <div class="scrolling-table-container" style="margin-right: 10px; overflow: auto;">
                                        <asp:GridView ID="gvDetalleOcupacion" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered" HeaderStyle-CssClass="tbl_cabecerareal" Style="font-size: 10;
                                            text-align: center;">
                                            <Columns>
                                                <asp:BoundField DataField="fecha" HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="15%" CssClass="HeaderOcupacion" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="estado" HeaderText="ESTADO">
                                                    <HeaderStyle Width="15%" CssClass="HeaderOcupacion" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="detalle" HeaderText="DETALLE">
                                                    <HeaderStyle CssClass="HeaderOcupacion" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <%--  <button type="button" class="btn btn-primary">OK</button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- fin popup -->
    <!-- popup mensaje operacion -->
    <asp:UpdatePanel ID="upmsjAccion" runat="server">
        <ContentTemplate>
            <div id="popmsjAccion" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content" style="height: 150px; border: solid 1px; margin-top: 35%;">
                        <div class="panel-heading" style="background: #207c9f; color: white">
                            Bombero - Real Plaza
                        </div>
                        <div class="panel-body" style="padding: 5px; height: 150px; overflow: auto;">
                            <div class="col-md-12" style="height: auto; margin-top: 5px;">
                                <asp:Label runat="server" ID="lblmensajeAccion"></asp:Label>
                            </div>
                        </div>
                        <div class="panel-footer text-center">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                ACEPTAR
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- popup mensaje -->
    <div id="CUMensajeCargando" class="modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <uc5:CUCargando ID="CUCargando" runat="server" />
    </div>
</asp:Content>
