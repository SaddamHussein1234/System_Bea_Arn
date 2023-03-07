<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSupportForPrismsEdit.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageSupportForPrismsEdit" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageSupportForPrismsEdit.ascx" TagPrefix="uc1" TagName="PageSupportForPrismsEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<script type="text/javascript">
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
        
        <uc1:PageSupportForPrismsEdit runat="server" ID="PageSupportForPrismsEdit" />

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

