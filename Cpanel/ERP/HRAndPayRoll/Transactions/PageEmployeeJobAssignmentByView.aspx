<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeJobAssignmentByView.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeJobAssignmentByView" %>

<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/JobAssignments/PageEmployeeJobAssignmentByView.ascx" TagPrefix="uc1" TagName="PageEmployeeJobAssignmentByView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <uc1:PageEmployeeJobAssignmentByView runat="server" ID="PageEmployeeJobAssignmentByView" />
</asp:Content>

