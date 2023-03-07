<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/HRAndPayRoll/Admin/MPAdmin.master" AutoEventWireup="true" CodeFile="PageEmployeeLoanAdd.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Admin_PageEmployeeLoanAdd" %>
<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/EmployeeLoans/PageEmployeeLoanAdd.ascx" TagPrefix="uc1" TagName="PageEmployeeLoanAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageEmployeeLoanAdd runat="server" ID="PageEmployeeLoanAdd" />
</asp:Content>
