<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPGeneralAssembly/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdd.aspx.cs" Inherits="Cpanel_CPGeneralAssembly_PageAdd" %>

<%@ Register Src="~/Shaerd/ERP/FMS/GeneralAssembly/PageAdd.ascx" TagPrefix="uc1" TagName="PageAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageAdd runat="server" ID="PageAdd" />
</asp:Content>
