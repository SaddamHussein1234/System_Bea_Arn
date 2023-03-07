<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/WSM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageApprovalOfTheDirector.aspx.cs" Inherits="Cpanel_ERP_WSM_PageShipping_PageApprovalOfTheDirector" %>

<%@ Register Src="~/Shaerd/ERP/WSM/PageShipping/PageApprovalOfTheDirector.ascx" TagPrefix="uc1" TagName="PageApprovalOfTheDirector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageApprovalOfTheDirector runat="server" ID="PageApprovalOfTheDirector" />
</asp:Content>

