<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMainItems.aspx.cs" Inherits="Cpanel_ERP_OM_Cashing_PageMainItems" %>

<%@ Register Src="~/Shaerd/ERP/OM/MainItems/PageSubItems.ascx" TagPrefix="uc1" TagName="PageSubItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageSubItems runat="server" ID="PageSubItems" />
</asp:Content>
