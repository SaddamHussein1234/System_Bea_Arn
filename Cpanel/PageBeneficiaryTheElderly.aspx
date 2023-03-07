<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryTheElderly.aspx.cs" Inherits="Cpanel_PageBeneficiaryTheElderly" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
            document.getElementById("<%=btnGetByType.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>

    <style type="text/css">
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
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
                Width: 49%;
            }

            .WidthText5 {
                float: right;
                Width: 100%;
            }

            .WidthText20 {
                Width: 150px;
                height: 36px;
            }

            .WidthText30 {
                float: right;
                Width: 16%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText30 {
                Width: 95%;
            }

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

        @media screen and (min-width: 768px) {
            .WidthMaglis {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

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

            .WidthMaglis24 {
                Width: 95%;
            }
        }

        .HideEdarah {
            display: none;
        }

        .redFont{
            color:red;
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
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVMostafeed.ClientID%>");
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

    <script src="../view/Chart/fusioncharts.js"></script>
    <script src="../view/Chart/fusioncharts.charts.js"></script>

    <link href="css/chosen.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">

                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="PageBeneficiaryTheElderly.aspx">إحصائية حسب كبار السن</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="Label2" runat="server" Text="إحصائية حسب كبار السن"></asp:Label>
                        </h3>
                        <div style="float: left">
                            نوع المستفيد : 
                            <asp:DropDownList ID="DLTypeMostafeed" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                <asp:ListItem Value="دائم" Selected="True">دائم</asp:ListItem>
                                <asp:ListItem Value="مستبعد">مستبعد</asp:ListItem>
                            </asp:DropDownList>
                            <span class="hide">
                                القرية : 
                                <asp:DropDownList ID="DLAlQriahByData" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                            <asp:LinkButton ID="btnHide" runat="server" class="btn btn-warning" data-toggle="tooltip" Style="margin-right: 5px" 
                                OnClientClick="return ConfirmDelete();" OnClick="btnHide_Click"
                                    title="إخفاء المحدد"><i class="fa fa-eye-slash"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnView" runat="server" class="btn btn-info" data-toggle="tooltip" Style="margin-right: 5px" 
                                OnClientClick="return ConfirmDelete();" OnClick="btnView_Click"
                                    title="إظهار المحدد"><i class="fa fa-eye"></i></asp:LinkButton>
                            <asp:LinkButton ID="LBPrintAll" runat="server" class="btn btn-success" data-toggle="tooltip"
                                title="طباعة" OnClick="LBPrintAll_Click">
                            <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:LinkButton ID="LBReafrchAll" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBReafrchAll_Click"
                                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>الذكور : </h5>
                                <small>أكبر من : </small><asp:TextBox ID="txtAgeBoy" Width="50" runat="server" class="form-control2" ValidationGroup="GAge" Text="60"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                   ControlToValidate="txtAgeBoy" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="GAge" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>الإناث : </h5>
                                <small>أكبر من : </small><asp:TextBox ID="txtAgeGirls" Width="50" runat="server" class="form-control2" ValidationGroup="GAge" Text="60"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                   ControlToValidate="txtAgeGirls" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="GAge" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <h5>الفلترة : </h5>
                                <asp:RadioButtonList ID="RBLFilter" runat="server"  Height="34" Style="font-size: 12px; width: 140px">
                                    <asp:ListItem Value="_All" Selected="True">الكل</asp:ListItem>
                                    <asp:ListItem Value="_Boy">الذكور</asp:ListItem>
                                    <asp:ListItem Value="_Gir">الإناث</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnGetByType" runat="server" Text="جلب بيانات" ValidationGroup="GAge"
                                    CssClass="btn btn-info btn-fill" OnClick="btnGetByType_Click" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <h5><i class="fa fa-star"></i> فلترة العرض : </h5>
                                <div class="checkbox checkbox-primary">
                                    <asp:CheckBox ID="CB3" runat="server" Text="رقم الملف" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB4" runat="server" Text="الإسم" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB6" runat="server" Text="حالات الاسر" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB7" runat="server" Text="القرية" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB8" runat="server" Text="رقم السجل" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB9" runat="server" Text="الجوال" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB10" runat="server" Text="تاريخ الميلاد" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB11" runat="server" Text="العمر" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB12" runat="server" Text="حالة المسكن" Font-Size="12px" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB13" runat="server" Text="الحالة الصحية" Font-Size="12px" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB14" runat="server" Text="أفراد الاسرة" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                    <asp:CheckBox ID="CB15" runat="server" Text="الراتب" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                </div>
                            </div>
                        </div>
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlPrintAllData" runat="server" Direction="RightToLeft" Visible="False">
                                        <div class="table table-responsive">
                                            <table class='table' style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <div class="HideNow">
                                                                <uc1:WUCHeader runat="server" ID="WUCHeader3" />
                                                            </div>
                                                            <div align="center" class="w">
                                                                <div>
                                                                    <asp:TextBox ID="txtSearchOrphans" runat="server" Text="0" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%;; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GVMostafeed" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                                                    <asp:BoundField DataField="IDItem" HeaderText="IDItem" InsertVisible="False" ReadOnly="True"
                                                                        SortExpression="IDItem" Visible="false" />
                                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="NumberMostafeed" HeaderText="الملف" SortExpression="NumberMostafeed" Visible="false"
                                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                                    <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <span class='<%# Convert.ToBoolean(Eval("_Is_Print_Hide_"))?"redFont":"" %>' style="margin-bottom:3px;"><%# Eval("NameMostafeed") %></span>
                                                                            <%# FCheckHide((bool) (Eval("_Is_Print_Hide_"))) %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="المستفيد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassMosTafeed.FChangColor((string) Eval("TypeMostafeed"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassQuaem.FHalatMostafeed((Int32) Eval("HalafAlMosTafeed"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="رقم السجل" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# Eval("NumberAlSegelAlMadany")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الجوال" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            0<%# Eval("PhoneNumber")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="تاريخ الميلاد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassSaddam.FCheckNullDateMostafeed((String) (ClassDataAccess.FChangeF((DateTime) Eval("dateBrith"))))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassSaddam.FCheckNullDateMostafeedAge((DateTime) (Eval("dateBrith"))) %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="حالة المسكن" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassQuaem.FHalatAlMaskan((Int32) Eval("HaletAlMasken"))%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الحالة الصحية" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# ClassSaddam.FCheckHealthStatus(Convert.ToBoolean(Eval("Saleem")), Convert.ToBoolean(Eval("Moalek")), Convert.ToString(Eval("TypeAleakah")), Convert.ToBoolean(Eval("Mareedh")), Convert.ToString(Eval("TypeAlmaradh"))) %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الافراد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# Eval("AfradAlOsrah")%>  فرد
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الراتب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%# Eval("AlDakhlAlShahryllMostafeed")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC">
                                                                        <ItemTemplate>
                                                                            <a href='PageBeneficiaryByView.aspx?ID=<%# Eval("NumberMostafeed")%>&XID=<%# Eval("IDUniq")%>' title="عرض الملف" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                                            <a href='PageBeneficiaryEdit.aspx?XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"><span class="fa fa-edit"></span></a>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                                <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي "
                                                                    PreviousPageText=" السابق - " />
                                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                                <RowStyle CssClass="rows"></RowStyle>
                                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                <%--<SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                    <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                                                            </asp:GridView>
                                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                            <br />
                                                            <div class="HideEdarah">
                                                                
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th>
                                                            <hr style='border: solid; border-width: 1px; width:100%' />
                                                            <div style="float:right">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                            <div style="float:left">
                                                                <span style="font-size: 12px; padding-right: 5px">عدد القُرى بحسب كبار السن : </span>
                                                                <asp:Label ID="lblCountAitam" runat="server"  Style='color: Red; font-size: 12px'></asp:Label>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                            <div class="container-fluid HideNow" dir="rtl" runat="server">
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
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
                                    <asp:Panel ID="pnlWaiting" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px"> الرجاء تحديد البيانات 
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
                </div>
            </div>
        </asp:Panel>

        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

