<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageIn_Kind_Donation.aspx.cs" Inherits="Cpanel_ERP_FMS_In_Kind_Donation_PageIn_Kind_Donation" %>

<%@ Register Src="~/Shaerd/ERP/FMS/In_Kind_Donation/PageIn_Kind_Donation.ascx" TagPrefix="uc1" TagName="PageIn_Kind_Donation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageIn_Kind_Donation runat="server" ID="PageIn_Kind_Donation" />
</asp:Content>

