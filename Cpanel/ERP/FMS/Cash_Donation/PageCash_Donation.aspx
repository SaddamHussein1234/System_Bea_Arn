<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageCash_Donation.aspx.cs" Inherits="Cpanel_ERP_FMS_Cash_Donation_PageCash_Donation" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Cash_Donation/PageCash_Donation.ascx" TagPrefix="uc1" TagName="PageCash_Donation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageCash_Donation runat="server" ID="PageCash_Donation" />
</asp:Content>

