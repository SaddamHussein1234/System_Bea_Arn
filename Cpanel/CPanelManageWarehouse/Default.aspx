<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_CPanelManageWarehouse_Default" %>

<%@ Register Src="~/Shaerd/CPanelManageWarehouse/Default.ascx" TagPrefix="uc1" TagName="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Default runat="server" ID="Default" />
</asp:Content>

