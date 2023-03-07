<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryBySearchBoysComprehensive.aspx.cs" Inherits="Cpanel_PageBeneficiaryBySearchBoysComprehensive" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>


<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
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

    <script src='../view/Chart/fusioncharts.js'></script>
    <script src='../view/Chart/fusioncharts.charts.js'></script>

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
                    <li><a href="PageBeneficiaryBySearchBoysComprehensive.aspx">بحث شامل عن بيانات أفراد أسرة المستفيدين</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="بحث شامل عن بيانات أفراد أسرة المستفيدين"></asp:Label>
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
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب القرية : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBAlQariah" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب القرابة : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBQarabah" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> المستوى الدراسي : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBAlmostawaAlDerasy" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem Value="01">الأول الابتدائي</asp:ListItem>
                                                        <asp:ListItem Value="02">الثاني الابتدائي</asp:ListItem>
                                                        <asp:ListItem Value="03">الثالث الابتدائي</asp:ListItem>
                                                        <asp:ListItem Value="04">الرابع الابتدائي</asp:ListItem>
                                                        <asp:ListItem Value="05">الخامس الابتدائي</asp:ListItem>
                                                        <asp:ListItem Value="06">السادس الابتدائي</asp:ListItem>
                                                        <asp:ListItem Value="07">الأول المتوسط</asp:ListItem>
                                                        <asp:ListItem Value="08">الثاني المتوسط</asp:ListItem>
                                                        <asp:ListItem Value="09">الثالث المتوسط</asp:ListItem>
                                                        <asp:ListItem Value="10">الأول الثانوي</asp:ListItem>
                                                        <asp:ListItem Value="11">الثاني الثانوي</asp:ListItem>
                                                        <asp:ListItem Value="12">الثالث الثانوي</asp:ListItem>
                                                        <asp:ListItem Value="13">جامعة</asp:ListItem>
                                                        <asp:ListItem Value="00">غير ذلك</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> العام الدراسي : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBYearStudy" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> حسب المهنة : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBAlMehnah" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem Value="رب منزل">رب منزل</asp:ListItem>
                                                        <asp:ListItem Value="ربة منزل">ربة منزل</asp:ListItem>
                                                        <asp:ListItem Value="موظف">موظف</asp:ListItem>
                                                        <asp:ListItem Value="موظفة">موظفة</asp:ListItem>
                                                        <asp:ListItem Value="طالب">طالب</asp:ListItem>
                                                        <asp:ListItem Value="طالبة">طالبة</asp:ListItem>
                                                        <asp:ListItem Value="عاطل">عاطل</asp:ListItem>
                                                        <asp:ListItem Value="عاطلة">عاطلة</asp:ListItem>
                                                        <asp:ListItem Value="طفل">طفل</asp:ListItem>
                                                        <asp:ListItem Value="طفلة">طفلة</asp:ListItem>
                                                        <asp:ListItem Value="غير ذلك">غير ذلك</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> الحالة الصحية : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBAlHalahAlSehe" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem Value="سليم">سليم</asp:ListItem>
                                                        <asp:ListItem Value="سليمة">سليمة</asp:ListItem>
                                                        <asp:ListItem Value="مريض">مريض</asp:ListItem>
                                                        <asp:ListItem Value="مريضة">مريضة</asp:ListItem>
                                                        <asp:ListItem Value="غير ذلك">غير ذلك</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> عرض المستفيدين : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CMMostafeed" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i> فلترة العرض : </h5>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBox ID="CB3" runat="server" Text="رقم الملف" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB4" runat="server" Text="الإسم" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB6" runat="server" Text="القرابة" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB7" runat="server" Text="القرية" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB8" runat="server" Text="رقم السجل" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB9" runat="server" Text="الجوال" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB10" runat="server" Text="تاريخ الميلاد" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB11" runat="server" Text="العمر" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB12" runat="server" Text="المهنة" Font-Size="12px" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB13" runat="server" Text="العام الدراسي" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB14" runat="server" Text="المستوى الدراسي" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                    <asp:CheckBox ID="CB15" runat="server" Text="الحالة الصحية" Font-Size="12px" Checked="true" CssClass="styled" Width="100" />
                                                </div>
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
                                                                <asp:TextBox ID="txtSearchMostafeed" runat="server" class="form-control" placeholder="عنوان البحث" Text="قائمة بيانات أفراد أسرة المستفيدين" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
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
                                                                <asp:BoundField DataField="NumberMostafed" HeaderText="ملف" SortExpression="NumberMostafed" Visible="false"
                                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                                <asp:TemplateField HeaderText="الإسم" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <span style="font-size: 11px"><%# Eval("Name")%></span>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ولي الأمر" HeaderStyle-ForeColor="#CCCCCC">
                                                                    <ItemTemplate>
                                                                        <span style="font-size: 11px"><%# Eval("NameMostafeed")%></span>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="القرابة" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassQuaem.FQarabah((Int32) Eval("AlQarabah"))%>
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
                                                                        <%# Eval("A2")%>
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
                                                                        <%# ClassSaddam.FCheckNullDate(ClassDataAccess.FChangeF((DateTime) Eval("DateBrith")))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassSaddam.FGetAge((DateTime) (Eval("DateBrith")))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="AlMehnahAlHaliah" HeaderText="المهنة الحالية" SortExpression="AlMehnahAlHaliah" Visible="false"
                                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                                <asp:BoundField DataField="A1" HeaderText="العام الدراسي" SortExpression="A1" Visible="false"
                                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                                <asp:TemplateField HeaderText="المستوى الدراسي" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassSaddam.FCheckAlmostawaAlDerasy((String) (Eval("AlmostawaAlDerasy")))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="AlHalahAlseHeyah" HeaderText="الحالة الصحية" SortExpression="AlHalahAlseHeyah" Visible="false"
                                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                                <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC">
                                                                    <ItemTemplate>
                                                                        <a href='PageBeneficiaryAddBoys.aspx?ID=<%# Eval("NumberMostafed")%>&XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                                            class="btn btn-primary"><span class="fa fa-edit"></span></a>
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
                                                        <div>
                                                            <div class="container-fluid" dir="rtl" runat="server">
                                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                                <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
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
                                                            <span style="font-size: 12px; padding-right: 5px">عدد الأفراد : </span>
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
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

