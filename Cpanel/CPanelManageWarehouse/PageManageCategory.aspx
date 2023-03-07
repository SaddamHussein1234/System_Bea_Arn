<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageCategory.aspx.cs" Inherits="Cpanel_CPanelManageWarehouse_PageManageCategory" %>

<%@ Register Src="~/Shaerd/CPanelManageWarehouse/PageManageCategory.ascx" TagPrefix="uc1" TagName="PageManageCategory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <style>
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
        }

    </style>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <uc1:PageManageCategory runat="server" ID="PageManageCategory" />
        <br />
        <br />
        <br />
</asp:Content>