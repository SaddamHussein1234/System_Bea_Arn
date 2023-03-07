<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageOrganstions.aspx.cs" Inherits="Cpanel_ERP_OM_Receipt_PageOrganstions" %>

<%@ Register Src="~/Shaerd/ERP/OM/PageOrganstions.ascx" TagPrefix="uc1" TagName="PageOrganstions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageOrganstions runat="server" ID="PageOrganstions" />
</asp:Content>

