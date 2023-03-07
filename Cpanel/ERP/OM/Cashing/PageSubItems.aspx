<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSubItems.aspx.cs" Inherits="Cpanel_ERP_OM_Cashing_PageSubItems" %>

<%@ Register Src="~/Shaerd/ERP/OM/MainItems/PageMainItems.ascx" TagPrefix="uc1" TagName="PageMainItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageMainItems runat="server" ID="PageMainItems" />
</asp:Content>
