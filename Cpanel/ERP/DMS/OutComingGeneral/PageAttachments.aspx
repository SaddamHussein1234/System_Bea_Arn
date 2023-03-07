<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/DMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAttachments.aspx.cs" Inherits="Cpanel_ERP_DMS_OutComingGeneral_PageAttachments" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a runat="server" id="ID_Edit_" class="btn btn-info" visible="false" data-toggle="tooltip" title="تعديل الخطاب">الذهاب إلى وضع التعديل <span class="fa fa-edit"></span></a>
                        &nbsp;
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="/Cpanel/ERP/DMS/">الرئيسية</a></li>
                    <li><a href="">إضافة المرفقات</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة المرفقات"></asp:Label>
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
                            <span id="lblSuccess" runat="server"></span>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h5>الخطاب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLOutComing_General" runat="server" ValidationGroup="GVImg" Width="500" AutoPostBack="true"
                                            CssClass="form-control2 chzn-select dropdown" OnSelectedIndexChanged="DLOutComing_General_SelectedIndexChanged"
                                            Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="DLOutComing_General" ErrorMessage="* حدد الخطاب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GVImg" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>عنوان الملف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txt_Title" runat="server" class="form-control" ValidationGroup="GVImg"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txt_Title" ErrorMessage="* عنوان الملف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GVImg" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>حدد الملف
                                        </h5>
                                        <asp:FileUpload ID="FUFiles" runat="server" AllowMultiple="false" ValidationGroup="GVImg" />
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="FUFiles" ErrorMessage="* حدد الملف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GVImg" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:LinkButton ID="LBImage" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="رفع الملف"
                                            ValidationGroup="GVImg" OnClick="LBImage_Click">
                                        رفع الملف <span class="tip-bottom"><i class="fa fa-upload" style="font-size:16px"></i></span></asp:LinkButton>
                                        <asp:LinkButton ID="LBView" runat="server"  ValidationGroup="GBill"  OnClick="LBView_Click" data-toggle="tooltip" title="عرض الخطاب"
                                                class="btn btn-info">عرض الخطاب <span class="tip-bottom"><i class="fa fa-eye" style="font-size:16px"></i></span></asp:LinkButton>
                                            <asp:LinkButton ID="LB_Back" runat="server"  OnClick="LB_Back_Click"
                                                class="btn btn-danger">خروج</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-12" runat="server" id="IDTable" visible="false">
                                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                        <thead>
                                            <tr class="th">
                                                <th class="StyleTD">م</th>
                                                <th class="StyleTD">عنوان الملف</th>
                                                <th class="StyleTD">نوع الملف</th>
                                                <th class="StyleTD">حجم الملف</th>
                                                <th class="StyleTD">بتاريخ</th>
                                                <th class="StyleTD">العرض</th>
                                                <th class="StyleTD"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RPTFiles" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 10px;" class="StyleTD">
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                        </td>
                                                        <td class="StyleTD">
                                                            <span style="font-size: 12px"><%# Eval("_The_Title_")%></span>
                                                        </td>
                                                        <td class="StyleTD">
                                                            <span style="font-size: 12px"><%# Eval("_Type_File_").ToString().Replace(".", "") %></span>
                                                        </td>
                                                        <td class="StyleTD">
                                                            <span style="font-size: 12px"><%# ClassSaddam.FormatSize(Convert.ToInt32(Eval("_Size_File_"))) %></span>
                                                        </td>
                                                        <th class="StyleTD th">
                                                            <span style="font-size: 12px"><%# Eval("_CreatedDate_", "{0:MM/dd/yyyy}") %></span>
                                                        </th>
                                                        <td class="StyleTD">
                                                            <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNullFile(Eval("_Src_").ToString()) %>"
                                                                href="/<%# Eval("_Src_") %>" data-file="pdf" data-fancybox data-type="iframe" title="عرض الملف" data-toggle="tooltip">
                                                                <i class="fas fa-file-pdf"></i>
                                                                <div>
                                                                    <span>عرض الملف </span><small><%# ClassSaddam.FGetTypeFileOutTitle((string)Eval("_Type_File_")) %> </small>
                                                                </div>
                                                            </a>
                                                            <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNotNullFile(Eval("_Src_").ToString()) %>" data-file="pdf">
                                                                <i class="fas fa-file-pdf"></i>
                                                                <div><span>بدون ملف مرفق</span></div>
                                                            </a>
                                                        </td>
                                                        <td class="StyleTD">
                                                            <asp:LinkButton ID="LBDelete" runat="server" OnClientClick="return insertConfirmation();"
                                                                OnClick="LBDelete_Click" title="حذف" data-toggle="tooltip"
                                                                CommandArgument='<%# Eval("_ID_Item_") %>'><i class="fa fa-trash"></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th colspan="9">
                                                    <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لا توجد ملفات بعد !!!
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <asp:HiddenField ID="HFIDStore" runat="server" />
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

        <link href="../jquery.fancybox.min.css" rel="stylesheet" />
        <script src="../jquery.fancybox.min.js"></script>
        <asp:HiddenField ID="HFIDYears" runat="server" />
        <asp:HiddenField ID="HFIDNumber" runat="server" />
</asp:Content>

