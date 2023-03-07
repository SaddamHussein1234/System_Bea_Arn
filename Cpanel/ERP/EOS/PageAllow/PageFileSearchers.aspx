<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/EOS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageFileSearchers.aspx.cs" Inherits="Cpanel_ERP_EOS_PageAllow_PageFileSearchers" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.WSM.Repostry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
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

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
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
            .WidthMaglis24 {
                Width: 95%;
            }
        }
    </style>

    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
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
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVApprovalOfTheDirector.ClientID%>");
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
    <link href="/Cpanel/css/PageNext.css" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:Button ID="Button1" runat="server" />
                    <label class="control-label">
                        الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                    </label>
                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                        Width="100" ValidationGroup="GDetails">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="LBRefrsh" runat="server" class="btn btn-default" data-toggle="tooltip"
                                title="تحديث" OnClick="LBRefrsh_Click">
                            <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="">قائمة أوامر الصرف التي تحتاج إلى تسليم الباحث</a></li>
                    </ul>
                </div>
            </div>
            <div align="rigth" style="padding: 0 15px 0 15px">
                <hr style="border: double; border-width: 1px; width: 100%" />
                <h5><i class="fa fa-star"></i>حدد نوع الصرف : </h5>
                <asp:RadioButton ID="RBTathith" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RBTathith_CheckedChanged" Visible="false" />
                <i class="fa fa-star"></i>
                <span>فرز أوامر الدعم العيني - الأدوية والأجهزة - تأثيث المنازل - التالف </span>
                <span style="color: #9c1800">( الموجود حالياً
                    <asp:Label ID="lblCountCard" runat="server" Text="0"></asp:Label>
                    )</span>
                <nav class="navbar-dark bg-dark">
                    <a class="btn navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="fa fa-list"></span> عرض الفلترة
                    </a>
                </nav>
                <div class="collapse" id="navbarToggleExternalContent" style="background-color: #ecedec">
                    <div class="bg-dark p-4" style="padding: 5px 5px 10px 0">
                        <h5 class="text-white h4"><i class="fa fa-star"></i>حدد الملفات المراد عرضها : </h5>
                        <span class="text-muted">
                            <asp:RadioButton ID="RBCardCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBCardCheck_CheckedChanged" />
                            <span>عرض ملفات الدعم العيني </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBDeviceCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBDeviceCheck_CheckedChanged" />
                            <span>عرض ملفات الأدوية والأجهزة </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBTathithCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBTathithCheck_CheckedChanged" />
                            <span>عرض ملفات تأثيث منازل </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBTalefCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBTalefCheck_CheckedChanged" Enabled="false" />
                            <span>عرض ملفات التالف </span>
                        </span>
                    </div>
                </div>
                <hr style="border: double; border-width: 1px; width: 100%" />
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
                <div class="clearfix"></div>
            </div>
            
            <div class="container-fluid" id="IDCard" runat="server" visible="false">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة أوامر الصرف التي تحتاج إلى تسليم الباحث
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnAllow" class="btn btn-info" runat="server" Text="تم التسليم" OnClick="btnAllow_Click"
                                title="تحديد على انه تم التسليم" data-toggle="tooltip" OnClientClick="return ConfirmDelete();" />
                            <asp:Button ID="btnNotAllow" class="btn btn-danger" runat="server" Text="لم يتم التسليم" OnClick="btnNotAllow_Click"
                                title="تحديد على انه لم يتم التسليم" data-toggle="tooltip" OnClientClick="return ConfirmDelete();" />
                              
                        <asp:TextBox ID="txtNotAllow" runat="server" class="form-control2" ValidationGroup="g2" 
                            Height="30" Width="200px" placeholder="سبب عدم التسليم ... "></asp:TextBox>
                            <asp:Label ID="lblNotAllow" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>

                            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                                title="تحديث" OnClick="btnRefrish_Click">
                            <i class="fa fa-refresh"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                                title="طباعة" OnClientClick="return insertConfirmation();">
                            <i class="fa fa-print"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <div class="table table-responsive">
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div class="HideNow">
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                </div>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة أوامر الصرف التي تحتاج إلى تسليم الباحث" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVApprovalOfTheDirector" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVApprovalOfTheDirector_RowDataBound"
                                                    UseAccessibleHeader="False" AllowPaging="true" OnPageIndexChanging="GVApprovalOfTheDirector_PageIndexChanging" PageSize="200">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                        
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Font-Size="12px" Text='<%# Eval("_ID_Item_") %>' Visible="false"></asp:Label>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="_ID_MosTafeed_" HeaderText="رقم الملف" InsertVisible="False" ReadOnly="True"
                                                             SortExpression="_ID_MosTafeed_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:BoundField DataField="_bill_Number_" HeaderText="رقم الفاتورة" InsertVisible="False" ReadOnly="True"
                                                             SortExpression="_bill_Number_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("NameMostafeed") %>
                                                                <div style="font-size: 11px" >
                                                                    <%# ClassSaddam.FCheckAllowModer4((bool) (Eval("_Is_Moder_")))%> 
                                                                     , <%# ClassSaddam.FAmeenAlsondoq4((bool) (Eval("_Is_Ammen_AlSondoq_")))%>
                                                                     , <%# ClassSaddam.FRaeesMaglis4((bool) (Eval("_Is_Raees_Maglis_AlEdarah_")))%>
                                                                     , <%# ClassSaddam.FAmeenAlmostodaa4((bool) (Eval("_Is_Storekeeper_")))%>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("TypeAlDam") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                            <ItemTemplate>
                                                               <%# Eval("FirstName") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate_Add" runat="server" 
                                                                    Text='<%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# ClassProductShopWarehouse.FCount((Int32) (Eval("_bill_Number_")))%>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px"
                                                                    Text='<%# WSM_Repostry_Exchange_Order_Details_.FGetBySumBill(new Guid(Eval("_ID_Item_").ToString()))%>'></asp:Label>
                                                                <%# XMony %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href='../In_Kind_Donation/PageView.aspx?IDUniq=<%# ddlYears.SelectedValue %>&ID=<%# Eval("_bill_Number_")%>&XID=<%# Eval("_ID_MosTafeed_")%>&XIDCate=<%# Eval("_ID_Project_")%>&IsCart=<%# Eval("_Is_Cart_") %>&IsDevice=<%# Eval("_Is_Device_") %>&IsTathith=<%# Eval("_Is_Tathith_") %>&IsTalef=<%# Eval("_Is_Talef_") %>' 
                                                                    title="عرض التفاصيل" data-toggle="tooltip" class="btn btn-info"><span class="fa fa-eye"></span></a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="GridPager" BackColor="White" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                 / <span style="font-size: 12px; padding-right: 5px">المبلغ الاجمالي : </span>
                                                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <asp:Label ID="lblMony" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <div align="Left" class="HideThis">
                                                    <img src='/Img/IconTrue.png' style='width: 20px' alt="" />
                                                    <span style="font-size: 11px">موافق</span>
                                                    <img src='/Img/IconFalse.png' style='width: 20px' alt="" />
                                                    <span style="font-size: 11px">غير موافق</span>
                                                </div>
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlSelect" runat="server" Visible="False">
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
                    <h3 style="font-size: 20px">يرجى تحديد نوع الفرز
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
            </asp:Panel>
        </div>
        <br />
        <br />
        <br />
</asp:Content>

