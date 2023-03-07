<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Count/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStatisticFiles.aspx.cs" Inherits="Cpanel_Count_PageStatisticFiles" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.WSM" %>
<%@ Import Namespace="Library_CLS_Arn.WSM.Repostry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
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
        }

        @font-face {
            font-family: 'Alwatan';
            font-size: 18px;
            src: url(/fonts/AlWatanHeadlines-Bold.ttf);
        }

        @media screen and (min-width: 768px) {
            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .Width10Percint {
                float: right;
                Width: 15%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }

            .Width10Percint {
                Width: 95%;
            }
        }
    </style>
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة"><i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="">قائمة الإحصاء العام للمالفات المؤرشفة</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة الإحصاء العام للمالفات المؤرشفة
                        </h3>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="IDFilter" runat="server" ScrollBars="Auto" Height="250">
                            <div class="col-sm-12">
                                <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                    <span class="badge badge-pill badge-warning">تحذير</span>
                                    <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                    <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                            </div>
                            <div class="col-sm-3 hide">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> سنوات الإرشيف : </h5>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBYears" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> المستخدمين : </h5>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBAdmin" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> الأنظمة الفعلية : </h5>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBAccount" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem Value="111">نظام البحث الاجتماعي</asp:ListItem>
                                            <asp:ListItem Value="222">نظام المستودع</asp:ListItem>
                                            <asp:ListItem Value="333">نظام أوامر الصرف</asp:ListItem>
                                            <asp:ListItem Value="444">نظام الزكاة</asp:ListItem>
                                            <asp:ListItem Value="555">نظام الجمعية العمومية</asp:ListItem>
                                            <asp:ListItem Value="666">نظام الموارد البشرية</asp:ListItem>
                                            <asp:ListItem Value="777">نظام الاستثمار وتنمية الموارد</asp:ListItem>
                                            <asp:ListItem Value="888">نظام إدارة الجمعية</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> من تاريخ : </h5>
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
                                        <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtDateFrom" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> إلى تاريخ : </h5>
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
                                        <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtDateTo" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <div class="form-group">
                                    <asp:Button ID="btnGet" runat="server" Text="بحث حسب الفلترة" Style="margin-right: 4px;"
                                        CssClass="btn btn-info btn-fill " ValidationGroup="VGDetails" OnClick="btnGet_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <div class="table table-responsive" runat="server" id="pnlDataPrint" dir="rtl" visible="false">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                            <div class="hide">
                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                            </div>
                            <div align="center" class="w">
                                <div align="center" class="w">
                                    <div class="col-lg-11">
                                        <asp:TextBox ID="txtTitleReceipt" runat="server" class="form-control" Text="قائمة الحساب" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 HideThis">
                                        <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                <thead>
                                    <tr class="th">
                                        <th class="StyleTD" style="width:10%;">م
                                        </th>
                                        <th class="StyleTD" style="width:70%;">النظام
                                        </th>
                                        <th class="StyleTD" style="width:20%;">عدد الملفات
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">1</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                                نظام البحث الاجتماعي
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumSSM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">2</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                                نظام المستودع
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumWSM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">3</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                            نظام أوامر الصرف
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumEOS" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">4</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                            نظام الزكاة
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumZSM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">5</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                            نظام الجمعية العمومية
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumGAM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">6</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                            نظام الموارد البشرية
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumHRM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">7</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                            نظام الاستثمار وتنمية الموارد
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumCRM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">8</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 12px">
                                            نظام إدارة الجمعية
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumOM" runat="server" 
                                                Text=''></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th class="StyleTD">
                                            - 
                                        </th>
                                        <th class="StyleTD">
                                            إجمالي عدد الملفات المؤرشفة
                                        </th>
                                        <th class="StyleTD">
                                            <asp:Label ID="lbl_SumAll" runat="server" Text="0"></asp:Label>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="hide">
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="container-fluid" dir="rtl" runat="server">
                                    <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="HideNow">
                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                </div>
                            </div>
                            </asp:Panel>
                        </div>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <hr />
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
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>
