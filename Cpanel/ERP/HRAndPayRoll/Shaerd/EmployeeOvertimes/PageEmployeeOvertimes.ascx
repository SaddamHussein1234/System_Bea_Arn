<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeOvertimes.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeOvertimes_PageEmployeeOvertimes" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
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

    .HideNow {
        display: none;
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
    //-->
</script>

<script type="text/javascript">
    function ConfirmDelete() {
        var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVEmployeeOvertimes.ClientID%>");
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

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="~/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeOvertimeAdd.aspx" data-toggle="tooltip" 
                title="إضافة قرار" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
            <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" title="حذف الملفات المحددة" 
                OnClick="btnDelete_Click"
                data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" />
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                title="طباعة">
            <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click">
            <i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="../../Default.aspx">الرئيسية</a></li>
                <li><a href="PageEmployeeOvertimes.aspx">قائمة قرارات العمل الإضافي </a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i>قائمة قرارات العمل الإضافي
                </h3>
                <div style="float: left">
                    <label class="control-label">
                        الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                    </label>
                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true"
                        Width="100" ValidationGroup="g2" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
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
                <div class="col-sm-9">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> من تاريخ : </h5>
                        <div class="input-group date " style="margin-right: -10px;">
                                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                                <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> إلى تاريخ : </h5>
                        <div class="input-group date " style="margin-right: -10px;">
                            <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                            <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                            <span class="input-group-btn">
                                <button class="btn btn-default" type="button">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
                        </div>
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> جملة البحث : </h5>
                            <asp:TextBox ID="txtSearch" runat="server" placeholder=" إبحث هنا ... " class="form-control"></asp:TextBox>
                            
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <br />
                        <div class="form-group">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" class="btn btn-info" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                    <div class="table table-responsive" runat="server" dir="rtl" id="pnlprint">
                        <table class='table' style="width: 100%">
                            <thead>
                                <tr>
                                    <th>
                                        <div class="HideNow">
                                            <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                        </div>
                                        <div align="center" class="w">
                                            <div>
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" 
                                                    Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                    Text="قائمة قرارات العمل الإضافي"></asp:TextBox>
                                            </div>
                                        </div>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GVEmployeeOvertimes" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeOverTimeMapID"
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
                                                        <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Number_OverTime_" HeaderText="ر/القرار" SortExpression="Number_OverTime_"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="إسم الموظف" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <span style="font-size: 11px"><%# Eval("_Name")%></span>
                                                        <div class="HideThis">
                                                            <%# ClassSaddam.FCheckAllow((bool) (Eval("Is_Raees_Lagnat_Allow_")) , (bool) (Eval("Is_Raees_Lagnat_Not_Allow_")))%>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OverTimeTitle" HeaderText="العنوان" SortExpression="OverTimeTitle"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="المبلغ في اليوم" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("Amount")%> <%# ClassSaddam.FGetMonySa() %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("Total_Amount")%> <%# ClassSaddam.FGetMonySa() %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="عدد الأيام" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("TotalDays")%> / أيام
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText=" في اليوم" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("Hours_In_Day_")%> / ساعة
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="من الساعة" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("Start_Time_")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="إلى الساعة" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("End_Time_")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="فترة العمل" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassSaddam.FChangeDate((DateTime) (Eval("Start_Date_")))%> - 
                                                        <%# ClassSaddam.FChangeDate((DateTime) (Eval("End_Date_")))%>
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
                                                <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate_Add" runat="server" 
                                                            Text='<%# Eval("CreatedDate", "{0:dd/MM/yyyy}") + " " + Eval("CreatedDate", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                                    <ItemTemplate>
                                                        <a href='PageEmployeeOvertimeByView.aspx?IDYear=<%# Eval("FinancialYear_Id_") %>&ID=<%# Eval("Number_OverTime_") %>&IDU=<%# Eval("EmployeeOverTimeMapID")%>' 
                                                            title="عرض الملف" data-toggle="tooltip">
                                                            <i class="fa fa-eye"></i></a> 
                                                        <a href='PageEmployeeOvertimeAdd.aspx?ID=<%# Eval("EmployeeOverTimeMapID")%>' 
                                                            title="تعديل الملف" data-toggle="tooltip">
                                                            <i class="fa fa-edit"></i></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                                    <ItemTemplate>
                                                        <a href='PageEmployeeOvertimeByView.aspx?ID=<%# Eval("EmployeeOverTimeMapID")%>' 
                                                            title="عرض الملف" data-toggle="tooltip">
                                                            <i class="fa fa-eye"></i></a> 
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>
                                        <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                        <br />
                                        <div align="Left" class="HideThis">
                                            <img src='/Img/IconTrue.png' style='width: 20px' />
                                            <span style="font-size: 11px">إطلع</span>
                                            <img src='/Img/IconFalse.png' style='width: 20px' />
                                            <span style="font-size: 11px">لم يطلع</span>
                                        </div>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                        <div class="hide">
                            <div class="container-fluid " dir="rtl" runat="server">
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <uc1:WUCFooterBottomERP runat="server" ID="WUCFooterBottomERP" />
                            </div>
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <div class="HideNow">
                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
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
                        <h3 style="font-size: 20px">إدخل جملة البحث ... 
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
<script type="text/javascript">
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
</script>
<script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>