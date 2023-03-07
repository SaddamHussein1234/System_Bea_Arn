<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductRestorationAndConstructionEdit.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageProductRestorationAndConstructionEdit" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductRestorationAndConstructionEdit.ascx" TagPrefix="uc1" TagName="PageManageProductRestorationAndConstructionEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnEdit.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>

    
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        
        <uc1:PageManageProductRestorationAndConstructionEdit runat="server" ID="PageManageProductRestorationAndConstructionEdit" />

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

