<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="Cpanel_ERP_OM_Performance_Index_PageView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <span runat="server" id="IDSearch">
                        <a runat="server" id="IDEdit" href="#" data-toggle="tooltip" title="تعديل الملف" class="btn btn-info">وضع التعديل</a>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم البطاقة ... "></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                            class="btn btn-info" OnClick="btnSearch_Click" />
                    </span>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../">الرئيسية</a></li>
                    <li><a href="PageAll.aspx">بطاقات مؤشر الأداء</a></li>
                    <li><a href="#">تفاصيل بطاقة مؤشر الأداء</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="بطاقة مؤشر الأداء" Style="text-align: center; width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
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
                            <div class="container-fluid">
                                <div class="panel-body">
                                    <div class="content-box-large">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <b>إسم الموظف</b>
                                                    </td>
                                                    <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblNameEmp" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">بتاريخ
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="lblDateCreate" runat="server"></asp:Label>
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
                                                    <td style="width: 100%; border-width: 2px; border-color: #a1a0a0;">
                                                        <b>الربط مع بطاقة الأداء</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">المنظور (BSC)
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblBSC" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">المحور الرئيسي (KRA)
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblKRA" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%; border-width: 2px; border-color: #a1a0a0;">
                                                        <b>معلومات الهدف الإستراتيجي</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رمز الهدف الإستراتيجي
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblStrategic_Goal_Icon" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">نص الهدف الإستراتيجي
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblStrategic_Goal_text" runat="server"></asp:Label>
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
                                                        <b>الإدارات المسؤولة عن التنفيذ</b>
                                                    </td>
                                                    <td style="width: 75%; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblManagement" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%; border-width: 2px; border-color: #a1a0a0;">
                                                        <b>معلومات المؤشر</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رمز المؤشر
                                                                </td>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                    <asp:Label ID="lblpointer_Icon" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">المؤشر 
                                                    </td>
                                                    <td style="width: 50%; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblpointer" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="StyleTD" colspan="2">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">مالك المؤشر 
                                                                </td>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                    <asp:Label ID="lblPointer_Owner" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">وحدة القياس
                                                                </td>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                    <asp:Label ID="lblMeasruing_Unit" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">خط الأساس
                                                                </td>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                    <asp:Label ID="lblBaseline" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">القطبية
                                                                </td>
                                                                <td style="width: 50%;">
                                                                    <asp:Label ID="lblPolar" runat="server"></asp:Label>
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
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">دورية القياس 
                                                                </td>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                    <asp:Label ID="lblMeasurement_Periodicity" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 25%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">التراكمية 
                                                                </td>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                    <asp:Label ID="lblCumulative" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%; ">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">القيمة المرجعية
                                                                </td>
                                                                <td style="width: 50%;">
                                                                    <asp:Label ID="lblReference_Value" runat="server"></asp:Label>
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
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الغرض من القياس
                                                    </td>
                                                    <td style="width: 75%; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblPurpose_of_the_Measurement" runat="server"></asp:Label>
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
                                                        <b>معادلة صيغة المؤشر</b>
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%;" colspan="2" align="center">
                                                                    <b><asp:Label ID="lblPointer_Formula_Equation_One" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 80%; border-width: 2px; border-color: #a1a0a0;">
                                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <b>100 X</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%;" colspan="2" align="center">
                                                                    <b><asp:Label ID="lblPointer_Formula_Equation_Two" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <b>الهدف (المستوى المستهدف)</b>
                                                    </td>
                                                    <td style="width: 25%; border-width: 2px; border-color: #a1a0a0;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%;" colspan="2">
                                                                    <asp:Label ID="lblTarget" runat="server"></asp:Label>
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
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <b>مصدر البيانات</b>
                                                    </td>
                                                    <td style="width: 75%; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblData_Source" runat="server"></asp:Label>
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
                                                        <b>الشواهد المرفقة</b>
                                                    </td>
                                                    <td style="width: 75%; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblAttached_Evidence" runat="server"></asp:Label>
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
                                                        <b>الملاحظات</b>
                                                    </td>
                                                    <td style="width: 75%; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblNote" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="container-fluid">
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="container-fluid" dir="rtl">     
                                            <table style="width: 100%; font-size: 12px; margin-top:5px;">
                                                <tr>
                                                    <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                                        <div style="margin: 0 0 5px 0;">
                                                            <span style="font-family: 'Alwatan'; font-size: 17px">مسؤول القياس</span>
                                                            <div align="right" style="margin-right: 5px;">
                                                                الإسم :
                                                                <asp:Label ID="lblMeasurement_Officer" runat="server"></asp:Label><br />
                                                                التوقيع :
                                                                <asp:Image ID="ImgMeasurement_Officer" runat="server" Width='100px' Height='30px' /><br />
                                                                <asp:Label ID="lblMeasurement_OfficerAllowDate" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                                        <div style="margin: 0 0 5px 0;">
                                                            <span style="font-family: 'Alwatan'; font-size: 17px">مسؤول التنفيذ</span>
                                                            <div align="right" style="margin-right: 5px;">
                                                                الإسم :
                                                                <asp:Label ID="lblImplementation_Officer" runat="server"></asp:Label><br />
                                                                التوقيع :
                                                                <asp:Image ID="ImgImplementation_Officer" runat="server" Width='100px' Height='30px' /><br />
                                                                <asp:Label ID="lblImplementation_OfficerAllowDate" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                                        <div style="margin: 0 0 5px 0;">
                                                            <span style="font-family: 'Alwatan'; font-size: 17px">مدير الجمعية</span>
                                                            <div align="right" style="margin-right: 5px;">
                                                                الإسم :
                                                                <asp:Label ID="lblGeneral_Director" runat="server"></asp:Label><br />
                                                                التوقيع :
                                                                <asp:Image ID="ImgGeneral_Director" runat="server" Width='100px' Height='30px' /><br />
                                                                <asp:Label ID="lblGeneral_DirectorAllowDate" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                            <div align="left" style="margin-top: -90px" runat="server" id="IDKhatm" visible="false">
                                                <img src="/ImgSystem/ImgSignature/الختم.png" />
                                            </div>
                                            <div align="left" style="margin-top: -90px" runat="server" id="IDKhatmLodding" visible="false">
                                                <img src="/Cpanel/loader.gif" width="113" />
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
</asp:Content>

