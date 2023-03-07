﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeAccountableByView.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeAccountableByView" %>

<%@ Register Src="~/Cpanel/ERP/HRAndPayRoll/Shaerd/EmployeeAccountables/PageEmployeeAccountableByView.ascx" TagPrefix="uc1" TagName="PageEmployeeAccountableByView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageEmployeeAccountableByView runat="server" ID="PageEmployeeAccountableByView" />
</asp:Content>

