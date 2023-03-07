<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageView.ascx.cs" Inherits="Shaerd_ERP_OM_Cash_Donation_PageView" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterBill.ascx" TagPrefix="uc1" TagName="WUCFooterBill" %>
<style type="text/css">
    @media screen and (min-width: 768px) {
        .WidthText13 {
            float: right;
            Width: 13%;
            padding-right: 5px;
        }

        .WidthText38 {
            float: right;
            Width: 38%;
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
            padding-right: 5px;
        }

        .WidthText40 {
            float: right;
            Width: 45%;
            padding-right: 5px;
        }
    }

    @media screen and (max-width: 767px) {
        .WidthText13 {
            Width: 95%;
        }

        .WidthText38 {
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

        .WidthText40 {
            Width: 95%;
        }
    }

    @media screen and (min-width: 768px) {
        .WidthText20 {
            Width: 150px;
            height: 36px;
        }
    }

    @media screen and (max-width: 767px) {
        .WidthText20 {
            Width: 100px;
            height: 36px;
        }
    }
</style>

<asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="false">
    <header class="hide">
        <img src='/view/image/LogoTitleNew2.jpg' style='width: 100%; height: 100px;' /></header>
    <div class="">
        <div align="center" class="w">
            <table style="width: 100%; background-color: #ffffff; color: #393939">
                <tr>
                    <td style="border: thin double #808080; border-width: 1px; width: 45%">
                        <asp:TextBox ID="txtTitle" runat="server" Text="سند إيصال تبرع نقدي" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                    </td>
                    <td style="border: thin double #808080; border-width: 1px; width: 20%; font-family: 'Alwatan'; font-size: 18px;">
                        <span style="padding-right: 10px; font-size: 18px;">رقم السند /  </span>
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
            <td style="width: 30%;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 46%;">
                            <span style="font-size: 13px">إستلمنا من / 
                            </span>
                            <asp:DropDownList ID="DLType" runat="server" ValidationGroup="GPrint">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="السيد">السيد</asp:ListItem>
                                <asp:ListItem Value="السيدة">السيدة</asp:ListItem>
                                <asp:ListItem Value="السادة">السادة</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="DLType" ErrorMessage="* إجباري" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="REVPrint" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                            <div style="font-family: 'Alwatan'; font-size: 17px" align="center">
                                <asp:Label ID="lblFromDonor" runat="server"></asp:Label>
                            </div>
                        </td>
                        <td style="width: 33%; display: none;">
                            <table style="font-size: 12px; margin: 10px; width: 90%; display: none;">
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
                                <tr runat="server" id="IDUpdate" visible="false" style="display: none">
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                        <asp:Label ID="lblDataEntryEdit" runat="server" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px; display: none">بتاريخ :
                                                        <asp:Label ID="lblDateEntryEdit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 70%;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 13px'></asp:Label>
                            <asp:Label ID="Label150" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                        </td>
                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:Label ID="lblSumWord" runat="server" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">طريقة الدفع 
                        </td>
                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                            <div id="IDCash_Money" runat="server" visible="false">
                                <asp:CheckBox ID="CBCash_Money_" runat="server" Width="20px" Enabled="false" />
                                <span>نقداً </span>
                            </div>
                            <div id="IDShayk_Bank" runat="server" visible="false">
                                <asp:CheckBox ID="CBShayk_Bank" runat="server" Width="20px" Enabled="false" />
                                <span style="font-size: 11px;">شيك رقم : </span>
                                <asp:Label ID="lblNumber_Shayk_Bank" runat="server" Font-Size="11px"></asp:Label>
                                <span style="font-size: 11px;">- بتاريخ : </span>
                                <asp:Label ID="lblDate_Shayk" runat="server" Font-Size="11px"></asp:Label>
                                <span style="font-size: 11px;">- على : </span>
                                <asp:Label ID="lblFor_Bank" runat="server" Font-Size="11px"></asp:Label>
                            </div>
                            <div id="IDTransfer_On_Account" runat="server" visible="false">
                                <asp:CheckBox ID="CBTransfer_On_Account" runat="server" Width="20px" Enabled="false" />
                                <span style="font-size: 11px;">إيداع بنكي على :</span>
                                <asp:Label ID="lblFor_Bank_Transfer" runat="server" Font-Size="11px"></asp:Label>
                                <asp:Label ID="lblNumber_Account" runat="server" Font-Size="11px"></asp:Label>
                                <span style="font-size: 11px;">- تاريخ :</span>
                                <asp:Label ID="lblDate_Bank_Transfer" runat="server" Font-Size="11px"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="margin: 10px">
        <asp:Label ID="lblFor" runat="server" Font-Size="12px"></asp:Label>

        <asp:Label ID="lblProject" runat="server" Font-Size="12px"></asp:Label>
        / 
                            <asp:Label ID="lblThe_Purpose" runat="server" Font-Size="12px"></asp:Label>
    </div>
    <div align="left" style="margin-top: -60px" runat="server" id="IDKhatm" visible="false">
        <img src="/ImgSystem/ImgSignature/الختم.png" />
    </div>
    <div align="left" style="margin-top: -60px" runat="server" id="IDKhatmLodding" visible="false">
        <img src="/Cpanel/loader.gif" width="113" />
    </div>
    <table style="width: 100%; margin-top: -60px; font-size: 12px">
        <tr>
            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">المشرف المالي</span>
                    <div align="right" style="margin-right: 5px;">
                        الإسم :
                        <asp:Label ID="lblAmeenAlsondoq" runat="server"></asp:Label><br />
                        التوقيع :
                        <asp:Image ID="ImgAmeenAlsondoq" runat="server" Width='100px' Height='30px' />
                        <asp:Label ID="lblAmeenAlsondoqAllowDate" runat="server"></asp:Label>
                    </div>
                </div>
            </td>
            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">رئيس مجلس الإدارة</span>
                    <div align="right" style="margin-right: 5px;">
                        الإسم :
                        <asp:Label ID="lblRaeesMaglis" runat="server"></asp:Label><br />
                        التوقيع :
                        <asp:Image ID="ImgRaees" runat="server" Width='100px' Height='30px' />
                        <asp:Label ID="lblRaeesMaglisAllowDate" runat="server"></asp:Label>
                    </div>
                </div>
            </td>
            <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">المتبرع</span>
                    <div align="right" style="margin-right: 5px;">
                        الإسم :
                        <asp:Label ID="lblFromDonorTow" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="txtCoustmoer" runat="server" Text="وقع صورة طبق الأصل"
                        CssClass="form-control" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <div class="hide">
        <uc1:wucfooterbill runat="server" id="WUCFooterBill" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlNull" runat="server" Visible="False">
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
</asp:Panel>
<asp:Panel ID="pnlSelect" runat="server">
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
        <h3 style="font-size: 20px">الرجاء إدخال رقم الامر
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
<asp:HiddenField ID="HFID" runat="server" />
