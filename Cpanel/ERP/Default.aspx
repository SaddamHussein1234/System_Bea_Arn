<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_ERP_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="content">
    <div class="page-header">
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الرئيسية</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <%--<div class="row" style="margin: 5px; text-align: center">
            <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; background-color: #8b0101; color: #f5f5f5">
                <h3 style="font-family: 'Alwatan';">نسخة تجريبية
                </h3>
            </div>
        </div>--%>
        <div class="col-sm-12">
            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                <span class="badge badge-pill badge-warning">تحذير</span>
                <span>يرجى تفعيل المصادقة الثنائية لزيادة حماية حسابك 
                    <a href="<%=ResolveUrl("~/Cpanel/CHome/PageGoogleAuthenticator.aspx")%>">من هنا ... </a>
                </span>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </div>
        <div class="row" style="margin: 5px; text-align: center">
            <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                <br />
                <h3 style="font-family: 'Alwatan';">نظام الموارد البشرية - HR System
                </h3>
                <br />
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/HRAndPayRoll/Masters/PageDepartment.aspx")%>' data-toggle="tooltip" title="الإدارات">
                <div class="tile tile-heading" style="border-radius:8px">
                    <div class="tile-body">
                        <h4>عدد الإدارات : 
                            <asp:Label ID="lblCountDepartment" runat="server"></asp:Label>
                        </h4>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/HRAndPayRoll/Masters/PageEmployee.aspx")%>' data-toggle="tooltip" title="الموظفين">
                <div class="tile tile-heading" style="border-radius:8px">
                    <div class="tile-body">
                        <h4>عدد الموظفين : 
                            <asp:Label ID="lblCountEmp" runat="server"></asp:Label>
                        </h4>
                    </div>
                </div>
            </a>
        </div>
        <div class="clearfix"></div>
        <div class="col-lg-6 col-md-3 col-sm-6" runat="server" visible="true">
			<iframe src="HRAndPayRoll/Chart/PageMonthlyStipend.aspx" height="400" width="100%"></iframe>
        </div>
        <div class="col-lg-6 col-md-3 col-sm-6" runat="server" visible="true">
			<iframe src="HRAndPayRoll/Chart/PageChartDepartment.aspx" height="400" width="100%"></iframe>
        </div>
        <div class="clearfix"></div>
        <div class="col-md-6">
            <div class="widget box">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        أعياد الميلاد
                    </h3>
                </div>
                <div class="widget-content">
                    <asp:GridView ID="gvUpcomingBirthday" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvUpcomingBirthday">
                        <Columns>
                            <asp:BoundField HeaderText="الإسم" DataField="_Name" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="تاريخ الميلاد" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Eval("BirthDate", "{0:dd/MM/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle BackColor="#F9F9F9" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="widget box">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        الاجازات
                    </h3>
                </div>
                <div class="widget-content">
                    <asp:GridView ID="gvUpcomingHoliday" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvUpcomingHoliday">
                        <Columns>
                            <asp:BoundField HeaderText="العنوان" DataField="Title" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="الفترة" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Eval("StartDate", "{0:MM/dd/yyyy}") %> -  <%# Eval("EndDate", "{0:MM/dd/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle BackColor="#F9F9F9" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <hr /><hr /><hr />
        </div>
    </div>
</asp:Content>

