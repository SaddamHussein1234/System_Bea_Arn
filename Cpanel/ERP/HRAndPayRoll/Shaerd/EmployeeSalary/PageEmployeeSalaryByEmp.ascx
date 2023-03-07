<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeSalaryByEmp.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeSalary_PageEmployeeSalaryByEmp" %>

<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>

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
            var gv = document.getElementById("<%=gvEmployeeCompletedSalaryProcess.ClientID%>");
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
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click" title="طباعة">
            <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-danger" Visible="false"
                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
            <i class="fa fa-trash-o"></i></span></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="../../Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeeSalaryByEmp.aspx">رواتب الموظف </a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="رواتب الموظف"></asp:Label>
            </h3>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
                <div class="widget-box">
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
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label" id="Label1" runat="server">
                                    الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="ddlYears" ValidationGroup="g2"
                                    CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد السنة" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div runat="server" id="IDDepartment" class="col-md-4">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblTitleDepartment" runat="server" Text="حدد الإدارة : " CssClass="control-label"></asp:Label>
                                    <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblDepartment" Visible="false" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="IDEmployee" class="col-md-4">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblTitleEmployee" runat="server" Text="حدد الموظف : " CssClass="control-label"></asp:Label>
                                    <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <div class="col-md-12">
                                    <asp:HiddenField ID="HFIDEmployee" runat="server" Value="0" ClientIDMode="Static" />
                                    <asp:Label ID="lblEmployee" Visible="false" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown" 
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                        Width="100%" ValidationGroup="g2">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                        <div class="table table-responsive" runat="server" dir="rtl" id="pnlprint">
                            <div class="HideNow">
                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                            </div>
                            <table style="width: 100%">
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إسم الموظف
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                </td>
                                                       
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                                إدارة
                                                </td>
                                                    <td style="width: 25%; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblManagment" runat="server"></asp:Label>
                                                </td>
                                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    رقم الموظف
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblPFNumber" runat="server"></asp:Label>
                                                </td>
                                                       
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    تاريخ الإنضمام
                                                </td>
                                                    <td style="width: 25%; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                                                </td>
                                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الجنس
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ الميلاد
                                                </td>
                                                <td style="width: 25%; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                                    
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="0" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvEmployeeCompletedSalaryProcess" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeId"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
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
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="الشهر" DataField="Month" HeaderStyle-ForeColor="#CCCCCC" />

                                                    <asp:BoundField HeaderText="الراتب الأساسي" DataField="PaidBasic" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField HeaderText="البدلات" DataField="PaidTotalEarning" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField HeaderText="المستقطع" DataField="PaidTotalDeduction" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField HeaderText="السلفة" DataField="PaidLoanAmount" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField HeaderText="الحسومات" DataField="Amount_Resolved" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField HeaderText="إجمالي الراتب" DataField="PaidTotalSalary" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="تاريخ التسليم" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaidDate" runat="server" Text='<%# Eval("PaidDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="التوقيع" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <img src='/<%# Eval("EndDate")%>' width='100px' height='30' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>            
                                                            <a href='PageEmployeeSalaryByView.aspx?Monthyear=<%# Eval("Month").ToString().Replace("/","_") %>&Employeeid=<%#Eval("EmployeeId")%>&IDYear=<%# ddlYears.SelectedValue %>'
                                                                class="btnView"><i class="fa fa-eye" title="Click to View Employee Salary Process"></i></a>
                                                        </ItemTemplate>
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
                                            <asp:HiddenField ID="hfCount" runat="server" />                                                   
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                <tr>
                                    <th>
                                        <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    </th>
                                </tr>
                            </tfoot>
                            </table>
                            <div>
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
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
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
                    <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div align="center">
                            <h3 style="font-size: 20px">يرجى تحديد البيانات ... 
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
<script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>