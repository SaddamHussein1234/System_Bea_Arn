<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAcceptanceDecisionDetails.aspx.cs" Inherits="Cpanel_PageAcceptanceDecisionDetails" %>

<%@ Register Src="~/Shaerd/PageAcceptanceDecisionDetails.ascx" TagPrefix="uc1" TagName="PageAcceptanceDecisionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText30 {
                float: right;
                Width: 16%;
                padding-right: 5px;
            }

            .WidthText2 {
                float: right;
                Width: 32%;
                padding-left: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }

            .WidthText5 {
                float: right;
                Width: 100%;
            }

            .WidthText20 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText2 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText30 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }

            .WidthText5 {
                Width: 95%;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
    <link href="css/chosen.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageAcceptanceDecisionDetails runat="server" ID="PageAcceptanceDecisionDetails" />
</asp:Content>

