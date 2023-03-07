<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/HRAndPayRoll/Admin/MPAdmin.master" AutoEventWireup="true" CodeFile="PageEmployeeLeaveCategoryByView.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Admin_PageEmployeeLeaveCategoryByView" %>

<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/EmployLeave/PageEmployeeLeaveCategoryByView.ascx" TagPrefix="uc1" TagName="PageEmployeeLeaveCategoryByView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageEmployeeLeaveCategoryByView runat="server" ID="PageEmployeeLeaveCategoryByView" />
</asp:Content>

