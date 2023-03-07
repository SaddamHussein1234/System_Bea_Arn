<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStatisticsReceipt.aspx.cs" Inherits="Cpanel_ERP_FMS_Statistics_PageStatisticsReceipt" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Statistics/PageStatisticsReceipt.ascx" TagPrefix="uc1" TagName="PageStatisticsReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageStatisticsReceipt runat="server" ID="PageStatisticsReceipt" />
</asp:Content>

