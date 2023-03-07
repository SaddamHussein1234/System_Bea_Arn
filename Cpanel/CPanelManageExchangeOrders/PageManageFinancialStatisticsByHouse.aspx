<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageFinancialStatisticsByHouse.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageFinancialStatisticsByHouse" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageFinancialStatisticsByHouse.ascx" TagPrefix="uc1" TagName="PageManageFinancialStatisticsByHouse" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        
        <uc1:PageManageFinancialStatisticsByHouse runat="server" id="PageManageFinancialStatisticsByHouse" />

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

