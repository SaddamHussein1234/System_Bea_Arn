<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/HRAndPayRoll/Admin/MPAdmin.master" AutoEventWireup="true" CodeFile="PageEmployeeLeaveCategoryByAdmin.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Admin_PageEmployeeLeaveCategoryByAdmin" %>
<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/EmployLeave/PageEmployeeLeaveCategoryByAdmin.ascx" TagPrefix="uc1" TagName="PageEmployeeLeaveCategoryByAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageEmployeeLeaveCategoryByAdmin runat="server" ID="PageEmployeeLeaveCategoryByAdmin" />
</asp:Content>

