<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSupported.aspx.cs" Inherits="Cpanel_ERP_FMS_Supported_PageSupported" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Supported/PageSupported.ascx" TagPrefix="uc1" TagName="PageSupported" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageSupported runat="server" ID="PageSupported" />
</asp:Content>
