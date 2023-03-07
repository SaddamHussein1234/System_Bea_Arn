<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageBeneficiaryBySearchComprehensive.aspx.cs" Inherits="CResearchers_CPVillage_PageBeneficiaryBySearchComprehensive" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGetByAlMasder.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

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
            .WidthTex09 {
                float: right;
                Width: 9%;
                padding-right: 5px;
            }

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
                Width: 50%;
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
            .WidthTex09 {
                Width: 95%;
            }

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



        .checkbox label input[type="checkbox"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon {
                opacity: 0;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon {
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr {
                opacity: .5;
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
            var gv = document.getElementById("<%=GVMostafeedByDakhl.ClientID%>");
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

    <script src='../../view/Chart/fusioncharts.js'></script>
    <script src='../../view/Chart/fusioncharts.charts.js'></script>

    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
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
                    <li><a href="PageBeneficiaryBySearchComprehensive.aspx">بحث شامل عن بيانات المستفيدين</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="بحث شامل عن بيانات المستفيدين"></asp:Label>
                    </h3>
                    <div style="float: left">
                        <asp:Button ID="btnGetByAlMasder" runat="server" Text="بحث" Style="margin-right: 4px;"
                            class="btn btn-info btn-fill" OnClick="btnGetByAlMasder_Click" />
                        <asp:LinkButton ID="LBPrintAll" runat="server" class="btn btn-success" data-toggle="tooltip"
                            title="طباعة" OnClick="LBPrintAll_Click">
                            <i class="fa fa-print"></i></asp:LinkButton>
                        <asp:LinkButton ID="LBReafrchAll" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBReafrchAll_Click"
                            title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                            OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                            <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <asp:Panel ID="pnlFilter" runat="server">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlHideFilter" runat="server">
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                نوع المستفيد : 
                                                        <asp:DropDownList ID="DLTypeMostafeed" runat="server" ValidationGroup="g2" CssClass="dropdown" Enabled="false"
                                                            Height="34" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="دائم" Selected="True">دائم</asp:ListItem>
                                                            <asp:ListItem Value="مستبعد">مستبعد</asp:ListItem>
                                                        </asp:DropDownList>
                                                <hr />
                                                الراتب : 
                                                    <asp:TextBox ID="txtMasderAlDkhalMinimum" runat="server" class="form-control2" Text="0" ValidationGroup="g2" Width="100%" Height="30" placeholder="الحد الادنى .." TextMode="Number" Style="text-align: center; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                    ControlToValidate="txtMasderAlDkhalMinimum" ErrorMessage="* إدخل قيمة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <br />
                                                <i class="fa fa-minus"></i>
                                                <br />
                                                <asp:TextBox ID="txtMasderAlDkhalMaxiMam" runat="server" class="form-control2" Text="20000" ValidationGroup="g2" Width="100%" Height="30" placeholder="الحد الاعلى .." TextMode="Number" Style="text-align: center; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtMasderAlDkhalMaxiMam" ErrorMessage="* إدخل قيمة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب القرية : </h5>
                                                <asp:CheckBoxList ID="CBAlQariah" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox" Width="100%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب مصدر الدخل : </h5>
                                                <asp:CheckBoxList ID="CBLMasderAlDakhl" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب حالات الاسر : </h5>
                                                <asp:CheckBoxList ID="CBFamliyCases" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب نوع المسكن : </h5>
                                                <asp:CheckBoxList ID="CBAccommodationType" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب حالة المسكن : </h5>
                                                <asp:CheckBoxList ID="CBHousingStatus" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> عرض المستفيدين : </h5>
                                                <asp:CheckBoxList ID="CMMostafeed" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <hr />
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> فلترة العرض : </h5>
                                                <asp:CheckBox ID="CB3" runat="server" Text="رقم الملف" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB4" runat="server" Text="الإسم" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB6" runat="server" Text="حالات الاسر" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB7" runat="server" Text="القرية" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB8" runat="server" Text="رقم السجل" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB9" runat="server" Text="الجوال" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB10" runat="server" Text="تاريخ الميلاد" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB11" runat="server" Text="العمر" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB12" runat="server" Text="حالة المسكن" Font-Size="12px" />
                                                <asp:CheckBox ID="CB13" runat="server" Text="الحالة الصحية" Font-Size="12px" />
                                                <asp:CheckBox ID="CB14" runat="server" Text="أفراد الاسرة" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB15" runat="server" Text="الراتب" Font-Size="12px" Checked="true" />
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <asp:TextBox Visible="false" ID="txtSearchByFilter" runat="server" Style="direction: ltr" placeholder="Filter The Search ... "
                                                CssClass="form-control" TextMode="MultiLine" Rows="6" Width="100%" type="password"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlPrintAllData" runat="server" Direction="RightToLeft" Visible="False">
                                    <div class="table table-responsive">
                                        <table class='table' style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <div class="HideNow">
                                                            <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                        </div>
                                                        <div align="center" class="col-lg-11">
                                                            <div>
                                                                <asp:TextBox ID="txtSearchMostafeed" runat="server" class="form-control" placeholder="عنوان البحث" Text="قائمة بيانات المستفيدين" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 HideThis">
                                                                <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GVMostafeedByDakhl" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                                                <asp:BoundField DataField="NumberMostafeed" HeaderText="ملف" SortExpression="NumberMostafeed" Visible="false"
                                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                                <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <span style="font-size: 11px"><%# Eval("NameMostafeed")%></span>
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
                                                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
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
                                                        <div>
                                                            <div class="container-fluid" dir="rtl" runat="server">
                                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <div class="WidthMaglis24" align="center" runat="server" visible="false">
                                                                                الباحث الإجتماعي
                                                                                    <br />
                                                                                <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                                                                                <br />
                                                                                <asp:Label ID="lblAlBaheth" runat="server" Font-Size="11px"></asp:Label>
                                                                                <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%"
                                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                                                                مدير الجمعية
                                                                                    <br />
                                                                                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                                <br />
                                                                                <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                                                <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah_SelectedIndexChanged"
                                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                                                                رئيس لجنة البحث الإجتماعية
                                                                                    <br />
                                                                                <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                                                                                <br />
                                                                                <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Font-Size="20px"></asp:Label>
                                                                                <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesLagnatAlBahath_SelectedIndexChanged"
                                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                                                                رئيس مجلس الإدارة
                                                                                    <br />
                                                                                <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                                <br />
                                                                                <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                                                <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah_SelectedIndexChanged"
                                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="WidthMaglis24" align="center">
                                                                                <div runat="server" id="IDKhatm" align="left" style="margin-top: 5px">
                                                                                    <img src="../../ImgSystem/ImgSignature/الختم.png" />
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <th>
                                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                                        <div style="float: right">
                                                            <span style="font-size: 12px; padding-right: 5px">عدد الأسر : </span>
                                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </div>
                                                        <div style="float: left; display: none">
                                                            <span style="font-size: 12px; padding-right: 5px">عدد القرى : </span>
                                                            <asp:Label ID="lblCountQriah" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </div>
                                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                                    </th>
                                                </tr>
                                            </tfoot>
                                        </table>
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
                                        <h3 style="font-size: 20px">يرجى تحديد البيانات
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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

