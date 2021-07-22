<%@ Page Title="Tablero Informativo" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultCrg.aspx.vb" Inherits="TableroInf._DefaultCrg" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
            <asp:ScriptManager runat="server" ID="SM2">
            <Scripts>
                <%--Para obtener más información sobre cómo agrupar scripts en ScriptManager, consulte http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Scripts de Framework--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Scripts del sitio--%>
            </Scripts>
        </asp:ScriptManager>
    <div class="row">
        <div class="col-md-4">


            <br />

            <br />



            <br />

            <asp:UpdatePanel ID="updp_main" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <asp:Label ID="lblFecha" runat="server" style="text-align: center" Text="." Font-Names="Arial Black" Font-Size="X-Large" ForeColor="Black" Width="100%"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
                    <asp:Timer ID="Timer1" runat="server">
                    </asp:Timer>
            <br />
        </div>
        <div class="col-md-4">
        </div>
    </div>

</asp:Content>
