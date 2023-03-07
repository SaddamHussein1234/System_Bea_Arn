<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeBonusesByView.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeBonusesByView" %>

<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/EmployeeBonuses/PageEmployeeBonusesByView.ascx" TagPrefix="uc1" TagName="PageEmployeeBonusesByView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageEmployeeBonusesByView runat="server" ID="PageEmployeeBonusesByView" />
</asp:Content>

