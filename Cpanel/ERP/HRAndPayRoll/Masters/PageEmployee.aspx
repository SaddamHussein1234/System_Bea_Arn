<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployee.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Masters_PageEmployee" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            var gv = document.getElementById("<%=GVEmp.ClientID%>");
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

        .redFont{
            color:red;
        }
        .displayNone{
            display:none;
        }
    </style>
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageEmployeeAdd.aspx" data-toggle="tooltip" title="إضافة موظف جديد" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" title="حذف الملفات المحددة" data-toggle="tooltip" 
                        CssClass="btn btn-danger" OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete();" />
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="../../Default.aspx">الرئيسية</a></li>
                        <li><a href="PageEmployee.aspx">قائمة الموظفين </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة الموظفين
                        </h3>
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
                        <asp:Panel ID="pnlData" runat="server" Visible="False">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="row" style="margin: 5px; text-align: center">
                                    <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                                        <br />
                                        <h3 style="font-family: 'Alwatan';">
                                            <asp:Label ID="lblTitle" runat="server" Text="قائمة بيانات الموظفين"></asp:Label>
                                        </h3>
                                        <br />
                                    </div>
                                </div>
                                <div class="table table-responsive">
                                    <asp:GridView ID="GVEmp" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
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
                                            <asp:TemplateField HeaderText="ر.الموظف" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# Eval("EmployeeNo") %>
                                                    <img src='/<%# Eval("Img_Signature_") %>' alt="Lodding ..." width="60" height="30" />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="الإسم" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <span class='<%# Convert.ToBoolean(Eval("IsLeave"))?"redFont":"" %>'><%# Eval("_Name") %></span>
                                                    <br />
                                                    <span  class="redFont" style="font-size:11px; <%# Convert.ToBoolean(Eval("IsLeave"))?"":"display:none;" %>"><i class="fa fa-file"></i> <%# Convert.ToBoolean(Eval("IsLeave"))?"مستقيل":"" %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Department" HeaderText="عنوان الإدارة" SortExpression="Department" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="EmployeeType" HeaderText="طبيعة العمل" SortExpression="EmployeeType" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="Designation" HeaderText="نوع الوظيفة" SortExpression="Designation" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="Shift" HeaderText="نوع الدوام" SortExpression="Shift" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="CountryName" HeaderText="البلد" SortExpression="CountryName" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="EmployeeGrade" HeaderText="فصيلة الدم" SortExpression="EmployeeGrade" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:TemplateField HeaderText="حالة التفعيل" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    الحالة <%# ClassSaddam.FChangeStyleCheckbox((Boolean) Eval("IsActive")) %>
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
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <%--<a href="PageEmployeeAdd.aspx?ID=<%# Eval("EmployeeID") %>" class='<%# Convert.ToBoolean(Eval("IsLeave"))?"displayNone":"" %>'
                                                        data-toggle="tooltip" title="تعديل البيانات">
                                                        <i class="fa fa-edit"></i>
                                                    </a>
                                                    
                                                    <a <%# Convert.ToBoolean(Eval("IsLeave")) ? 
                                                            "href='PageEmployeeResignSave.aspx?ID="+ Eval("EmployeeID") +"&Type=Come' data-toggle='tooltip' title='إلغاء الإستقالة'"
                                                            :
                                                            "href='PageEmployeeResignSave.aspx?ID="+ Eval("EmployeeID") +"&Type=Leave' data-toggle='tooltip' title='إستقالة الموظف'" %>>
                                                        <%# Convert.ToBoolean(Eval("IsLeave"))?"<i class='fa fa-user'></i>":"<i class='fa fa-user-times'></i>" %>
                                                    </a>
                                                    <br />
                                                    <a href="PageEmployeeStartOfWork.aspx?ID=<%# Eval("EmployeeID") %>" data-toggle="tooltip" title="عرض إشعار مباشرة العمل">
                                                        <i class="fa fa-file"></i>
                                                    </a>--%>

                                                    <div class="clearfix pull-left pdn--tt">
                                                        <div class="dropdown pdn--an-imp">
                                                            <button class="dropdown-toggle btn btn-info " data-toggle="dropdown">
                                                                <i class="fa fa-gear"></i>
                                                                <span class="hidden-xs"><b>خيارات</b></span>
                                                                <i class="fa fa-caret-down"></i>
                                                            </button>
                                                            <ul class="dropdown-menu dropdown-menu-left" role="menu" aria-labelledby="خيارات" style="margin:-10px -40px 0 0;">
                                                                <li>
                                                                    <a href="PageEmployeeAdd.aspx?ID=<%# Eval("EmployeeID") %>" class='<%# Convert.ToBoolean(Eval("IsLeave"))?"displayNone":"" %>'
                                                                        data-toggle="tooltip" title="تعديل البيانات">
                                                                        <i class="fa fa-edit"></i> تعديل البيانات
                                                                    </a>
                                                                </li>
                                                                <li>
                                                                    <a <%# Convert.ToBoolean(Eval("IsLeave")) ? 
                                                                            "href='PageEmployeeResignSave.aspx?ID="+ Eval("EmployeeID") +"&Type=Come' data-toggle='tooltip' title='إلغاء الإستقالة'"
                                                                            :
                                                                            "href='PageEmployeeResignSave.aspx?ID="+ Eval("EmployeeID") +"&Type=Leave' data-toggle='tooltip' title='إستقالة الموظف'" %>>
                                                                        <%# Convert.ToBoolean(Eval("IsLeave"))?"<i class='fa fa-user'></i> إلغاء الإستقالة":"<i class='fa fa-user-times'></i> إستقالة الموظف" %>
                                                                    </a>
                                                                </li>
                                                                <li>
                                                                    <a href="PageEmployeeStartOfWork.aspx?ID=<%# Eval("EmployeeID") %>" data-toggle="tooltip" title="عرض إشعار مباشرة العمل">
                                                                        <i class="fa fa-file"></i> عرض إشعار مباشرة العمل
                                                                    </a>
                                                                </li>
                                                                <li>
                                                                    <a href="PageEmployeeAdd.aspx?ID=<%# Eval("EmployeeID") %>&Active=true" data-toggle="tooltip" title="ملفات الموظف">
                                                                        <i class="fa fa-file"></i> ملفات الموظف
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
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
                                </div>
                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                <span class="fa fa-table"></span>

                            </asp:Panel>
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
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
</asp:Content>

