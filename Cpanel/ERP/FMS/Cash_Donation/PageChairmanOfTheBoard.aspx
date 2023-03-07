<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageChairmanOfTheBoard.aspx.cs" Inherits="Cpanel_ERP_FMS_Cash_Donation_PageChairmanOfTheBoard" %>

<%@ Register Src="~/Shaerd/ERP/FMS/Cash_Donation/PageChairmanOfTheBoard.ascx" TagPrefix="uc1" TagName="PageChairmanOfTheBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageChairmanOfTheBoard runat="server" ID="PageChairmanOfTheBoard" />
</asp:Content>

