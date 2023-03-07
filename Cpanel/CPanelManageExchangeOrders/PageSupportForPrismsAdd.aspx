﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSupportForPrismsAdd.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageSupportForPrismsAdd" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageSupportForPrismsAdd.ascx" TagPrefix="uc1" TagName="PageSupportForPrismsAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
    <uc1:PageSupportForPrismsAdd runat="server" ID="PageSupportForPrismsAdd" />

        <script type="text/javascript">
            $('.date').datetimepicker({
                pickTime: false
            });

            $('.time').datetimepicker({
                pickDate: false
            });

            $('.datetime').datetimepicker({
                pickDate: true,
                pickTime: true
            });
        </script>
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

