<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CUMensajeError.ascx.cs"
    Inherits="SistemaBombero.ControlUsuario.CUMensajeError" %>
<style>
    .posicionPopup
    {
        margin-top: 10%;
        margin-left: 33%;
    }
    
    /* Media Queries */
    
    @media screen and (max-width: 900px)
    {
        .posicionPopup
        {
            margin-top: 10%;
            margin-left: 20%;
            width:100%;
        }
    }
    
        @media screen and (max-width: 600px)
    {
        .posicionPopup
        {
            margin-top: 10%;
            margin-left:10%;
            width:100%;
        }
    }
    
    
       @media screen and (max-width: 500px)
    {
        .posicionPopup
        {
            margin-top: 10%;
            margin-left:0px;
            width:100%;
        }
    }
    
</style>
<div class="col-sm-4 posicionPopup">
    <div class="panel panel-default" style="max-width: 425px; border-radius: 5px !important;">
        <div class="panel-heading">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                ×</button>
            <h3 class="panel-title">
                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Error.ico" Width="20px" style="margin-top:-3px;"/>--%>
                <asp:Label ID="lblCUTitulo" runat="server" Text="Ocurrio un error en la transacción"></asp:Label>
            </h3>
        </div>
        <div class="panel-body" style="text-align: center;">
            <div>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Error.ico" Width="22px"
                    Style="margin-top: -3px;" />
                <asp:Label ID="lblCUMensaje" runat="server" Text="Mensaje del sistema."></asp:Label>
            </div>
            <div style="text-align: center; margin-top: 5px;">
                <asp:Button ID="btnCUAceptar" runat="server" Text="Aceptar" CssClass="btn btn-default"
                    data-dismiss="modal" Style="border-radius: 5px !important;" />
            </div>
        </div>
    </div>
</div>
