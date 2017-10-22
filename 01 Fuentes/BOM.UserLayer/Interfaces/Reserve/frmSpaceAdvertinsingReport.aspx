<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal/Site.Master" AutoEventWireup="true" CodeBehind="frmSpaceAdvertinsingReport.aspx.cs" Inherits="BOM.UserLayer.Interfaces.Reserve.frmSpaceAdvertinsingReport" %>
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
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <h1>
            <span class="text-light-gray">PUBLICIDAD / </span>
            <asp:Label ID="lblTituloAccion" runat="server" Text="Reporte de espacios publicitarios"></asp:Label></h1>
    </div>
    <div class="table-primary">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="table-header" style="margin-bottom: 10px">
                    <div class="table-caption">
                        <div class="form-inline">
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
                                    Ejecutivo Publicidad</label>
                                <asp:DropDownList CssClass="form-control" Style="width: 100%;" ID="ddlEjecutivo"
                                    runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                 <label class="sr-only">
                                    Tipo Producto</label>
                                <asp:DropDownList CssClass="form-control" Style="width: 100%;" ID="ddlProducto"
                                    runat="server" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                          <%--  <div class="form-group">
                                <label class="sr-only">
                                    Cliente</label>
                                <asp:TextBox ID="txtTipoProducto" onkeypress="return NoJavaScript(event)" Style="width: 100%;"
                                    runat="server" CssClass="form-control" MaxLength="200" Placeholder="Tipo Producto..."></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarCliente" TargetControlID="txtTipoProducto"
                                    UseContextKey="True" FirstRowSelected="True" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"
                                    CompletionInterval="100">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>--%>
                             <div class="form-group">
                                <label class="sr-only">
                                    Estado</label>
                                <asp:TextBox ID="txtEstado" onkeypress="return NoJavaScript(event)" Style="width: 100%;"
                                    runat="server" CssClass="form-control" MaxLength="200" Placeholder="Estado..."></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" ServiceMethod="metodAutocompletarCliente" TargetControlID="txtEstado"
                                    UseContextKey="True" FirstRowSelected="True" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"
                                    CompletionInterval="100">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                            <asp:LinkButton ID="btnBuscar" CssClass="btn btn-labeled btn-primary" OnClick="btnBuscar_Click"
                                runat="server"><span class="btn-label icon fa fa-search"></span>Buscar</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row" style="overflow: auto">
                    <asp:GridView ID="gvReservas" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                        HeaderStyle-CssClass="tbl_cabecerareal" ShowHeaderWhenEmpty="True" EmptyDataText="No se han encontrado reservas en la base de datos."
                        DataKeyNames="MARCAR,ejec_c_cdoc_id,reser_mast_c_iid,reser_c_iid,pub_esp_c_iid,reser_c_dfech_inicio,reser_c_dfech_fin,pub_esp_c_vcod"
                        OnRowCommand="gvReservas_RowCommand"  
                        OnRowDataBound="gvReservas_RowDataBound" Width="2500px" 
                        onselectedindexchanged="gvReservas_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="row_acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkReservaMaster" runat="server" CommandName="editarReserva"
                                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Text="Editar"></asp:LinkButton>
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
                            <asp:TemplateField HeaderText="ESTADO RESERVA">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrecioFinalgv" runat="server" Text='<%# Eval("ESTADORESERVA") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="columGv"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
    </div>
</asp:Content>
