<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStatisticsByCategory.aspx.cs" Inherits="Cpanel_ERP_OM_Statistics_PageStatisticsByCategory" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Statistics/PageStatisticsByCategory.ascx" TagPrefix="uc1" TagName="PageStatisticsByCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageStatisticsByCategory runat="server" ID="PageStatisticsByCategory" />
</asp:Content>

