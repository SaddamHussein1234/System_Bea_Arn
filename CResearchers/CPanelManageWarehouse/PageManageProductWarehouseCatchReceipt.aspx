<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductWarehouseCatchReceipt.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_PageManageProductWarehouseCatchReceipt" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" />
    <style>
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
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVProductShopWarehouseByID.ClientID%>");
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
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>

    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <div>
                        <asp:Button ID="btnSearch" runat="server" Text="بحث" OnClick="btnSearch_Click" class="btn btn-info pull-right" data-toggle="tooltip" title="بحث" Style="margin-right: 4px" />
                        &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم الفاتورة ... "></asp:TextBox>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageManageProduct.aspx">المنتجات</a></li>
                    <li><a href="">سند إدخال أصناف للمستودع</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByUser">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="سند إدخال أصناف للمستودع"></asp:Label>
                    </h3>
                    <div style="float: left">
                        <asp:LinkButton ID="LbRefreshSaraf" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LbRefreshSaraf_Click"
                            title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        <asp:LinkButton ID="LBPrintSaraf" runat="server" class="btn btn-success" data-toggle="tooltip" ValidationGroup="DLType"
                            title="طباعة" OnClick="LBPrintSaraf_Click">
                    <i class="fa fa-print"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                            OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="false">
                        <hr style='border: solid; border-width: 1px; width: 100%' />
                        <div class="">
                            <div align="center" class="w">
                                <div class="table table-responsive">
                                    <table style="width: 100%; background-color: #ffffff; color: #393939">
                                        <tr>
                                            <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                                <asp:TextBox ID="txtTitle" runat="server" Text="سند إدخال أصناف للمستودع" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                            </td>
                                            <td style="border: thin double #808080; border-width: 1px; width: 20%">
                                                <table style="width: 100%; font-size: 12px">
                                                    <tr>
                                                        <td align="left" style="width: 60%">رقم الفاتورة / 
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
                                                    <asp:Label ID="lblFromDonor" runat="server" Style="font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                                </div>
                                            </td>
                                            <td style="border: thin double #808080; border-width: 1px; width: 20%; text-align: center">
                                                <asp:Image ID="IDBarcode" runat="server" alt='Loding' />
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
                            <asp:GridView ID="GVProductShopWarehouseByID" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVProductShopWarehouseByID_RowDataBound"
                                UseAccessibleHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="10px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="_IDItem" HeaderText="_IDItem" InsertVisible="False" ReadOnly="True"
                                        SortExpression="_IDItem" Visible="false" />
                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رقم الطلب" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("_IDNumberProduct")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Font-Size="12px" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("ProductName")%>
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
                                            <%# Eval("_PriceOfTheGrain")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="السعر الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تاريخ الطلب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%# ClassQuaem.FAlBaheth((Int32) (Eval("_IDAdmin")))%>
                                            <br />
                                            <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px">
                                        <ItemTemplate>
                                            <a href='PageManageProductShippingWarehouse.aspx?ID=<%# Eval("_IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                class="btn btn-primary"><span class="fa fa-edit"></span></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
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
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 40%; border: thin double #808080; border-width: 1px;">
                                    <div style="font-size: 13px">
                                        <div align="center" style="font-size: 13px">
                                            تم الإستلام
                                            <asp:CheckBox ID="CBDone" runat="server" Enabled="false" />
                                            / لم يتم الإستلام
                                            <asp:CheckBox ID="CBNotDone" runat="server" Enabled="false" />
                                        </div>
                                    </div>
                                    <div style="font-size: 13px; padding-right: 5px">
                                        أمين المستودع / 
                                            <asp:Label ID="lblAmeenAlmosTodaa2" runat="server"></asp:Label>
                                    </div>
                                    <div style="font-size: 13px; padding-right: 5px">
                                        التوقيع / 
                                        <asp:Image ID="ImgAmeenAlmosTodaa" runat="server" Width='100px' Height='30px' />
                                    </div>
                                    <div style="font-size: 13px; padding: 0 5px 5px 0">
                                        التاريخ / 
                                            <asp:Label ID="lblDateGo" runat="server"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 40%; border: thin double #808080; border-width: 1px;">
                                    <div style="font-size: 13px; padding-right: 5px">
                                        الإسم / 
                                            <asp:Label ID="lblFromDonorTow" runat="server"></asp:Label>
                                    </div>
                                    <div style="font-size: 13px; padding-right: 5px">
                                        التوقيع / 
                                        <asp:TextBox ID="txtCoustmoer" runat="server" Text="وقع صورة طبق الأصل" class="form-control" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div align="left" runat="server" id="IDKhatm" visible="false">
                                        <img src="../../ImgSystem/ImgSignature/الختم.png" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <hr style='border: solid; border-width: 1px; width: 100%' />
                        <div class="table table-responsive">
                            <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                                <table style="width: 100%; margin: 5px; font-size: 12px">
                                    <tr>
                                        <td style="width: 45%;">مدير الجمعية : 
                                        </td>
                                        <td style="width: 55%;">
                                            <asp:Image ID="ImgModer" runat="server" Width='100px' Height='30px' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                                <table style="width: 100%; margin: 5px; font-size: 12px">
                                    <tr>
                                        <td style="width: 45%;">أمين الصندوق : 
                                        </td>
                                        <td style="width: 55%;">
                                            <asp:Image ID="ImgAmeenAlsondoq" runat="server" Width='100px' Height='30px' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
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
                </div>
            </div>
        </div>
</asp:Content>

