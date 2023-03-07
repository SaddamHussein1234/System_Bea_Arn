﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAll.aspx.cs" Inherits="Cpanel_ERP_FMS_GA_PageAll" %>

<%@ Register Src="~/Shaerd/ERP/FMS/GeneralAssembly/PageAll.ascx" TagPrefix="uc1" TagName="PageAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageAll runat="server" ID="PageAll" />
</asp:Content>
