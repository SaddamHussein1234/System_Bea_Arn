<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_Default" %>

<%@ Register Src="~/Shaerd/CPanelManageWarehouse/Default.ascx" TagPrefix="uc1" TagName="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Default runat="server" ID="Default" />
</asp:Content>

