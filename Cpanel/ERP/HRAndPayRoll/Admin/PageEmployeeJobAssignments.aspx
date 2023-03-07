<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/HRAndPayRoll/Admin/MPAdmin.master" AutoEventWireup="true" CodeFile="PageEmployeeJobAssignments.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Admin_PageEmployeeJobAssignments" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Confirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" Visible="false"
                title="طباعة">
            <i class="fa fa-print"></i></asp:LinkButton>
            <%--<asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
            <i class="fa fa-trash-o"></i></span></asp:LinkButton>--%>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الرئيسية</a></li>
                <li><a href="PageEmployeeJobAssignments.aspx">ملفات مهام العمل</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i> 
                    <asp:Label ID="lbmsg" runat="server" Text="ملفات مهام العمل"></asp:Label>
                </h3>
                <div align="left" class="hide">
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
                <div class="col-md-2">
                    <div class="form-group">
                            <h4><i class="fa fa-star"></i> المهام</h4>
                    </div>
                    <div class="form-group">
                        <span class='label label-success pull-right' style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountActive" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnActive" runat="server" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-default" ValidationGroup="g2" OnClick="btnActive_Click">
                            سارية 
                        </asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <span class='label label-success pull-right' style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountNew" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnNew" runat="server" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-info Colorloading" ValidationGroup="g2" OnClick="btnNew_Click">
                            جديدة 
                        </asp:LinkButton>
                        
                    </div>
                    <div class="form-group">
                        <span class="label label-success pull-right" style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountLate" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnLate" runat="server" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-info Colorloading" ValidationGroup="g2" OnClick="btnLate_Click">
                            متأخرة 
                        </asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <span class="label label-success pull-right" style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountConvert" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnConvert" runat="server" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-info Colorloading" ValidationGroup="g2" OnClick="btnConvert_Click">
                            محولة 
                        </asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <span class="label label-success pull-right" style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountStoped" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnStoped" runat="server" Text="متوقفة" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-info Colorloading" ValidationGroup="g2" OnClick="btnStoped_Click">
                            متوقفة 
                        </asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <span class="label label-success pull-right" style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountFinshtoday" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnFinshtoday" runat="server" Text="أُنجزت اليوم" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-info Colorloading" ValidationGroup="g2" OnClick="btnFinshtoday_Click">
                            أُنجزت اليوم 
                        </asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <span class="label label-success pull-right" style="position:absolute; left:20px; margin-top:5px;"><asp:Label ID="lblCountDeny" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="btnDeny" runat="server" Text="أُنجزت اليوم" Style="width:100%;" Font-Bold="true"
                        CssClass="btn btn-info Colorloading" ValidationGroup="g2" OnClick="btnDeny_Click">
                            مرفوضة 
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-sm-10">
                    <div class="col-sm-3 hide">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> من تاريخ : </h5>
                            <div class="input-group date ">
                                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 hide">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> إلى تاريخ : </h5>
                        <div class="input-group date ">
                            <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                            <span class="input-group-btn">
                                <button class="btn btn-default" type="button">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
                        </div>
                    </div>
                    <div class="col-sm-5 hide">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> جملة البحث : </h5>
                            <asp:TextBox ID="txtSearch" runat="server" placeholder=" إبحث هنا ... " class="form-control"></asp:TextBox>
                            
                        </div>
                    </div>
                    <div class="col-sm-1 hide">
                        <br />
                        <div class="form-group">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" class="btn btn-info" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                        <div class="table table-responsive" runat="server" dir="rtl" id="pnlprint">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" 
                                                        Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                        Text="قائمة مهام العمل"></asp:TextBox>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVEmpJobAssignmentsActive" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span><br />
                                                            <%# Convert.ToBoolean(Eval("Is_View_"))
                                                                    ? 
                                                                    " <span style='font-size:10px; background-color:#5c9b02; color:#FFF;' class='badge order-status-badge'><i class='fa fa-eye'></i>  فُتحت بتأريخ " + Eval("Date_View_", "{0:dd/MM/yyyy hh:mm:tt}") + "</span> " 
                                                                    :
                                                                    " <span style='font-size:10px; color:#FFF;' class='BackgroundAll badge order-status-badge'><i class='fa fa-envelope'></i>  لم تُفتح </span>"
                                                            %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ت/بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ت/نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ملف المتابعة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <a href='PageEmployeeJobAssignmentDetails.aspx?ID=<%# Eval("EmployeeJobAssignmentID")%>' 
                                                                title="عرض ملف المتابعة" data-toggle="tooltip">
                                                                <i class="fa fa-eye"></i> عرض</a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="تاريخ الموافقة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Allow_", "{0:dd/MM/yyyy HH:mm:tt}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="أُنجزت" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton  ID="LB_End" runat="server" CommandArgument='<%# Eval("EmployeeJobAssignmentID") %>'
                                                                CommandName='<%# Eval("_Phone") %>' ValidationGroup='<%# Eval("Number_Job_") %>'
                                                                class="delete-button" OnClientClick="return Confirmation();" OnClick="LB_End_Click" 
                                                                title="تحديد أنها أنجزت" data-toggle="tooltip">
                                                                <img src='/Img/IconTrue.png' style='width: 20px' />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            
                                            <asp:GridView ID="GVEmpJobAssignmentsNew" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ت/بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ت/نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="موافقة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:LinkButton  ID="LB_Accept" runat="server" CommandArgument='<%# Eval("EmployeeJobAssignmentID") %>'
                                                                CommandName='<%# Eval("_Phone") %>' ValidationGroup='<%# Eval("Number_Job_") %>'
                                                                class="delete-button" OnClientClick="return Confirmation();" OnClick="LB_Accept_Click" 
                                                                title="الموافقة على المهمة" data-toggle="tooltip">
                                                                <img src='/Img/IconTrue.png' style='width: 20px' />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="رفض" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:LinkButton  ID="LB_Deny" runat="server" CommandArgument='<%# Eval("EmployeeJobAssignmentID") %>'
                                                                class="delete-button" OnClientClick="return Confirmation();" OnClick="LB_Deny_Click" 
                                                                CommandName='<%# Eval("_Phone") %>' ValidationGroup='<%# Eval("Number_Job_") %>'
                                                                title="رفض المهمة" data-toggle="tooltip">
                                                                <img src='/Img/IconFalse.png' style='width: 20px' />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            
                                            <asp:GridView ID="GVEmpJobAssignmentsLate" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ت/بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ت/نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            
                                            <asp:GridView ID="GVEmpJobAssignmentsConverted" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ت/بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ت/نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            
                                            <asp:GridView ID="GVEmpJobAssignmentsStoped" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ت/بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ت/نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            
                                            <asp:GridView ID="GVEmpJobAssignmentsFinshtoday" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ت/بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="ت/نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%--<%# ClassSaddam.FCheckStatus((DateTime) (Eval("Date_Job")),(DateTime) (Eval("Date_End_Job")), 
                                                                         (bool) (Eval("Is_Emp_Allow_")),(bool) (Eval("Is_Emp_Deny_")),
                                                                         (bool) (Eval("Is_Raees_Allow_Final_")),(bool) (Eval("Is_Raees_Allow_With_Commant_")),
                                                                         (bool) (Eval("Is_Raees_Lagnat_Not_Allow_")), (bool) (Eval("Is_Stoped_")),
                                                                         (bool) (Eval("Is_End_"))) %>--%>

                                                        <%# ClassSaddam.FCheckStatus((DateTime) (Eval("Date_Job")),(DateTime) (Eval("Date_End_Job")), 
                                                                (bool) (Eval("Is_Emp_Allow_")),(bool) (Eval("Is_Emp_Deny_")),
                                                                (bool) (Eval("Is_Moder_Allow_")),(bool) (false),
                                                                (bool) (Eval("Is_Moder_Not_Allow_")), (bool) (Eval("Is_Stoped_")),
                                                                (bool) (Eval("Is_End_")), (bool) (Eval("Is_Convert_"))) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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

                                            <asp:GridView ID="GVEmpJobAssignmentsDeny" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeJobAssignmentID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False" Visible="false">
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
                                                    <asp:BoundField DataField="Number_Job_" HeaderText="ر/المهمة" SortExpression="Number_Job_"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px"><%# Eval("The_Assignment_")%></span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ بداية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="تاريخ نهاية المهمة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_End_Job_", "{0:dd/MM/yyyy}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="تاريخ الرفض" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Date_Deny_", "{0:dd/MM/yyyy HH:mm:tt}")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                            </table>
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
                </div>
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
</asp:Content>

