<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductSupportByBeneficiary.aspx.cs" Inherits="CResearchers_CPanelManageExchangeOrders_PageManageProductSupportByBeneficiary" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductSupportByBeneficiary.ascx" TagPrefix="uc1" TagName="PageManageProductSupportByBeneficiary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageManageProductSupportByBeneficiary runat="server" ID="PageManageProductSupportByBeneficiary" />
</asp:Content>

