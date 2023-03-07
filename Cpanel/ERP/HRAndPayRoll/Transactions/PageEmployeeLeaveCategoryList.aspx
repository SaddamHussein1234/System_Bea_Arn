<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeLeaveCategoryList.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLeaveCategoryList" %>

<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/EmployLeave/PageEmployeeLeaveCategoryList.ascx" TagPrefix="uc1" TagName="PageEmployeeLeaveCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageEmployeeLeaveCategoryList runat="server" ID="PageEmployeeLeaveCategoryList" />
</asp:Content>

