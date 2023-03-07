<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryBySearch.aspx.cs" Inherits="Cpanel_PageBeneficiaryBySearch" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="css/chosen.css" rel="stylesheet" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
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
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVBeneficiaryAll.ClientID%>");
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
                return confirm(" هل تريد الإستمرار ؟");
            }
        }
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
            .WidthMaglis {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }
        }

        /*@media print{input{display: none;}}*/
        .HideNow {
            display: none;
        }
    </style>
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
    <%--<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageAddBeneficiary.aspx" data-toggle="tooltip" title="إضافة مستفيد" class="btn btn-primary" id="IDAdd" runat="server"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click" Visible="false"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    <asp:LinkButton ID="btnHide" runat="server" class="btn btn-warning" data-toggle="tooltip" Style="margin-right: 5px" OnClick="btnHide_Click"
                                title="إخفاء المحدد">
                                <i class="fa fa-eye-slash"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageBeneficiaryBySearch.aspx">قائمة المستفيدين</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة المستفيدين حسب فرز معين
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="btnSearch" runat="server" data-toggle="tooltip" title="بحث" OnClick="btnSearch_Click" Visible="false"
                                class="btn btn-info pull-right"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            نوع المستفيد : 
                                        <asp:DropDownList ID="DLTypeMostafeed" runat="server" ValidationGroup="g2" CssClass="dropdown"
                                            Height="34" Width="140">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="دائم">دائم</asp:ListItem>
                                            <asp:ListItem Value="مستبعد">مستبعد</asp:ListItem>
                                        </asp:DropDownList>
                            القرية : 
                                        <asp:DropDownList ID="DLAlQriah" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                            الجنس : 
                                        <asp:DropDownList ID="DLGender" runat="server" Height="34" Width="140" CssClass="dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                            حالة المستفيد : 
                                        <asp:DropDownList ID="DLHalafAlMosTafeed" runat="server" Height="34" Width="140" CssClass="dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>

                            <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;"
                                class="btn btn-info btn-fill pull-right" ValidationGroup="g2" OnClick="btnGet_Click" />
                        </div>
                        <div class="col-sm-12">
                            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-warning">تحذير</span>
                                <span>خطأ غير متوقع , يرجى المحاولة لاحقاً ... </span>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible " role="alert">
                                <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                <span>تمت العملية بنجاح ... </span>
                            </div>
                        </div>

                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive">
                                <table class='table' style="width: 100%" id="Grid">
                                    <thead>
                                        <tr>
                                            <th>
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
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVBeneficiaryAll" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
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
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px">
                                                                    <asp:Label ID="lblM" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NumberMostafeed" HeaderText="ملف" SortExpression="NumberMostafeed"
                                                            HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 11px">
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("NameMostafeed")%>'></asp:Label>
                                                                </span>
                                                                <div class="HideThis">
                                                                    <%# ClassSaddam.FCheckAllowRaeesLagnat((bool) (Eval("IsAllowRaeesLagnatAlBahth_")))%> , 
                                                                    <%# ClassSaddam.FCheckAllowModer((bool) (Eval("IsAllowModer_")))%> , 
                                                                    <%# ClassSaddam.FCheckAllowRaeesMaglis((bool) (Eval("IsRaeesMaglisAlEdarah_")))%>
                                                                </div>
                                                                <%--<div style="background:#000000; color:#f0f0f0;">
                                                                    <%# ClassQuaem.FAlBaheth(Convert.ToInt32(Eval("AlBaheth"))) %>
                                                                </div>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TypeMostafeed" runat="server" Text='<%# ClassMosTafeed.FChangColor((string) Eval("TypeMostafeed"))%>'></asp:Label>
                                                                <br />
                                                                <div class="HideThis">
                                                                    <%# ClassGroup.FGetGroupAdminByID(Convert.ToInt32(Eval("_ID_Group_"))) %>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_HalafAlMosTafeed" runat="server" Text='<%# ClassQuaem.FHalatMostafeed((Int32) Eval("HalafAlMosTafeed"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_AlQaryah" runat="server" Text='<%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NumberAlSegelAlMadany" HeaderText="رقم السجل" SortExpression="NumberAlSegelAlMadany"
                                                            HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:BoundField DataField="PhoneNumber" HeaderText="الجوال" SortExpression="PhoneNumber"
                                                            HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderText="الافراد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <a href='PageBeneficiaryAddBoys.aspx?ID=<%# Eval("NumberMostafeed")%>&XID=<%# Eval("IDUniq")%>' title="عرض الافراد" data-toggle="tooltip"><span class="fa fa-eye HideThis"></span>
                                                                    <asp:Label ID="lbl_Afrad" runat="server" Text='<%# Eval("AfradAlOsrah")%>'></asp:Label>  فرد
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:CheckBoxField DataField="IsActive" HeaderText="حساب" SortExpression="IsActive"
                                                            HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <a href='PageBeneficiaryByView.aspx?ID=<%# Eval("NumberMostafeed")%>&XID=<%# Eval("IDUniq")%>' title="عرض الملف" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                                <a href='PageBeneficiaryEdit.aspx?XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"><span class="fa fa-edit"></span></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AlDakhlAlShahryllMostafeed" HeaderText="الراتب" SortExpression="AlDakhlAlShahryllMostafeed"
                                                            HeaderStyle-ForeColor="#CCCCCC" />
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
                                                <div>
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <div class="container-fluid" dir="rtl" runat="server">
                                                        <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <div style="float: right">
                                                    <span style="font-size: 12px; padding-right: 5px">عدد الأسر : </span>
                                                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                </div>
                                                <div align="left" runat="server" visible="false">
                                                    <span style="font-size: 12px; padding-right: 5px">عدد الايتام : </span>
                                                    <asp:Label ID="lblCountBoys" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                </div>
                                                <div align="Left" class="HideThis">
                                                    <img src='../Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">إطلع</span>
                                                    <img src='../Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">لم يطلع</span>
                                                </div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                            <asp:HiddenField ID="hfGridHtml" runat="server" />
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
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

