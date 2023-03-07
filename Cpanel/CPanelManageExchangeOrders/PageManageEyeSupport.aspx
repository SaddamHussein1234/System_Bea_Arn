<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageEyeSupport.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageEyeSupport" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageEyeSupport.ascx" TagPrefix="uc1" TagName="PageManageEyeSupport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageManageEyeSupport runat="server" ID="PageManageEyeSupport" />
</asp:Content>
