﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageView.ascx.cs" Inherits="Shaerd_CPanelManageZakat_PageView" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
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

        .WidthText5 {
            float: right;
            Width: 100%;
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

        .WidthText5 {
            float: right;
            Width: 100%;
            padding-right: 5px;
        }

        .WidthText40 {
            Width: 95%;
        }

        .WidthText5 {
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
    <div class="">
        <div align="center" class="w">
            <div class="table table-responsive">
                <table style="width: 100%; background-color: #ffffff; color: #393939">
                    <tr>
                        <td style="border: thin double #808080; border-width: 1px; width: 45%">
                            <asp:TextBox ID="txtTitle" runat="server" Text="سند إستلام تبرع عيني" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </td>
                        <td style="border: thin double #808080; border-width: 1px; width: 20%">
                            <table style="width: 100%; font-size: 12px">
                                <tr>
                                    <td align="left" style="width: 60%; font-family: 'Alwatan'; font-size: 18px;">رقم الفاتورة / 
                                    </td>
                                    <td style="width: 40%">
                                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="border: thin double #808080; border-width: 1px; width: 35%">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                    </td>
                                    <td style="width: 80%">
                                        <asp:Label ID="lblDateHide" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: thin double #808080; border-width: 1px; width: 45%">
                            <div style="font-size: 13px; margin-right: 10px">
                                إستلمنا من 
                                                <asp:DropDownList ID="DLType" runat="server" ValidationGroup="GPrint">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="السيد">السيد</asp:ListItem>
                                                    <asp:ListItem Value="السيدة">السيدة</asp:ListItem>
                                                    <asp:ListItem Value="السادة">السادة</asp:ListItem>
                                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="DLType" ErrorMessage="* إجباري" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="REVPrint" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                                /
                                                    <br />
                                <asp:Label ID="lblFromDonor" runat="server" Style="font-family: 'Alwatan'; font-size: 18px;"></asp:Label><br />
                                <asp:Label ID="lblPhoneDonor" runat="server" Style="font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                            </div>
                        </td>
                        <td style="border: thin double #808080; border-width: 1px; width: 20%; text-align: center">
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
                        <td style="border: thin double #808080; border-width: 1px; width: 35%">
                            <table style="font-size: 12px; margin: 10px; width: 90%;">
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
            </div>
        </div>
    </div>
    <p style="font-size: 13px">
        الاصناف التالية : 
    </p>
    <hr style='border: solid; border-width: 1px; width: 100%' />
    <div class="table table-responsive">
        <asp:GridView ID="GVDeedDonationInKind" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
            Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVProductShopWarehouseByID_RowDataBound"
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
                <asp:BoundField DataField="_ID_Item_" HeaderText="_ID_Item_" InsertVisible="False" ReadOnly="True"
                    SortExpression="_ID_Item_" Visible="false" />
                <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                    <ItemTemplate>
                        <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                    <ItemTemplate>
                        <%# Eval("Name_Category_")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                    <ItemTemplate>
                        <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="السعر الفردي" HeaderStyle-ForeColor="#CCCCCC">
                    <ItemTemplate>
                        <%# Eval("_One_Price_")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="السعر الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                    <ItemTemplate>
                        <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_Total_Price_")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
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
    </div>
    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
    <div style="display: none">
        <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
        - <span style="font-size: 12px; padding-right: 5px">عدد المنتجات : </span>
        <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
        - 
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
            </td>
            <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                <asp:TextBox ID="lblSumWord" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
            </td>
            <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
            </td>
        </tr>
    </table>
    <hr style='border: solid; border-width: 1px; width: 100%' />
    وذلك لغرض / 
                        <asp:Label ID="lblThe_Purpose" runat="server" Font-Size="12px"></asp:Label>
    <hr style='border: solid; border-width: 1px; width: 100%' />
    <table style="width: 100%;">
        <tr>
            <td style="width: 25%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">أمين المستودع </span>
                    <div>
                        <asp:Image ID="ImgAmeenAlmosTodaa" runat="server" Width='100px' Height='30px' />
                    </div>
                    <asp:Label ID="lblAmeenAlmosTodaa2" runat="server"></asp:Label>
                </div>
            </td>
            <td style="width: 25%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">مدير الجمعية</span>
                    <div>
                        <asp:Image ID="ImgModer" runat="server" Width='100px' Height='30px' />
                    </div>
                    <asp:Label ID="lblModer" runat="server"></asp:Label>
                </div>
            </td>
            <td style="width: 25%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">المشرف المالي</span>
                    <div>
                        <asp:Image ID="ImgAmeenAlsondoq" runat="server" Width='100px' Height='30px' />
                    </div>
                    <asp:Label ID="lblAmeenAlsondoq" runat="server"></asp:Label>
                </div>
            </td>
            <td style="width: 25%; border: thin double #808080; border-width: 1px;" align="center">
                <div style="margin: 0 0 5px 0;">
                    <span style="font-family: 'Alwatan'; font-size: 17px">الداعم</span>
                    <div>
                        <asp:TextBox ID="txtCoustmoer" runat="server" Text="وقع صورة طبق الأصل" class="form-control" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblFromDonorTow" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <table style="width: 100%; margin-top:-20px;">
        <tr>

            <td>
                <div align="left" runat="server" id="IDKhatm" visible="false">
                    <img src="/ImgSystem/ImgSignature/الختم.png" />
                </div>
            </td>
        </tr>
    </table>
    <div class="">
        <div class="table table-responsive">
            
            <div class="WidthText4" style="border: thin double #808080; border-width: 1px; display: none;" align="center">
                <table style="width: 100%; margin: 5px; font-size: 12px">
                    <tr>
                        <td style="width: 45%;">رئيس الجمعية : 
                        </td>
                        <td style="width: 55%;">
                            <asp:Image ID="ImgRaees" runat="server" Width='100px' Height='30px' />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
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
        <h3 style="font-size: 20px">الرجاء إدخال رقم الفاتورة
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
