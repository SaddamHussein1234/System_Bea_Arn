<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageFinancialStatisticsByDamaged.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_PageManageFinancialStatisticsByDamaged" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>



<link href="../GridView.css" rel="stylesheet" type="text/css" />
<link href="../css/chosen.css" rel="stylesheet" />
<%--<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>
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
</style>

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="" data-toggle="tooltip" title="إضافة مستفيد" class="btn btn-primary" style="display: none"><i class="fa fa-plus"></i></a>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" Visible="false"
                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
        </div>
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الرئيسية</a></li>
                <li><a href="">قائمة الإحصاء المالي للتالف</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i>قائمة الإحصاء المالي للتالف
                </h3>
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <div style="float: right; width: 230px">
                        <asp:Label ID="lbmsg" runat="server" Text="فرز أوامر"></asp:Label>
                        لــ :
                                <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                    Width="150px" Height="30px" Enabled="false">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">أمر صرف لمستفيد</asp:ListItem>
                                    <asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">تالف</asp:ListItem>
                                </asp:DropDownList>
                        <asp:Label ID="lblType" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                    <div style="float: right; width: 150px">
                        <div class="col-sm-3">
                            <div class="input-group date " style="margin-right: -10px;">
                                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div style="float: right; width: 150px">
                        <div class="col-sm-3">
                            <div class="input-group date " style="margin-right: -10px;">
                                <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;"
                        class="btn btn-info btn-fill " ValidationGroup="g2" OnClick="btnGet_Click" />
                </div>
                <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                    <div class="table table-responsive">
                        <table class='table' style="width: 100%">
                            <thead>
                                <tr>
                                    <th colspan="2">
                                        <div class="HideNow">
                                            <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                        </div>
                                        <div align="center" class="w">
                                            <div>
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="margin: 5px">
                                    <td class="StyleTD ColorBackGround" style="width: 60%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">البيان
                                    </td>
                                    <td class="StyleTD" style="width: 40%; font-size: 15px; background-color: #4b4b4b; color: #bababa; font-family: 'Alwatan'; font-size: 18px;">المبلغ 
                                    </td>
                                </tr>
                                <tr style="margin: 5px">
                                    <td class="StyleTD">إجمالي التالف
                                    </td>
                                    <td class="StyleTD">
                                        <asp:Label ID="lblSumTalef" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="2">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                                                </td>
                                                <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                                    <asp:TextBox ID="lblSumWord" runat="server" Text="0" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </td>
                                                <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                                    <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                        <div class="hide">
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <div class="container-fluid" dir="rtl" runat="server">
                                <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM" />
                            </div>
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
                <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                    <br />
                    <br />
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
