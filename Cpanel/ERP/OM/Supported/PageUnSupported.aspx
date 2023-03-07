<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageUnSupported.aspx.cs" Inherits="Cpanel_ERP_OM_Supported_PageUnSupported" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Supported/PageUnSupported.ascx" TagPrefix="uc1" TagName="PageUnSupported" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageUnSupported runat="server" ID="PageUnSupported" />
</asp:Content>

