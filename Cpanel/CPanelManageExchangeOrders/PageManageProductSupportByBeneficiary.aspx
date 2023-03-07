<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductSupportByBeneficiary.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageProductSupportByBeneficiary" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductSupportByBeneficiary.ascx" TagPrefix="uc1" TagName="PageManageProductSupportByBeneficiary" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <uc1:PageManageProductSupportByBeneficiary runat="server" ID="PageManageProductSupportByBeneficiary" />
</asp:Content>

