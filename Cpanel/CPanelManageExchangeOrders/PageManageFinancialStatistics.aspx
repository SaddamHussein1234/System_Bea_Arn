<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageFinancialStatistics.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageFinancialStatistics" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageFinancialStatistics.ascx" TagPrefix="uc1" TagName="PageManageFinancialStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        
        <uc1:PageManageFinancialStatistics runat="server" ID="PageManageFinancialStatistics" />

        <script type="text/javascript"><!--
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
    //--></script>
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>