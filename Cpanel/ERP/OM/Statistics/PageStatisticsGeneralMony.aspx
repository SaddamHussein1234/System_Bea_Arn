<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStatisticsGeneralMony.aspx.cs" Inherits="Cpanel_ERP_OM_Statistics_PageStatisticsGeneralMony" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Statistics/PageStatisticsGeneralMony.ascx" TagPrefix="uc1" TagName="PageStatisticsGeneralMony" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageStatisticsGeneralMony runat="server" ID="PageStatisticsGeneralMony" />
</asp:Content>

