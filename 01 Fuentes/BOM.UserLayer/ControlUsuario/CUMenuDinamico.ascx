<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CUMenuDinamico.ascx.cs"
    Inherits="SistemaBombero.ControlUsuario.CUMenuDinamico" %>
<style type="text/css">
    div.hideSkiplink
    {
        background-color: #3a4f63;
        width: 100%;
    }
    
    div.menu
    {
        background: white;
        width: 100%;
        border-top:2px solid #7ac0da;
        border-bottom:2px solid #7ac0da;
    }
    
    div.menu ul
    {
        list-style: none;
        margin: 0px;
        background: none;

    }
    
    div.menu ul li
    {
        background:white;
        
    }
    
    
    /*PARA CAMBIAR ESTILOS Y COLORES AL MENU*/
    div.menu ul li a, 
    div.menu ul li a:visited
    {
        cursor: pointer;
        background-color: none;
        
        color: Black;
        display: block;
        line-height: 1.35em;
        padding: 4px 20px;
        text-decoration: none;
        white-space: nowrap;
        transition: 0.2s all;
    }
   
     div.menu ul li a:active
    {
        background-color: #337ab7;
        color: #cfdbe6;
        text-decoration: none;
    }
  
    div.menu ul li a:hover
    {
        background-color: #bfcbd6;
        color: #465c71;
        text-decoration: none;
        padding: 4px 20px;
    }
    
    div.menu ul li a:active
    {
        background-color: #465c71;
        color: #cfdbe6;
        text-decoration: none;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.level3.dynamic
    {
        background: White;
    }
    
    .level1 a, #subMenusContainer a
    {
        text-decoration: none;
        display: block;
        padding: 8px 20px;
    }
    
    
    .level1 a
    {
        margin: 0;
        float: left;
    }
    
    #subMenusContainer a, .level1 li li a
    {
        text-align: left;
        white-space: nowrap;
    }
    
    
    .level1 a:hover, .level1 a:focus, #subMenusContainer a:hover, #subMenusContainer a:focus, .level1 a.mainMenuParentBtnFocused, #subMenusContainer a.subMenuParentBtnFocused a.level2:hover, a.level2:focus
    {
        background-color: #7ac0da;
        color: #FFF;
    }
    
    
    
    #subMenusContainer a:hover, #subMenusContainer a:focus, .level1 a.mainMenuParentBtnFocused, #subMenusContainer a.subMenuParentBtnFocused
    {
        background-color: #843388;
        background-image: url(../Imagenes/Menu/bg-topmenu-over.jpg);
        background-repeat: repeat-x;
        background-position: top;
        color: #FFF;
    }
    
    
    .subMenuParentBtn
    {
        background: url(../Imagenes/Menu/arrow_right.gif) right center no-repeat;
    }
    
    .subMenuParentBtnFocused
    {
        background: url(../Imagenes/Menu/arrow_right_over.gif) right center no-repeat;
    }
    
    .mainMenuParentBtn, a.level1
    {
        background: url(../Imagenes/Menu/arrow_down.gif) right center no-repeat;
    }
    
    .mainMenuParentBtnFocused
    {
        background: url(../Imagenes/Menu/arrow_down_over.gif) right center no-repeat;
    }
    
    .smOW
    {
        display: none;
        position: absolute;
        overflow: hidden;
        padding: 0 2px;
        margin: 0 0 0 -2px;
    }
    
    .level1, .level1 ul, .level1 ol, #subMenusContainer ul, #subMenusContainer ol
    {
        padding: 0;
        margin: 0;
        list-style: none;
        line-height: 1em;
        font-family: Arial, Helvetica, sans-serif !important;
        font-size: 12px !important;
    }
    
    .level1 ol, .level1 ul, #subMenusContainer ul, #subMenusContainer ol
    {
        background: #fff; /*border: 1px solid #b590b7;*/
        left: 0;
    }
    
    .level1 li
    {
        display: block;
        list-style: none;
        position: relative;
        float: left;
    }
    
    #subMenusContainer li
    {
        list-style: none;
    }
    
    .level1
    {
        display: block;
        list-style: none;
        margin: 0 0 0 0;
        z-index: 5;
        top: 0px;
        left: 0%;
        text-align: center;
    }
    
    #subMenusContainer
    {
        display: block;
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 0;
        overflow: visible;
        z-index: 1000000000;
    }
    
    .level1 li li
    {
        float: none;
    }
    
    .level1 li li a
    {
        position: relative;
        float: none;
    }
    
    .level1 li ul
    {
        position: absolute;
        width: 10em;
        margin-left: -1000em;
        margin-top: 2.2em;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.static
    {
        padding-left: 1.25em !important;
        padding-right: 1.25em !important;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a:link
    {
        color: #8b8a8a;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a:visited
    {
        color: #8b8a8a;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.dynamic
    {
        color: #8b8a8a;
        background: url("/WebResource.axd?d=aRbFavUJW87dTAR89hyVZHqaYNeHRG0qAqH1QebZd5N58Ny-XiMmaUag7iDvUTc_TklxFx6J4H5jeX0ju5oREcqmQVXradEYhlyQfRp1XaM1&t=635073195924108275") no-repeat right center;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.dynamic:hover, focus
    {
        background-color: #843388;
        background-image: url(../Imagenes/Menu/bg-topmenu-over.jpg);
        background-repeat: repeat-x;
        background-position: top;
        color: #FFF;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.dynamic:focus
    {
        color: #843388;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a:hover
    {
        background-color: #843388;
        background-image: url(../Imagenes/Menu/bg-topmenu-over.jpg);
        background-repeat: repeat-x;
        background-position: top;
        color: #FFF;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.level2.dynamic
    {
        background: White;
        color: gray;
    }
    #ctl00_wucMenuDinamico2_MnDinamico a.popout-dynamic.level2.dynamic
    {
        background: White;
        color: gray;
        background: url(../Imagenes/Menu/arrow_right.gif) right center no-repeat;
    }
    #ctl00_wucMenuDinamico2_MnDinamico a.popout-dynamic.level2.dynamic:hover, #ctl00_wucMenuDinamico2_MnDinamico a.popout-dynamic.level2.dynamic:focus
    {
        background-color: #843388;
        background-image: url(../Imagenes/Menu/bg-topmenu-over.jpg);
        background-repeat: repeat-x;
        background-position: top;
        color: #FFF;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.level3.dynamic
    {
        background: White;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a.level3.dynamic:hover, #ctl00_wucMenuDinamico2_MnDinamico a.level2:focus
    {
        background-color: #843388;
        background-image: url(../Imagenes/Menu/bg-topmenu-over.jpg);
        background-repeat: repeat-x;
        background-position: top;
        color: #FFF;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico a:hover, #ctl00_wucMenuDinamico2_MnDinamico a:focus
    {
        color: #FFFFFF;
        text-decoration: none;
    }
    #ctl00_wucMenuDinamico2_MnDinamico a.level2:hover, #ctl00_wucMenuDinamico2_MnDinamico a.level2:focus
    {
        background-color: #843388;
        background-image: url(../Imagenes/Menu/bg-topmenu-over.jpg);
        background-repeat: repeat-x;
        background-position: top;
        color: #FFF;
    }
    
    #ctl00_wucMenuDinamico2_MnDinamico
    {
        padding: 0px;
        width: 100%;
        height: 28px;
        background: #fff;
        background-image: url(../Imagenes/Menu/bg-topmenu.jpg);
        background-repeat: repeat-x;
        background-position: top;
        margin: 0 0 0 0;
        border: solid #843388;
        border-width: 1px 0 1px 0;
    }
</style>
<asp:Menu ID="MnDinamico" runat="server" Font-Bold="False" CssClass="menu" Orientation="Horizontal"
    RenderingMode="List" IncludeStyleBlock="true" StaticEnableDefaultPopOutImage="False"
    SkipLinkText="">
</asp:Menu>
