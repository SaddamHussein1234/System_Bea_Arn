<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageVisitReportByDevice.aspx.cs" Inherits="Cpanel_PageVisitReportByDevice" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>


<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="css/chosen.css" rel="stylesheet" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGetByType.ClientID %>").disabled = true;
            document.getElementById("<%=btnSearch.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <style type="text/css">
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
                Width: 16%;
                padding-right: 5px;
            }

            .Width10Percint {
                float: right;
                Width: 10%;
                padding-right: 5px;
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

            .Width10Percint {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }
        }

        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .StyleTextBox {
            border-width: 2px;
            border-color: #03a7cc;
            border-radius: 4px;
            padding-right: 2px;
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

        @media print {
            thead {
                display: table-header-group;
            }
        }

        .HideNow {
            display: none;
        }
    </style>
    <script src="../view/Chart/fusioncharts.js"></script>
    <script src="../view/Chart/fusioncharts.charts.js"></script>

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
            var gv = document.getElementById("<%=GVVisitReport.ClientID%>");
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
                return confirm(" هل تريد الإستمرار ؟");
            }
        }
    </script>
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">

                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageVisitReportByDevice.aspx">قائمة إحتياجات المستفيدين حسب الأجهزة الكهربائية </a></li>
                    </ul>
                </div>
            </div>
            <div id="PnlChart" class="container-fluid" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="Label5" runat="server" Text="إحصائية حسب إحتياجات الأجهزة الكهربائية"></asp:Label>
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnGetByType" runat="server" Text="بحث" Style="margin: 0 5px 0 4px;" OnClick="btnGetByType_Click"
                                class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                            <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="تحديث" OnClick="LBExit_Click"
                                class="btn btn-default"> <i class="fa fa-refresh"></i></asp:LinkButton>
                            <asp:LinkButton ID="LBPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LBPrint_Click"
                                title="طباعة" Style="margin-left: 5px">
                            <i class="fa fa-print"></i></asp:LinkButton>
                        </div>                       
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="col-md-10">
                                    <div class="form-group">
                                        <asp:CheckBoxList ID="CBLType" runat="server" Font-Size="12px" RepeatDirection="Horizontal">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
                                        <div class="form-group">
                                            <div class="HideEdarah">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader1" />
                                            </div>
                                            <div>
                                                <hr style="border: solid; border-width: 1px; width: 100%" />
                                                <asp:TextBox ID="txtSearchStatistic" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                            </div>
                                            <div class="col-md-5 HideThis">
                                                <asp:Literal ID="IDChart" runat="server"></asp:Literal>
                                            </div>
                                            <div class="col-md-7" runat="server" id="reportGrid">
                                                <table style="width: 100%;">
                                                    <tr style="margin: 5px">
                                                        <td class="StyleTD" style="width: 40%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">البيان
                                                        </td>
                                                        <td class="StyleTD" style="width: 30%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">عدد الأجهزة 
                                                        </td>
                                                    </tr>
                                                    <asp:Label ID="lblvaluesType" runat="server"></asp:Label>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="HideEdarah">
                                            <div class="container-fluid" dir="rtl" runat="server">
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM1" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSelectByCheck" runat="server" Visible="False">
                                        <div class="form-group">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <div align="center">
                                                <h3 style="font-size: 20px">حدد البيانات
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
            <div class="container-fluid" runat="server" id="IDDataDevice">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة إحتياجات المستفيدين حسب الأجهزة الكهربائية
                        </h3>
                        <div class="pull-right">
                            <asp:LinkButton ID="btnHide" runat="server" class="btn btn-warning" data-toggle="tooltip" Style="margin-right: 5px" OnClick="btnHide_Click"
                                title="إخفاء المحدد">
                                <i class="fa fa-eye-slash"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                                title="طباعة">
                                <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                                title="تحديث" OnClick="btnRefrish_Click">
                                <i class="fa fa-refresh"></i></asp:LinkButton>
                        </div>

                        <div style="float: left">
                            <asp:Label ID="Label2" runat="server" Text=" إسم المستفيد"></asp:Label>
                            <asp:TextBox ID="txtNameMostafeed" runat="server" class="form-control2" ValidationGroup="g2" Width="150px" Height="30px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div style="float: right; width: 140px">
                            <asp:Label ID="lbmsg" runat="server" Text=" القرية"></asp:Label>
                            <asp:DropDownList ID="DLAlQriah" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                Width="90px" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; width: 140px">
                            <asp:Label ID="Label1" runat="server" Text="الحالة : "></asp:Label>
                            <asp:DropDownList ID="DLHalafAlMosTafeed" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                Width="90px" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; width: 140px">
                            <asp:Label ID="Label4" runat="server" Text="الجهاز : "></asp:Label>
                            <asp:DropDownList ID="DlVevice" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                Width="70px" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: right; width: 140px">
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 90px"></asp:TextBox>
                                    <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div style="float: right; width: 140px">
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 90px"></asp:TextBox>
                                    <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div style="float: right; width: 160px">
                            <asp:Label ID="Label3" runat="server" Text=" نسبة الإحتياج"></asp:Label>
                            <asp:DropDownList ID="DLPercint" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                Width="60px" Height="30px">
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="5">*****</asp:ListItem>
                                <asp:ListItem Value="4">****</asp:ListItem>
                                <asp:ListItem Value="3">***</asp:ListItem>
                                <asp:ListItem Value="2">**</asp:ListItem>
                                <asp:ListItem Value="1">*</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" ValidationGroup="g2"
                            class="btn btn-info btn-fill" OnClick="btnSearch_Click" />
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="false">
                            <div class="table table-responsive">
                                <div>
                                    <div class="HideNow">
                                        <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                    </div>
                                </div>
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVVisitReport" runat="server" AutoGenerateColumns="False" DataKeyNames="IDMustafeed"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    UseAccessibleHeader="False" AllowPaging="false" ShowInsertButton="true" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="رقم الملف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("IDMustafeed")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 11px">
                                                                    <%# Eval("NameMostafeed")%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlQarabah((Int32) (Eval("AlQaryah")))%>
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
                                                        <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FHalatMostafeed((Int32) (Eval("HalafAlMosTafeed")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الأجهزة المطلوبة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# FGetDevice(Convert.ToInt32(Eval("IDMustafeed")),Convert.ToInt32(Eval("IDReport")),DlVevice.SelectedValue) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="رقم تقرير الزيارة الميدانية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("IDReport")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="تاريخ التقرير" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                            <ItemTemplate>
                                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("DateReport")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="أُضيف من قبل" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href='PageVisitReportDetails.aspx?ID=<%# Eval("IDReport")%>&XID=<%# Convert.ToString(Guid.NewGuid())%>' title="عرض الملف" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                                <br />
                                                                <a href='PageVisitReportEdit.aspx?ID=<%# Eval("IDReport")%>&XID=<%# Convert.ToString(Guid.NewGuid())%>' title="تعديل" data-toggle="tooltip"
                                                                    style="margin-top: 5px"><span class="fa fa-edit"></span></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="container-fluid" dir="rtl" runat="server">
                                                    <div class="WidthText4" style="text-align: right">
                                                        <h5>إجمالي إحتياجات المستفيدين حسب الأجهزة الكهربائية 
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="container-fluid" dir="rtl" runat="server">
                                                    <asp:Repeater ID="RPTDeviceAll" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceAll((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByQariah" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByQariah((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByHalatMostafeed" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByHalatMostafeed((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByQariahAndHalatMostafeed" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByQariahAndHalatMostafeed((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByDevice" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByDevice((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByQariahAndDevice" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByQariahAndDevice((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByHalatMostafeedAndDevice" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByHalatMostafeedAndDevice((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="RPTDeviceByAlQariahAndHalatMostafeedAndDevice" runat="server" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckNullDevice(Convert.ToString(Eval("ProductName")), Convert.ToInt32(FGetCountDeviceByAlQariahAndHalatMostafeedAndDevice((Int64) (Eval("ProductID"))))) %>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <span style="font-size: 12px; padding-right: 5px">عدد الأسر : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
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
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <br />
                            <br />
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
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">يرجى إدخال جملة البحث
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
    //-->
        </script>
</asp:Content>

