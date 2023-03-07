<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageView.ascx.cs" Inherits="Shaerd_ERP_FMS_GeneralAssembly_PageView" %>

<%@ Register Src="~/Cpanel/CAttach/WUCFooterBill.ascx" TagPrefix="uc1" TagName="WUCFooterBill" %>

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
                Width: 33.333%;
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
                    <a runat="server" id="IDEdit" href="#" data-toggle="tooltip" title="تعديل الملف" class="btn btn-info">وضع التعديل</a>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم الإيصال ... "></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="بحث"  data-toggle="tooltip" title="بحث"
                            class="btn btn-info" OnClick="btnSearch_Click" />
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return ConfirmDelete();" >
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../">الرئيسية</a></li>
                    <li><a href="PageAll.aspx">إيصالات الإشتراكات</a></li>
                    <li><a href="#">إيصال إشتراك</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="ايصال إشتراك الأعضاء"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
                            <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <header class="hide"><img src='/view/image/LogoTitleNew2.jpg' style='width:100%; height:100px;' /></header>
                                    <div class="">
                                        <div align="center" class="w">
                                            <table style="width: 100%; background-color: #ffffff; color: #393939">
                                                <tr>
                                                    <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="ايصال / إستلام إشتراكات الأعضاء" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </td>
                                                    <td style="border: thin double #808080; border-width: 1px; width: 20%; font-family: 'Alwatan'; font-size: 18px;">
                                                        <span style="padding-right: 10px; font-size: 18px;">رقم الايصال /  </span>
                                                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                                    </td>
                                                    <td rowspan="2" style="border: thin double #808080; border-width: 1px; width: 35%">
                                                        <div align='center' class="w">
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
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="border: thin double #808080; border-width: 1px; width: 35%">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                                                </td>
                                                                <td style="width: 80%">
                                                                    <asp:Label ID="lblDateHide" runat="server" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    
                                    <%--<table style="width: 100%; margin-top:0;">
                                        <tr>
                                            <td style="width: 25%" align="Right" colspan="4">
                                                <span style="font-size: 14px">
                                                    <div style="float: right; font-family: 'Alwatan'; font-size: 17px">
                                                        مبلغ وقدرة : - 
                                                    </div>
                                                    <div align="left" style="font-family: 'Alwatan'; font-size: 17px">
                                                        <asp:Label ID="lbl_Initiatives" runat="server"></asp:Label>
                                                    </div>
                                                </span>
                                            </td>
                                        </tr>
                                    </table>--%>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 35%;">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 46%;">
                                                            <span style="font-size: 13px">إستلمنا من / 
                                                            </span>
                                                            <div style="font-family: 'Alwatan'; font-size: 17px" align="center">
                                                                <asp:Label ID="lbl_Name" runat="server"></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td style="width: 33%; display: none;">
                                                            <table style="font-size: 12px">
                                                                <tr>
                                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                                                        <asp:Label ID="lblDataEntry" runat="server" Font-Size="12px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                                                        <asp:Label ID="lblDateEntry" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="Tr2" visible="false">
                                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                                                        <asp:Label ID="Label17" runat="server" Font-Size="12px"></asp:Label>
                                                                    </td>
                                                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                                                        <asp:Label ID="Label18" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 65%;">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                                                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 13px'></asp:Label>
                                                            <asp:Label ID="Label150" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </td>
                                                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                                                            <asp:Label ID="lblSumSaraf" runat="server" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">طريقة الدفع 
                                                        </td>
                                                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                                                            <div id="PnlCash" runat="server" visible="false">
                                                                <asp:CheckBox ID="CBCash_Money_" runat="server" Width="20px" Enabled="false" />
                                                                <span>نقداً </span>
                                                            </div>
                                                            <div id="PnlShayk" runat="server" visible="false">
                                                                <asp:CheckBox ID="CBShayk_Bank" runat="server" Width="20px" Enabled="false" />
                                                                <span style="font-size:11px;"> : </span>
                                                                <asp:Label ID="lblNumber_Shayk_Bank_" runat="server" Font-Size="11px"></asp:Label>
                                                                <span style="font-size:11px;">- بتاريخ :</span>
                                                                <asp:Label ID="lblDate_Shayk_Bank_" runat="server" Font-Size="11px"></asp:Label>
                                                                <span style="font-size:11px;">- على :</span>
                                                                <asp:Label ID="lblFor_Bank_" runat="server" Font-Size="11px"></asp:Label>
                                                            </div>
                                                            <div id="PnlTrnfire" runat="server" visible="false">
                                                                <asp:CheckBox ID="CBTrnfire_Bank" runat="server" Width="20px" Enabled="false" />
                                                                <span style="font-size:11px;">إيداع بنكي على :</span>
                                                                <asp:Label ID="lblFor_Edaa_Bank" runat="server" Font-Size="11px"></asp:Label>
                                                                <span style="font-size:11px;">- رقم الإيداع :</span>
                                                                <asp:Label ID="lblNumber_Edaa" runat="server" Font-Size="11px"></asp:Label>
                                                                <span style="font-size:11px;">- تاريخ :</span> 
                                                                <asp:Label ID="lblDate_Edaa_Bank" runat="server" Font-Size="11px"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                    <div style="margin: 10px">
                                        وذلك رسوم إشتراك عضوية : 
                                         ( <asp:Label ID="lblMore_Details" runat="server"></asp:Label> ) , 
                                        عن عام ( <asp:Label ID="lbl_Years" runat="server"></asp:Label> )
                                    </div>
                                    <div align="left" style="margin-top: -60px" runat="server" id="IDKhatm" visible="false">
                                        <img src="/ImgSystem/ImgSignature/الختم.png" />
                                    </div>
                                    <table style="width: 100%; margin-top: -60px;">
                                        <tr>
                                            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                                <div style="margin: 0 0 5px 0;">
                                                    <span style="font-family: 'Alwatan'; font-size: 17px">المشرف المالي</span>
                                                    <div>
                                                        <asp:Image ID="ImgAmeenAlsondoq" runat="server" Width='100px' Height='30px' />
                                                    </div>
                                                    <asp:Label ID="lblAmeenAlsondoq" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                                <div style="margin: 0 0 5px 0;">
                                                    <span style="font-family: 'Alwatan'; font-size: 17px">رئيس مجلس الإدارة</span>
                                                    <div>
                                                        <asp:Image ID="ImgRaees_AlMagles" runat="server" Width='100px' Height='30px' />
                                                    </div>
                                                    <asp:Label ID="lblReesAlmaglis" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                                <div style="margin: 0 0 5px 0;">
                                                    <span style="font-family: 'Alwatan'; font-size: 17px">العضو</span>
                                                    <div  style="display:none;">
                                                        <asp:Image ID="Img_Admin" runat="server" Width='100px' Height='30px' />
                                                    </div><br /><br />
                                                    <asp:Label ID="lbl_Name_2" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="hide">
                                        <uc1:WUCFooterBill runat="server" ID="WUCFooterBill" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">يرجى إدخال رقم إيصال صحيح
                                </h3>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
