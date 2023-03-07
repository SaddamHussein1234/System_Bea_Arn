<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageRemamber.aspx.cs" Inherits="Cpanel_ERP_CRM_PageRemamber_PageRemamber" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>


<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            var gv = document.getElementById("<%=GVRemamberAll.ClientID%>");
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
    </style>
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip"
                        title="طباعة" OnClick="btnPrint_Click">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:Button ID="btnDelete" runat="server" Text="إلغاء المحددة" title="إلغاء المحددة"
                        data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageRemamber.aspx">رسائل التذكير</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="float: right">
                            <i class="fa fa-list"></i>قائمة رسائل التذكير 
                            <asp:RadioButton ID="RBAll" runat="server" Text="جميع الرسائل" GroupName="GCheck" Style="font-size: 13px;" />
                            <asp:RadioButton ID="RBIsCaming" runat="server" Text="التنبيهات القادمة" Checked="true" GroupName="GCheck" Style="font-size: 13px;" />
                            <asp:RadioButton ID="RBIsBriv" runat="server" Text="تنبيهات سابقة" GroupName="GCheck" Style="font-size: 13px;" />
                        </h3>
                        <div class="panel-title" align="left" style="margin: -5px 0 -5px 0" dir="rtl">
                            <asp:Button ID="btnGet" runat="server" Text="جلب البيانات" class="btn btn-info pull-right" ValidationGroup="g2" OnClick="btnGet_Click" />
                            <div class="col-md-2">
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtStartDate" runat="server" class="form-control" ValidationGroup="g2" data-date-format="YYYY-MM-DD" placeholder=" من تاريخ ... "
                                        Style="text-align: center"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                        ControlToValidate="txtStartDate" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtEndDate" runat="server" class="form-control" ValidationGroup="g2" placeholder=" إلى تاريخ ... " data-date-format="YYYY-MM-DD"
                                        Style="text-align: center"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtEndDate" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div runat="server" visible="false">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthTextSearch2" placeholder=" إبحث هنا ... " Height="31px"></asp:TextBox>
                                &nbsp;
                             <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info" data-toggle="tooltip" ValidationGroup="g2"
                                 title="بحث">
                            <i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-warning">تحذير</span>
                                <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <asp:Panel ID="pnlData" runat="server" Visible="false" Direction="RightToLeft">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                <div class="HideNow">
                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                </div>
                                <div align="center" class="w">
                                    <div>
                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                            Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                            Text="قائمة أنواع شركات الدعم"></asp:TextBox>
                                    </div>
                                </div>
                                <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                    <thead>
                                        <tr class="th">
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVRemamberAll" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                                    Width="100%" CssClass="footable1"
                                                    EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
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
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="_Company_Name_" HeaderText="إسم الداعم" SortExpression="_Company_Name_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:BoundField DataField="_Phone_Number1_" HeaderText="رقم الهاتف" SortExpression="_Phone_Number1_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="تاريخ التنبيه">
                                                            <ItemTemplate>
                                                                <%# ClassSaddam.FChangeDate((DateTime) (Eval("_Remamber_Date_")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="_Remamber_Desc_" HeaderText="رسالة التنبيه" SortExpression="_Remamber_Desc_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="الحالة">
                                                            <ItemTemplate>
                                                                <%# ClassSaddam.FCheckDateAgoList((DateTime) (Eval("_Remamber_Date_"))) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlBaheth((Int32) (Eval("CreatedBy")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                                            <ItemTemplate>
                                                                <a href="PageRemamberAdd.aspx?ID=<%# Eval("_ID_Company_") %>" data-toggle="tooltip" title="عرض رسائل التذكير">
                                                                    <i class="fa fa-file"></i>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي " PreviousPageText=" السابق - " />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="9">
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <span class="fa fa-table"></span>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="HideNow">
                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                    </div>
                                </div>
                            </div>
                            </asp:Panel>
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
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">حدد التاريخ
                                </h3>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript"><!--
    $('.date').datetimepicker({
        pickTime: false
    });

    $('.time').datetimepicker({
        pickDate: false
    });

    $('.datetime').datetimepicker({
        pickDate: true,
        pickTime: true
    });
    //--></script>
        <script type="text/javascript"><!--
    $('#language a:first').tab('show');
    $('#option a:first').tab('show');
    //--></script>
</asp:Content>

