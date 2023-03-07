<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageAffiliation.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_PageManageAffiliation" %>

<%@ Register Src="~/Shaerd/CPanelManageWarehouse/PageManageAffiliation.ascx" TagPrefix="uc1" TagName="PageManageAffiliation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
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
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <uc1:PageManageAffiliation runat="server" ID="PageManageAffiliation" />
</asp:Content>

