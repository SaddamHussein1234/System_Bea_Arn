<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/EOS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdd.aspx.cs" Inherits="Cpanel_ERP_EOS_Operating_Expenses_PageAdd" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Operating_Expenses/PageAdd.ascx" TagPrefix="uc1" TagName="PageAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageAdd runat="server" ID="PageAdd" />
</asp:Content>

