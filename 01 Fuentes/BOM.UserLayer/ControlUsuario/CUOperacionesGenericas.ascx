<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CUOperacionesGenericas.ascx.cs" Inherits="SistemaBombero.ControlUsuario.CUOperacionesGenericas" %>
<div class="modal-dialog">
    <div class="panel panel-success" style="max-width:600px;">
    	<div class="modal-header">
		    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
			<h4 id="H2">Operaciones Generales</h4>
	    </div> 
    	<div class="modal-body" style="text-align:center">
        <asp:UpdatePanel ID="upGenericas" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMensaje" runat="server" Text="[...]"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
	    </div> 
        <div class="form-box-content">
            <div class="form-group" style="text-align:center">
                <asp:UpdatePanel ID="upBotones" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnAceptar" CssClass="btn btn-default" runat="server" Text="Aceptar" onclick="btnAceptar_Click"/>              
                    <asp:Button ID="btnCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" data-dismiss="modal"/>              
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>  
    </div>
</div>

