<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_Default" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/Default.ascx" TagPrefix="uc1" TagName="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <uc1:Default runat="server" ID="Default" />
</asp:Content>

