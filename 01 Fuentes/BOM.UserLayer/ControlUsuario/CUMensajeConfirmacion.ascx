<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CUMensajeConfirmacion.ascx.cs" Inherits="SistemaBombero.ControlUsuario.CUMensajeConfirmacion" %>
<div id="Confirmacion" >
    <div class="col-sm-4" style="margin-top:15%; margin-left:33%;">
    <div class="panel panel-default" style="max-width:425px; border-radius:5px !important;">
            <div class="panel-heading">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3 class="panel-title">
                    <%--<asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Confirmacion.ico" Width="20px" style="margin-top:-3px;"/>--%>
                    <asp:Label ID="lblCUTitulo" runat="server" Text="Confirmar la siguiente transacción"></asp:Label>
                </h3>
            </div>
            <div class="panel-body" style="text-align:center;">
                <div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Confirmacion.ico" Width="20px" style="margin-top:-3px;"/>
                    <asp:Label ID="lblCUMensaje" runat="server" Text="Mensaje del sistema."></asp:Label>
                </div>
                <div style="text-align:center; margin-top:5px;">
                    <asp:Button ID="btnCUAceptar" runat="server" Text="Aceptar" CssClass="btn btn-default" onclick="btnCUAceptar_Click" data-dismiss="modal" UseSubmitBehavior="false" style="border-radius: 5px !important;"/>
                    <asp:Button ID="btnCUCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" data-dismiss="modal" style="border-radius: 5px !important;"/>
                </div>
            </div>
        </div>
    </div>
</div>
