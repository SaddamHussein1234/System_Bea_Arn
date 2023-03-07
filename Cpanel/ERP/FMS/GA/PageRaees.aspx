<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageRaees.aspx.cs" Inherits="Cpanel_ERP_FMS_GA_PageRaees" %>

<%@ Register Src="~/Shaerd/ERP/FMS/GeneralAssembly/PageRaees.ascx" TagPrefix="uc1" TagName="PageRaees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageRaees runat="server" ID="PageRaees" />
</asp:Content>

