<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageZakat/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_CPanelManageZakat_Default" %>

<%@ Register Src="~/Shaerd/CPanelManageZakat/Default.ascx" TagPrefix="uc1" TagName="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Default runat="server" ID="Default" />
</asp:Content>

