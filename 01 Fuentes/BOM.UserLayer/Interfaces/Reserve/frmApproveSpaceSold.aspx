<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal/Site.Master"
    AutoEventWireup="true" CodeBehind="frmApproveSpaceSold.aspx.cs" Inherits="BOM.UserLayer.Interfaces.Reserve.frmApproveSpaceSold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<style>
 .columGv
 {
     text-align: center;
 }
</style>
    <script type="text/javascript" language="javascript">
        function f_AbrirPopAccion() {
            //$("#popupinfo").modal();
            $("#popmsjAccion").modal({
                "backdrop": true,
                "keyboard": true,
                "show": true
            });

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
        function f_AbrirPopConfirmacion() {
            //$("#popupinfo").modal();
            $("#popmsjConfirmacion").modal({
                "backdrop": true,
                "keyboard": true,
                "show": true
            });
        }


        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1>
            <span class="text-light-gray">PUBLICIDAD / </span>
            <asp:Label ID="lblTituloAccion" runat="server" Text="Reserva"></asp:Label></h1>
    </div>
    <div class="table-primary">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="table-header" style="margin-bottom: 10px">
                    <div class="table-caption">
                        <div class="form-inline">
                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <div class="form-group">
                                <label class="sr-only">
                                    Inmueble</label>
                                <asp:TextBox ID="txtInmueble" onkeypress="return NoJavaScript(event)" Style="width: 100%;"
                                    runat="server" CssClass="form-control" MaxLength="200" Placeholder="Inmueble..."></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarInmueble" TargetControlID="txtInmueble"
                                    UseContextKey="True" FirstRowSelected="True" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"
                                    CompletionInterval="100">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                            <div class="form-group">
                                <label class="sr-only">
                                    Cliente</label>
                                <asp:TextBox ID="txtCliente" onkeypress="return NoJavaScript(event)" Style="width: 100%;"
                                    runat="server" CssClass="form-control" MaxLength="200" Placeholder="Cliente..."></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarCliente" TargetControlID="txtCliente"
                                    UseContextKey="True" FirstRowSelected="True" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"
                                    CompletionInterval="100">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                            <div class="form-group">
                                <label class="sr-only">
                                    Ejecutivo Publicidad</label>
                                <asp:DropDownList CssClass="form-control" Style="width: 100%;" ID="ddlEjecutivo"
                                    runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <asp:LinkButton ID="btnBuscar" CssClass="btn btn-labeled btn-primary" OnClick="btnBuscar_Click"
                                runat="server"><span class="btn-label icon fa fa-search"></span>Buscar</asp:LinkButton>
                            <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
                <div class="row" style="overflow: auto">
                    <asp:GridView ID="gvReservas" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="tbl_cabecerareal" ShowHeaderWhenEmpty="True" EmptyDataText="No se han encontrado reservas en la base de datos."
                        DataKeyNames="MARCAR,reser_mast_c_iid,reser_c_iid,pub_esp_c_iid,reser_c_dfech_inicio,reser_c_dfech_fin,pub_esp_c_vcod,FACTURARA,PRECIOFINAL"
                        OnRowDataBound="gvReservas_RowDataBound" Width="2500px">
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="row_acciones">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEspacio" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INMUEBLE">
                                <ItemTemplate>
                                    <asp:Label ID="lblInmueble" runat="server" Text='<%# Eval("INMUEBLE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRODUCTO">
                                <ItemTemplate>
                                    <asp:Label ID="lblProducto" runat="server" Text='<%# Eval("PRODUCTO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TIPO ELEMENTO / ACTIVACIÓN">
                                <ItemTemplate>
                                    <asp:Label ID="lblElemento" runat="server" Text='<%# Eval("ELEMENTO_ACTIVACION") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="COD. ESPACIO">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodEspacio" runat="server" Text='<%# Eval("COD_ESPACIO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CLIENTE">
                                <ItemTemplate>
                                    <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("CLIENTE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MARCA">
                                <ItemTemplate>
                                    <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("MARCA") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AGENCIA">
                                <ItemTemplate>
                                    <asp:Label ID="lblAgencia" runat="server" Text='<%# Eval("AGENCIA") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PERIODO">
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriodo" runat="server" Text='<%# Eval("PERIODO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TIPO PERIODO">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoPeriodo" runat="server" Text='<%# Eval("TIPO_PERIODO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EJECUTIVO">
                                <ItemTemplate>
                                    <asp:Label ID="lblEjecutivo" runat="server" Text='<%# Eval("EJECUTIVO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRECIO FINAL S/">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrecioFinalgv" MaxLength="19" runat="server" Text='<%# Eval("PRECIOFINAL") %>'
                                        Style="text-align: right;" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtPrecioFinalgv" runat="server"
                                        Enabled="True" TargetControlID="txtPrecioFinalgv" ValidChars=".0123456789">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FACTURAR A NOMBRE DE">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbFacturarXClientegv" Text="Cliente" runat="server" GroupName="gnFacturarA" />
                                    <asp:RadioButton ID="rbFacturarXAgenciagv" Text="Agencia" runat="server" GroupName="gnFacturarA" />
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reserva" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblReserva" runat="server" Text='<%# Eval("RESERVA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <asp:Button ID="btnVendido" class="btn btn-primary" runat="server" Text="Vendido"
                    OnClick="btnVendido_Click"></asp:Button>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <asp:Literal runat="server" ID="ltvalidacionReservaVendido"></asp:Literal>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnVendido" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="row">
        </div>
    </div>
    <!-- popup mensaje operacion -->
    <asp:UpdatePanel ID="upmsjAccion" runat="server">
        <ContentTemplate>
            <div id="popmsjAccion" class="modal fade" data-backdrop="static" data-keyboard="true">
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
                            <button id="btnpopAceptarAccion" runat="server"  onkeypress="return NoJavaScript(event)"  type="button" class="btn btn-primary" data-dismiss="modal">
                                ACEPTAR
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnVendido" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- popup mensaje -->
  <!-- popup mensaje confirmacion -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="popmsjConfirmacion" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content" style="height: 150px; border: solid 1px; margin-top: 35%;">
                        <div class="panel-heading" style="background: #207c9f; color: white">
                            Bombero - Real Plaza
                        </div>
                        <div class="panel-body" style="padding: 5px; height: 150px; overflow: auto;">
                            <div class="col-md-12" style="height: auto; margin-top: 5px;">
                                <asp:Label runat="server" ID="lblMensajeConfirmacion"></asp:Label>
                            </div>
                        </div>
                        <div class="panel-footer text-center">
                            <asp:Button ID="idOkAceptar"  onkeypress="return NoJavaScript(event)"  class="btn btn-primary" data-dismiss="modal" runat="server" Text="ACEPTAR"
                                    OnClick="btnidOkAceptar_Click"></asp:Button>
                            </button>
                             <button id="idOkCancelar"  onkeypress="return NoJavaScript(event)"  runat="server" type="button" class="btn btn-primary" data-dismiss="modal">
                                CANCELAR
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- popup mensaje -->
</asp:Content>
