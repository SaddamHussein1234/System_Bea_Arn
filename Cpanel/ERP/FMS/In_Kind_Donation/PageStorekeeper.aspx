<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStorekeeper.aspx.cs" Inherits="Cpanel_ERP_FMS_In_Kind_Donation_PageStorekeeper" %>

<%@ Register Src="~/Shaerd/ERP/FMS/In_Kind_Donation/PageStorekeeper.ascx" TagPrefix="uc1" TagName="PageStorekeeper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageStorekeeper runat="server" ID="PageStorekeeper" />
</asp:Content>

