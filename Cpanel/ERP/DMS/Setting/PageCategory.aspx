<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/DMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageCategory.aspx.cs" Inherits="Cpanel_ERP_DMS_Setting_PageCategory" %>

<%@ Register Src="~/Shaerd/ERP/DMS/Setting/PageCategory.ascx" TagPrefix="uc1" TagName="PageCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageCategory runat="server" ID="PageCategory" />
</asp:Content>

