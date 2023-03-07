<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryOrphans.aspx.cs" Inherits="Cpanel_PageBeneficiaryOrphans" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>


<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
            document.getElementById("<%=btnGetByType.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>

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
                Width: 49%;
            }

            .WidthText5 {
                float: right;
                Width: 100%;
            }

            .WidthText20 {
                Width: 150px;
                height: 36px;
            }

            .WidthText30 {
                float: right;
                Width: 16%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText30 {
                Width: 95%;
            }

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

        @media screen and (min-width: 768px) {
            .WidthMaglis {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }

            .WidthMaglis24 {
                Width: 95%;
            }
        }

        .HideEdarah {
            display: none;
        }

        .redFont{
            color:red;
        }
    </style>

    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVOrphansAll.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي سجل");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>

    <script type="text/javascript">
        function PrintTextbox() {
            var text_box = document.getElementById("<%=pnlPrint.ClientID %>");
            var printWindow = window.open('', '', 'height=900,width=1000');
            printWindow.document.write('<html><head><title>النظام الإلكتروني</title><link href="../view/StylePrint.css" rel="stylesheet" />');
            printWindow.document.write('<link href="../fonts/font-awesome.css" rel="stylesheet" /><link href="../view/stylesheet/bootstrap-a.css" rel="stylesheet" /></head><body >');
            printWindow.document.write(text_box.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    
    <script src="../view/Chart/fusioncharts.js"></script>
    <script src="../view/Chart/fusioncharts.charts.js"></script>

    <link href="css/chosen.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    ذكور : 
                       <asp:TextBox ID="txtAgeBoy" Width="50" runat="server" class="form-control2" ValidationGroup="GAge" ></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                           ControlToValidate="txtAgeBoy" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                        ValidationGroup="GAge" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                    إناث : 
                       <asp:TextBox ID="txtAgeGirls" Width="50" runat="server" class="form-control2" ValidationGroup="GAge" ></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                           ControlToValidate="txtAgeGirls" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                        ValidationGroup="GAge" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:LinkButton ID="LBEditAge" runat="server" class="btn btn-info" data-toggle="tooltip"
                             title="تحديث العمر" Style="margin-left: 5px"  ValidationGroup="GAge" OnClick="LBEditAge_Click">
                       <i class="fa fa-edit"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="PageBeneficiaryOrphans.aspx">إحصائية حسب الأيتام</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrintAll" runat="server" Direction="RightToLeft">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="إحصائية جميع الايتام"></asp:Label>
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip"
                                title="طباعة" Style="margin-left: 5px" OnClick="btnPrint_Click">
                            <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:Button ID="btnPrintTow" runat="server" Text="طباعة الإحصاء" title="طباعة"
                                class="btn btn-success" data-toggle="tooltip" OnClientClick="PrintTextbox();" />
                            <asp:LinkButton ID="LBR" runat="server" data-toggle="tooltip" title="تحديث" OnClick="LBR_Click"
                                class="btn btn-default"> <i class="fa fa-refresh"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
                                        <div class="form-group">
                                            <div class="HideEdarah">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                            </div>
                                            <div>
                                                <hr style='border: solid; border-width: 1px; width:100%' />
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <hr style="border: solid; border-width: 1px" />
                                            </div>
                                            <div class="col-lg-5 HideThis">
                                                <asp:Literal ID="IDChart" runat="server"></asp:Literal>
                                            </div>
                                            <div class="col-lg-7">
                                                <table style="width: 100%;">
                                                    <tr style="margin: 5px">
                                                        <td class="StyleTD ColorBackGround" style="width: 60%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">البيان
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">العدد 
                                                        </td>
                                                    </tr>
                                                    <tr style="margin: 5px">
                                                        <td class="StyleTD">إجمالي عدد الأسر
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberOser" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">إجمالي عدد الأيتام
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberAitam" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">عدد الأيتام الذكور
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberAitamByMale" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">عدد الأيتام الإناث
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberAitamByFeMale" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">عدد القرى بحسب الأيتام
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberQriahByFeMale" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;"></td>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">
                                                            <asp:Label ID="lblSum" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="HideEdarah">
                                            <div class="container-fluid" dir="rtl" runat="server">
                                                <hr style='border: solid; border-width: 1px; width:100%' />
                                                <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM2" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlByQariah" runat="server" Direction="RightToLeft">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="lbmsg2" runat="server" Text="إحصائية الايتام حسب القرية"></asp:Label>
                        </h3>
                        <div style="float: left">
                            القرية : 
                                        <asp:DropDownList ID="DLAlQriah" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                            <asp:Label ID="lblQriah" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;"
                                class="btn btn-info btn-fill" ValidationGroup="g2" OnClick="btnGet_Click" />
                            <asp:LinkButton ID="lbPrintByQariah" runat="server" class="btn btn-success" data-toggle="tooltip"
                                title="طباعة" OnClick="lbPrintByQariah_Click">
                            <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlPrintByQariah" runat="server" Direction="RightToLeft" Visible="false">
                                        <div class="form-group">
                                            <div class="HideEdarah">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader1" />
                                            </div>
                                            <div>
                                                <hr style='border: solid; border-width: 1px; width:100%' />
                                                <asp:TextBox ID="txtTitleByQariah" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <hr style="border: solid; border-width: 1px" />
                                            </div>
                                            <div class="col-md-5 HideThis">
                                                <asp:Literal ID="IDChartByQariah" runat="server"></asp:Literal>
                                            </div>
                                            <div class="col-md-7">
                                                <table style="width: 100%;">
                                                    <tr style="margin: 5px">
                                                        <td class="StyleTD" style="width: 60%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">البيان
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">العدد 
                                                        </td>
                                                    </tr>
                                                    <tr style="margin: 5px">
                                                        <td class="StyleTD">إجمالي عدد الأسر
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberOserByQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">إجمالي عدد الأيتام
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberAitamByQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">عدد الأيتام الذكور
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberAitamByMaleByQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">عدد الأيتام الإناث
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberAitamByFeMaleByQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;"></td>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">
                                                            <asp:Label ID="lblSumByQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="HideEdarah">
                                            <div class="container-fluid" dir="rtl" runat="server">
                                                <hr style='border: solid; border-width: 1px; width:100%' />
                                                <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM1" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                                        <div class="form-group">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <div align="center">
                                                <h3 style="font-size: 20px">حدد القرية
                                                </h3>
                                            </div>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="Label2" runat="server" Text="بيانات الايتام"></asp:Label>
                        </h3>
                        <div style="float: left">
                            نوع المستفيد : 
                            <asp:DropDownList ID="DLTypeMostafeed" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                <asp:ListItem Value="دائم" Selected="True">دائم</asp:ListItem>
                                <asp:ListItem Value="مستبعد">مستبعد</asp:ListItem>
                            </asp:DropDownList>
                            القرية : 
                            <asp:DropDownList ID="DLAlQriahByData" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnGetByType" runat="server" Text="جلب بيانات الايتام" Style="margin-right: 4px;"
                                                    class="btn btn-info btn-fill" OnClick="btnGetByType_Click" />
                            <asp:LinkButton ID="btnHide" runat="server" class="btn btn-warning" data-toggle="tooltip" Style="margin-right: 5px" 
                                OnClientClick="return ConfirmDelete();" OnClick="btnHide_Click"
                                    title="إخفاء المحدد"><i class="fa fa-eye-slash"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnView" runat="server" class="btn btn-info" data-toggle="tooltip" Style="margin-right: 5px" 
                                OnClientClick="return ConfirmDelete();" OnClick="btnView_Click"
                                    title="إظهار المحدد"><i class="fa fa-eye"></i></asp:LinkButton>
                            <asp:LinkButton ID="LBPrintAll" runat="server" class="btn btn-success" data-toggle="tooltip"
                                title="طباعة" OnClick="LBPrintAll_Click">
                            <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:LinkButton ID="LBReafrchAll" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBReafrchAll_Click"
                                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlPrintAllData" runat="server" Direction="RightToLeft" Visible="False">
                                        <div class="table table-responsive">
                                            <table class='table' style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <div class="HideNow">
                                                                <uc1:WUCHeader runat="server" ID="WUCHeader3" />
                                                            </div>
                                                            <div align="center" class="w">
                                                                <div>
                                                                    <asp:TextBox ID="txtSearchOrphans" runat="server" Text="0" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%;; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GVOrphansAll" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                                UseAccessibleHeader="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="10px">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="IDItem" HeaderText="IDItem" InsertVisible="False" ReadOnly="True"
                                                                        SortExpression="IDItem" Visible="false" />
                                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="NumberMostafeed" HeaderText="ملف" SortExpression="NumberMostafeed"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:TemplateField HeaderText="ولي الامر" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <span class='<%# Convert.ToBoolean(Eval("_Is_Print_Hide_"))?"redFont":"" %>' style="margin-bottom:3px; font-size:11px;"><%# Eval("NameMostafeed") %></span>
                                                                            <%# FCheckHide((bool) (Eval("_Is_Print_Hide_"))) %> 
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الهاتف" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            0<%# Eval("PhoneNumber")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Name" HeaderText="الإسم" SortExpression="Name"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:TemplateField HeaderText="القرابة" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# ClassQuaem.FQarabah((Int32) Eval("AlQarabah"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="تاريخ الميلاد" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# ClassDataAccess.FChangeF((DateTime) Eval("DateBrith"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# ClassSaddam.FGetAge((DateTime) (Eval("DateBrith")))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="AlMehnahAlHaliah" HeaderText="المهنة الحالية" SortExpression="AlMehnahAlHaliah" Visible="false"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:TemplateField HeaderText="المستوى الدراسي" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# ClassSaddam.FCheckAlmostawaAlDerasy(Convert.ToString(Eval("AlmostawaAlDerasy")))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الحساب البنكي" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <a href='PageBeneficiaryOrphansIDBank.aspx?ID=<%# Eval("NumberMostafed")%>&XID=<%# Eval("IDUniq")%>' title="إضافة الحساب" data-toggle="tooltip">
                                                                                <span style="font-size:13px"> <%# Eval("A4")%> </span>
                                                                            </a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="AlHalahAlseHeyah" HeaderText="الحالة الصحية" SortExpression="AlHalahAlseHeyah" Visible="false"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:TemplateField HeaderText="من قبل" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16" Visible="false">
                                                                        <ItemTemplate>
                                                                            <a href='PageBeneficiaryAddBoys.aspx?ID=<%# Eval("NumberMostafed")%>&XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                                                class="btn btn-primary"><span class="fa fa-edit"></span></a>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                                <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي "
                                                                    PreviousPageText=" السابق - " />
                                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                                <RowStyle CssClass="rows"></RowStyle>
                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                <%--<SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                                                            </asp:GridView>
                                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                            <br />
                                                            <div class="HideEdarah">
                                                                
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th>
                                                            <hr style='border: solid; border-width: 1px; width:100%' />
                                                            <div style="float:right">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد الأيتام : </span>
                                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                            <div style="float:left">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد القُرى بحسب الايتام : </span>
                                                                <asp:Label ID="lblCountAitam" runat="server"  Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                            <div align="center">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد الأسر بحسب الأيتام : </span>
                                                                <asp:Label ID="lblCountAoser" runat="server"  Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                            <div class="container-fluid" dir="rtl" runat="server">
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لا توجد نتائج
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlWaiting" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px"> الرجاء تحديد البيانات 
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="modal fade" id="NotBuy" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" dir="rtl">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <br />
                        <h4 class="modal-title" align="right">لم تسدد بعد !</h4>
                    </div>
                    <div class="modal-body">
                        <h5 align="right">لقد قمت فعلاً بطلب منتج ولاكن لم تقم بالتسديد . <span style="font-size: 18px" class="fa fa-spin fa-warning"></span>
                        </h5>
                        <br />
                        <p align="left">
                            <a href="DetailsBuy" class="btn btn-primary">تسديد الآن !</a>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" align="left">خروج</button>
                    </div>
                </div>
            </div>
        </div>

        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

