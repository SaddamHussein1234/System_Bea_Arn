<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageCashier.aspx.cs" Inherits="Cpanel_ERP_FMS_Cash_Donation_PageCashier" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Cash_Donation/PageCashier.ascx" TagPrefix="uc1" TagName="PageCashier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageCashier runat="server" ID="PageCashier" />
</asp:Content>

