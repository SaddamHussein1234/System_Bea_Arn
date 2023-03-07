<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeePermissionByView.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeePermission_PageEmployeePermissionByView" %>
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
                <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم القرار ... "></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                    class="btn btn-info" OnClick="btnSearch_Click" />
            </span>
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                title="طباعة" OnClientClick="return ConfirmDelete();" Style="margin-left: 5px">
            <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeePermission.aspx">أستئذانات الموظفين</a></li>
            <li><a href="">تفاصيل إستئذان موظف</a></li>
        </ul>
    </div>
</div>
<asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="إستئذان موظف" Style="text-align: center; width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
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
                                                    <i class="fa fa-star"></i> QR Code : 
                                                </label><br />
                                                <div align="center">
                                                    <asp:Image ID="ImgQRCode2" runat="server" alt='صورة QRCode' style="width:300px; height:300px;" />
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
                    <table style="width: 95%">
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الإستئذان
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblNumberPermission" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ الإستئذان
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
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إسم الموظف
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblNameEmp" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 50%;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إدارة
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="lblManagment" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الموظف
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblEmpNo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الوظيفة
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الجوال
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">0<asp:Label ID="lblPhone" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الحالة الإجتماعية
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblMaratialStatus" runat="server"></asp:Label>
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
                    <br />
                    <div class="container-fluid">
                        <div class="panel-body">
                            <div class="content-box-large">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText4">
                                        <div class="form-group">
                                            <strong> <i class="fa fa-star"></i> طبيعة الإستئذان :-
                                            </strong>
                                        </div>
                                    </div>
                                    <div class="WidthText5">
                                        <div class="WidthText1">
                                            <div class="form-group" style="margin-top: 4px">
                                                <asp:Label ID="lblEarly_Dismissal" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="WidthText1">
                                            <div class="form-group" style="margin-top: 4px">
                                                <asp:Label ID="lblLate_In_Attendance" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="WidthText1">
                                            <div class="form-group" style="margin-top: 4px">
                                                <asp:Label ID="lblExit_And_Return" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <br />
                                        <div class="form-group">
                                            <strong> <i class="fa fa-star"></i> وقت الإستئذان :-
                                            </strong>
                                        </div>
                                    </div>
                                    <div class="WidthText5">
                                        <div class="WidthText2">
                                            <div class="form-group" style="margin-top: 4px">
                                                <i class="fa fa-calendar"></i> <asp:Label ID="lblFrom" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="WidthText2">
                                            <div class="form-group" style="margin-top: 4px">
                                                <i class="fa fa-calendar"></i> <asp:Label ID="lblTo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <br />
                                        <div class="form-group">
                                            <strong> <i class="fa fa-star"></i> سبب الإستئذان :-
                                            </strong>
                                        </div>
                                    </div>
                                    <div class="WidthText5">
                                        <div class="form-group" style="margin-top: 4px">
                                            <asp:Label ID="lblDescrption" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="WidthText5">
                                        <div class="form-group">
                                            <table style="width: 100%; margin-top:-30px;">
                                                <tr>
                                                    <td align="right">
                                                        <br />
                                                    </td>
                                                    <td align="left">
                                                        <span style=" font-family: 'Alwatan'; font-size: 18px;">
                                                            توقيع الموظف
                                                        </span>
                                                        <br />
                                                        <asp:Image ID="Img_Emp" runat="server" Width='100px' Height='25' />
                                                        <br />
                                                        <asp:Label ID="lbl_Name2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid">
                        <div class="panel-body">
                            <div class="content-box-large">
                                <div class="container-fluid" dir="rtl">                                          
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="WidthText4">
                                                    <div class="form-group">
                                                        <strong>
                                                                <i class="fa fa-star"></i> المسؤل المباشر /
                                                        </strong>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <div runat="server" id="pnlAllow">
                                                    <i class="fa fa-check-square"></i> موافق
                                                </div>
                                                <div runat="server" id="pnlNotAllow">
                                                    <i class="fa fa-check-square"></i> غير موافق للأسباب التالية :
                                                    <br />
                                                    <asp:Label ID="lblComments" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                الإسم : 
                                                <asp:Label ID="lblModer" runat="server"></asp:Label>
                                                <br />
                                                التوقيع : 
                                                <asp:Image ID="Img_Moder" runat="server" Width='100px' Height='25' /><br />
                                                التاريخ : 
                                                <asp:Label ID="lblDateModer" runat="server"></asp:Label>
                                            </td>
                                            <%--<td align="center">
                                                <span style="font-family: 'Alwatan'; font-size: 20px;">مدير الجمعية
                                                </span>
                                                <br />
                                                        
                                                <br />
                                                        
                                            </td>--%>
                                        </tr>
                                    </table>
                                    <div runat="server" id="IDKhatm" align="left" style="margin-top: -120px" visible="false">
                                        <img src="<%=ResolveUrl("~/ImgSystem/ImgSignature/الختم.png")%>" />
                                    </div>
                                    <br />
                                    <div style="font-size:12px;">
                                        <strong>
                                                <i class="fa fa-star"></i> ملاحظة :
                                        </strong><br />
                                            - لا يسمح بعمل أكثر من ثلاثة إستئذانات في الشهر . <br />
                                            - الإستئذان يؤثر في تقييم الموظف .
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
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