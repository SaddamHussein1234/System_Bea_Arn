<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryEducationalSituations.aspx.cs" Inherits="Cpanel_PageBeneficiaryEducationalSituations" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
            document.getElementById("<%=btnGetByType.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style>
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
                Width: 48%;
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
    </style>

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
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="PageBeneficiaryEducationalSituations.aspx">إحصائية حسب الحالات التعليمية</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrintAll" runat="server" Direction="RightToLeft">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="إحصائية الحالات التعليمية"></asp:Label>
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip"
                                title="طباعة" OnClick="btnPrint_Click">
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
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <hr style="border: solid; border-width: 1px" />
                                            </div>
                                            <div class="col-md-5 HideThis">
                                                <asp:Literal ID="IDChart" runat="server"></asp:Literal>
                                            </div>
                                            <div class="col-md-7">
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
                                                        <td class="StyleTD">إجمالي عدد الطلاب
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberStudentMale" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">إجمالي عدد الطالبات
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberStudentFeMale" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">إجمالي جميع الطلاب
                                                        </td>
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
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div class="WidthMaglis24" align="center" runat="server" visible="false">
                                                                الباحث الإجتماعي
                                                                <br />
                                                                <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblAlBaheth" runat="server" Font-Size="11px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                مدير الجمعية
                                                                <br />
                                                                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                لحنة البحث الاجتماعي
                                                                <br />
                                                                <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Font-Size="20px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                رئيس مجلس الإدارة
                                                                <br />
                                                                <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center">
                                                                <div runat="server" id="IDKhatm" style="margin-top: 0px">
                                                                    <img src="../ImgSystem/ImgSignature/الختم.png" width="120" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
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
                            <asp:Label ID="lbmsg2" runat="server" Text="إحصائية الحالات التعليمية حسب القرية"></asp:Label>
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
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <asp:TextBox ID="txtTitleByQariah" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <hr style="border: solid; border-width: 1px; width: 100%" />
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
                                                        <td class="StyleTD">إجمالي عدد الطلاب
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberStudentMaleByQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD">إجمالي عدد الطالبات
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:Label ID="lblNumberStudentFeMaleQariah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">إجمالي عدد الطلاب
                                                        </td>
                                                        <td class="StyleTD" style="font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">
                                                            <asp:Label ID="lblSumByQriah" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="HideEdarah">
                                            <div class="container-fluid" dir="rtl" runat="server">
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div class="WidthMaglis24" align="center" runat="server" visible="false">
                                                                الباحث الإجتماعي
                                                                <br />
                                                                <asp:Image ID="Image1" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="11px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                مدير الجمعية
                                                                <br />
                                                                <asp:Image ID="ImgModerByQariah" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblModerAlGmeiahbyQariah" runat="server" Font-Size="20px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                لحنة البحث الاجتماعي
                                                                <br />
                                                                <asp:Image ID="ImgRaeesLagnatAlBahathByQariah" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblRaeesLagnatAlBahathByQariah" runat="server" Font-Size="20px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                رئيس مجلس الإدارة
                                                                <br />
                                                                <asp:Image ID="ImgRaeesMaglesAlEdarahByQariah" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblRaeesMaglesAlEdarahByQariah" runat="server" Font-Size="20px"></asp:Label>
                                                            </div>
                                                            <div class="WidthMaglis24" align="center">
                                                                <div runat="server" id="Div1" style="margin-top: 0px">
                                                                    <img src="../ImgSystem/ImgSignature/الختم.png" width="120" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
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
                            <asp:Label ID="Label2" runat="server" Text="بيانات الحالات التعليمية"></asp:Label>
                        </h3>
                        <div style="float: left">
                            ( 
                            <asp:RadioButton ID="RBMale" runat="server" Text=" الطلاب " GroupName="FG" />
                            <asp:RadioButton ID="RBFeMale" runat="server" Text=" الطالبات " GroupName="FG" />
                            ) 
                            القرية : 
                            <asp:DropDownList ID="DLAlQriahByData" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblAlQriahByData" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:Button ID="btnGetByType" runat="server" Text="بحث" Style="margin-right: 4px;"
                                class="btn btn-info btn-fill" ValidationGroup="g2" OnClick="btnGetByType_Click" />
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
                                                                    <asp:TextBox ID="txtSearchOrphans" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
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
                                                                    <asp:TemplateField HeaderStyle-Width="10px" Visible="false">
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
                                                                    <asp:BoundField DataField="NumberMostafed" HeaderText="ملف" SortExpression="NumberMostafed"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:TemplateField HeaderText="ولي الامر" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# Eval("NameMostafeed")%>
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
                                                                    <asp:BoundField DataField="AlMehnahAlHaliah" HeaderText="الحالة التعليمية" SortExpression="AlMehnahAlHaliah"
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
                                                                            <%# ClassSaddam.FCheckNullDateMostafeed((String) (ClassDataAccess.FChangeF((DateTime) Eval("DateBrith"))))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <%# ClassSaddam.FCheckNullDateMostafeedAge((DateTime) (Eval("DateBrith"))) %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="AlMehnahAlHaliah" HeaderText="المهنة الحالية" SortExpression="AlMehnahAlHaliah" Visible="false"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:BoundField DataField="AlmostawaAlDerasy" HeaderText="المستوى الدراسي" SortExpression="AlmostawaAlDerasy"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
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
                                                            <br />
                                                            <div class="HideEdarah">
                                                                <div class="container-fluid" dir="rtl" runat="server">
                                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="WidthMaglis24" align="center" runat="server" visible="false">
                                                                                    الباحث الإجتماعي
                                                                                    <br />
                                                                                    <asp:Image ID="Image2" runat="server" Width='100px' Height='25' />
                                                                                    <br />
                                                                                    <asp:Label ID="Label8" runat="server" Font-Size="11px"></asp:Label>
                                                                                    <asp:DropDownList ID="DropDownList1" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true"
                                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="WidthMaglis24" align="center">
                                                                                    مدير الجمعية
                                                                                    <br />
                                                                                    <asp:Image ID="ImgModerByAll" runat="server" Width='100px' Height='25' />
                                                                                    <br />
                                                                                    <asp:Label ID="lblModerAlGmeiahbyAll" runat="server" Font-Size="11px"></asp:Label>
                                                                                </div>
                                                                                <div class="WidthMaglis24" align="center">
                                                                                    لحنة البحث الاجتماعي
                                                                                    <br />
                                                                                    <asp:Image ID="ImgRaeesLagnatAlBahathByAll" runat="server" Width='100px' Height='25' />
                                                                                    <br />
                                                                                    <asp:Label ID="lblRaeesLagnatAlBahathByAll" runat="server" Font-Size="11px"></asp:Label>
                                                                                </div>
                                                                                <div class="WidthMaglis24" align="center">
                                                                                    رئيس مجلس الإدارة
                                                                                    <br />
                                                                                    <asp:Image ID="ImgRaeesMaglesAlEdarahByAll" runat="server" Width='100px' Height='25' />
                                                                                    <br />
                                                                                    <asp:Label ID="lblRaeesMaglesAlEdarahByAll" runat="server" Font-Size="11px"></asp:Label>
                                                                                </div>
                                                                                <div class="WidthMaglis24" align="center">
                                                                                    <div runat="server" id="Div2" style="margin-top: 0px">
                                                                                        <img src="../ImgSystem/ImgSignature/الختم.png" width="100" />
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th>
                                                            <div style="float: right">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                            <div style="float: left; display: none">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد القرى بحسب الايتام : </span>
                                                                <asp:Label ID="lblCountAitam" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                                            <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
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
                                            <h3 style="font-size: 20px">حدد البيــانــات
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
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

