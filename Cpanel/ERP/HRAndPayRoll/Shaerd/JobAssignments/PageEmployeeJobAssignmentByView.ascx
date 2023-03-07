<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeJobAssignmentByView.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentByView" %>
<style type="text/css">
.StyleTD {
    text-align: center;
    padding: 5px;
    border: double;
    border-width: 2px;
    border-color: #a1a0a0;
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

    .WidthText30 {
        float: right;
        Width: 16%;
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
        Width: 50%;
    }

    .Width10Percint {
        float: right;
        Width: 10%;
        padding-right: 5px;
    }

    .WidthText5 {
        float: right;
        Width: 100%;
    }


    .WidthText20 {
        Width: 150px;
        height: 36px;
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

    .WidthText2 {
        Width: 95%;
    }

    .WidthText3 {
        Width: 95%;
    }

    .Width10Percint {
        Width: 95%;
    }

    .WidthText30 {
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
</style>

<div class="page-header">
        <div class="container-fluid">
            <div class="pull-right">
                <span runat="server" id="IDSearch">
                    <a runat="server" id="IDEdit" href="#" data-toggle="tooltip" title="تعديل الملف" class="btn btn-info">وضع التعديل</a>
                    <label class="control-label">
                        الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                    </label>
                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2"
                        Width="100" ValidationGroup="GDetails">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم المهمة ... "></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                        class="btn btn-info" OnClick="btnSearch_Click" />
                </span>
                <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                    title="طباعة"><i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                    class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
            </div>
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الرئيسية</a></li>
                <li><a href="PageEmployeeJobAssignments.aspx">مهام الموظفين</a></li>
                <li><a href="">تفاصيل مهام عمل</a></li>
            </ul>
        </div>
    </div>
    <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
        <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
            <div class="container-fluid">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="مهمة عمل داخلي" Style="text-align: center; width: 95%; background-color: #08a81d; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <a href='javaScript:void(0)' data-toggle='modal' data-target='#IDQRCode' title='تكبير'>
                                            <asp:Image ID="ImgQRCode" runat="server" alt='QR Code' />
                                        </a>
                                        <div id="IDQRCode" class="modal fade in modal_New_Style HideThis">
                                            <div class="modal-dialog " style="max-width: 450px">
                                                <div class="modal-content">
                                                    <div class="modal-header no-border">
                                                        <button type="button" class="close" data-dismiss="modal">×</button>
                                                    </div>
                                                    <div class="modal-body" id="modal_ajax_content">
                                                        <div class="page-container">
                                                            <div class="page-content">
                                                                <div class=" panel-body">
                                                                    <label>
                                                                        <i class="fa fa-star"></i>QR Code : 
                                                                    </label>
                                                                    <br />
                                                                    <div align="center">
                                                                        <asp:Image ID="ImgQRCode2" runat="server" alt='صورة QRCode' Style="width: 300px; height: 300px;" />
                                                                    </div>
                                                                    <div class='clearfix'></div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-default" data-dismiss="modal">اغلاق</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="StyleTD" colspan="2">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم المهمة
                                                            </td>
                                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                <asp:Label ID="lblNumberAccountable" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ الطلب
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <asp:Label ID="lblDateCreate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="StyleTD" colspan="2">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">المشتركين بالمهمة
                                                            </td>
                                                            <td style="width: 75%;">
                                                                <asp:Repeater ID="RPTJobAssignment_Map" runat="server">
                                                                    <ItemTemplate>
                                                                        <span><%# Container.ItemIndex + 1 %> - <%# Eval("_Name") %><br /></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br /><br /><br />
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <strong>المكرم الموضف الموضح بياناته أعلاه .... تحيه طيبه وبعد نشعركم بانه تم تكليفكم بالمهمة / المهام التالية : 
                                                </strong>
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <%--<table class="table table-bordered table-condensed table-responsive" style="width: 100%">
                                                    <thead>
                                                        <tr>
                                                            <th>م</th>
                                                            <th>البيان</th>
                                                            <th>الحالة</th>
                                                            <th>العدد</th>
                                                            <th>ملاحظة</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>1</td>
                                                            <td>سيرفر ونطاق نظام الجمعية والمعهد</td>
                                                            <td>مكتمل</td>
                                                            <td>1</td>
                                                            <td>مدفوع الإشتراك لسنة 2025</td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td>سورس كود نظام إدارة الجمعية</td>
                                                            <td>مكتمل</td>
                                                            <td>13 نظام</td>
                                                            <td>تسليم وتشغيل بالكامل</td>
                                                        </tr>
                                                        <tr>
                                                            <td>3</td>
                                                            <td>حساب شركة مزود رسائل sms</td>
                                                            <td>مكتمل</td>
                                                            <td>1</td>
                                                            <td>تسليم الحساب</td>
                                                        </tr>
                                                        <tr>
                                                            <td>4</td>
                                                            <td>حساب لوحة تحكم المعهد</td>
                                                            <td>مكتمل</td>
                                                            <td>1</td>
                                                            <td>تسليم الحساب</td>
                                                        </tr>
                                                    </tbody>
                                                </table>--%>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td class="StyleTD" colspan="2">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                        <strong>المهمة </strong>
                                                                    </td>
                                                                    <td style="width: 75%;">
                                                                        <asp:Label ID="lblThe_Assignment" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" colspan="2">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                        <strong>تاريخ التكليف </strong>
                                                                    </td>
                                                                    <td style="width: 75%;">
                                                                        <asp:Label ID="lblDate_Job" runat="server"></asp:Label>
                                                                        م
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" colspan="2">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                        <strong>وقت التكليف </strong>
                                                                    </td>
                                                                    <td style="width: 75%;">
                                                                        <asp:Label ID="lblTime_Assignment" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" colspan="2">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                        <strong>المهام </strong>
                                                                    </td>
                                                                    <td style="width: 75%;">
                                                                        <asp:Label ID="lblThe_Qriah" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div align="center">
                                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                                <span style="font-family: 'Alwatan'; font-size: 20px;">الاعتماد الرسمي
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width: 50%;">
                                                            <%--<span style="font-family: 'Alwatan'; font-size: 20px;">الموظف
                                                            </span>
                                                            <br />
                                                            <asp:Image ID="Img_Emp" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lblEmp" runat="server"></asp:Label>--%>
                                                        </td>
                                                        <td align="center" style="width: 50%;">
                                                            <span style="font-family: 'Alwatan'; font-size: 20px;">
                                                                <asp:Label ID="lbl_Job" runat="server"></asp:Label>
                                                            </span>
                                                            <br />
                                                            <asp:Image ID="Img_Moder" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lblModer" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div runat="server" id="IDKhatm" align="left" style="margin-top: -20px" visible="true">
                                                    <img src="<%=ResolveUrl("~/ImgSystem/ImgSignature/الختم.png")%>" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                        <div class="WidthText5" style="display:none;">
                                            <div class="form-group">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="WidthText4">
                                                                <div class="form-group">
                                                                    <%--<strong>سعادة رئيس لجنة الشؤون المالية والإدارية 
                                                                    </strong>--%>
                                                                    <strong>سعادة رئيس مجلس الادارة 
                                                                    </strong>
                                                                </div>
                                                            </div>
                                                            <div class="WidthText2">
                                                                <div class="form-group">
                                                                    <strong>وفقه الله
                                                                    </strong>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="WidthText4">
                                                                <div class="form-group">
                                                                    تحية طيبة ...
                                                                </div>
                                                            </div>
                                                            <div class="WidthText2">
                                                                <div class="form-group">
                                                                    <strong>وبعد ,
                                                                    </strong>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="form-group">
                                                                نحيطكم علماً بأن ملاحظتنا حول المهمة المكلف بها الموظف الموضح بياناته  بعاليه كالتالي : -
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <div runat="server" id="pnlRaeesAllow" visible="false">
                                                                <i class="fa fa-check-square"></i>أنجزت المهمة بشكل كامل . 
                                                            </div>
                                                            <div runat="server" id="pnlRaeesWithComment" visible="false">
                                                                <i class="fa fa-check-square"></i>أنجزت المهمة مع الملاحظات التالية .
                                                            <br />
                                                                <i class="fa fa-minus"></i>
                                                                <asp:Label ID="lblRaees" runat="server"></asp:Label>
                                                            </div>
                                                            <div runat="server" id="pnlRaeesNotAllow" visible="false">
                                                                <i class="fa fa-check-square"></i>لم تنجز المهمة . 
                                                            </div>
                                                        </td>
                                                        <td align="center">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">رئيس مجلس الادارة</span>
                                                            <br />
                                                            <asp:Image ID="Img_Raees" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_Raees" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--<div runat="server" id="IDKhatm" align="left" style="margin-top: -120px" visible="false">
                                                    <img src="<%=ResolveUrl("~/ImgSystem/ImgSignature/الختم.png")%>" />
                                                </div>--%>
                                            </div>
                                        </div>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlSelect" runat="server" Direction="RightToLeft" Visible="false">
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم سجل صحيح"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <asp:Panel ID="Panel1" runat="server">
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>