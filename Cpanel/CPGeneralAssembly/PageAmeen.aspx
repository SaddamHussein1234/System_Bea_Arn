<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPGeneralAssembly/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAmeen.aspx.cs" Inherits="Cpanel_CPGeneralAssembly_PageAmeen" %>

<%@ Register Src="~/Shaerd/ERP/FMS/GeneralAssembly/PageAmeen.ascx" TagPrefix="uc1" TagName="PageAmeen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageAmeen runat="server" ID="PageAmeen" />
</asp:Content>

