<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CResearchers_CPanelManageExchangeOrders_Default" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/Default.ascx" TagPrefix="uc1" TagName="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:Default runat="server" ID="Default" />
</asp:Content>

