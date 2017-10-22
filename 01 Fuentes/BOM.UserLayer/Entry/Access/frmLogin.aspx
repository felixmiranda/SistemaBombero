<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="BOM.UserLayer.Entry.Access.frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html class="gt-ie8 gt-ie9 not-ie" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"/>

    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,400,600,700,300&subset=latin" rel="stylesheet" type="text/css"/>

    <link href="~/Styles/Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Styles/pixel-admin.min.css" rel="stylesheet" type="text/css"/>
    <link href="~/Styles/Styles/pages.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Styles/rtl.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Styles/themes.min.css" rel="stylesheet" type="text/css"/>
</head>
<body class="theme-purple-hills page-signin" >
    <form id="frmPrincipal" runat="server">
    <div id="page-signin-bg">
        <div class="overlay">
        </div>
        <asp:Image ID="imgBackground" runat="server" ImageUrl="~/Images/background/signin-bg-1.jpg" style="-moz-background-size: cover !important; -webkit-background-size: cover !important; -o-background-size: cover !important;background-size: cover !important;" />
    </div>
    <div class="signin-container" >
        <div class="signin-form" style="background: rgba(255,255,255,0.5) !important">
            <div class="alert alert-danger" id="alert_content" style="display: none;">
                <button type="button" class="close" data-dismiss="alert">
                    ×</button>
                <div id="alert_mensaje">
                </div>
            </div>
            
            <div align="center" style="padding-bottom:15px">
                <asp:Image ID="imgLogo" Style="height: 64px; text-align:center;" runat="server"
                    ImageUrl="~/Images/logo_rp.png" />
            </div>
            <div class="form-group w-icon">
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control input-lg" title="Ingrese su usuario."
                    Placeholder="Usuario" Width="100%" Style="font-family: Calibri;" MaxLength="20"
                    onkeypress="return NoJavaScript(event)"></asp:TextBox>
                <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-lg" Placeholder="Usuario" Text=""></asp:TextBox>--%>
                <span class="fa fa-user signin-form-icon"></span>
            </div>
            <div class="form-group w-icon">
                <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control input-lg" TextMode="Password"
                    Placeholder="Contraseña" title="Ingrese su contraseña." Width="100%" Style="font-family: Calibri;"
                    MaxLength="20" onkeypress="getEventoClick()"></asp:TextBox>
                <%-- <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control input-lg" Placeholder="Contraseña" TextMode="Password"></asp:TextBox>--%>
                <span class="fa fa-lock signin-form-icon"></span>
            </div>
            <div class="form-actions">
                <asp:Button ID="btnIngresar" runat="server" Width="100%" runat="server" CssClass="signin-btn bg-primary" 
                    Text="INGRESAR" Style="border-radius: 5px !important; background-color:#0F7989 !important" OnClick="btnIngresar_Click" />
                <%--<asp:Button ID="Button1" runat="server" Text="INGRESAR" CssClass="signin-btn bg-primary" OnClick="btnIngresar_Click"/>
                --%>
            </div>
        </div>
        <script type="text/javascript">            window.jQuery || document.write('<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js">' + "<" + "/script>"); </script>
        <script>
            $("#<%= btnIngresar.ClientID%>").click(function () {
                var bValid = true;
                if ($("#<%=txtUsuario.ClientID%>").val() == "") {
                    bValid = false;
                }

                if ($("#<%=txtContrasena.ClientID%>").val() == "")
                    bValid = false;
                return bValid;
            });
          </script>
          <script src="<%=ResolveClientUrl("~/Scripts/bootstrap.min.js") %>"></script>
          <script src="<%=ResolveClientUrl("~/Scripts/pixel-admin.min.js") %>"></script>
    </div>
    </form>
</body>
</html>
