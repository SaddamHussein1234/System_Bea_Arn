<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductWarehouseApprovalOfTheDirector.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_PageManageProductWarehouseApprovalOfTheDirector" %>

<%@ Register Src="~/Shaerd/CPanelManageWarehouse/PageManageProductWarehouseApprovalOfTheDirector.ascx" TagPrefix="uc1" TagName="PageManageProductWarehouseApprovalOfTheDirector" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />
    

    <%--<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAllow.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        
        <uc1:PageManageProductWarehouseApprovalOfTheDirector runat="server" ID="PageManageProductWarehouseApprovalOfTheDirector" />

        <br />
        <br />
        <br />
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

