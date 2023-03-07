<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductAddThePriceToOrder.aspx.cs" Inherits="CResearchers_CPanelManageExchangeOrders_PageManageProductAddThePriceToOrder" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductAddThePriceToOrder.ascx" TagPrefix="uc1" TagName="PageManageProductAddThePriceToOrder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">

        <uc1:PageManageProductAddThePriceToOrder runat="server" ID="PageManageProductAddThePriceToOrder" />

    <script src="../css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

