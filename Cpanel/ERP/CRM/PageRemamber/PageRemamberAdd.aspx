<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageRemamberAdd.aspx.cs" Inherits="Cpanel_ERP_CRM_PageRemamber_PageRemamberAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btn_Add_To_.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

    <script type="text/javascript">
        function ConfirmDelete() {
            if (confirm("هل تريد إلغاء الامر ستفقد البيانات التي قمت بكتابتها حالياً ؟") == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBRefrsh" runat="server" data-toggle="tooltip" title="تحديث" OnClick="LBRefrsh_Click"
                        class="btn btn-info"> <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageRemamber.aspx">رسائل التذكير</a></li>
                    <li><a href="PageRemamberAdd.aspx">إضافة تذكير</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        إضافة رسالة تذكير
                    </h3>
                </div>
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
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid2">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>حدد الشركة :
                                        </h5>
                                        <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="g2" AutoPostBack="true" OnSelectedIndexChanged="DLCompany_SelectedIndexChanged"
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLCompany" ErrorMessage="* حدد الشركة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                                    <br />
                                    <hr />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div align="center">
                                        <h3 style="font-size: 18px">يرجى تحديد الداعم المراد عمل رسالة تذكير له ...
                                        </h3>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="Pnl_Account" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        إضافة تذكير للداعم : 
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid">
                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <h5>تاريخ التذكير : <span class="required">*</span></h5>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtRemamberDate" runat="server" class="form-control" ValidationGroup="VGAddRemamber" data-date-format="YYYY-MM-DD" placeholder=" حدد التاريخ ... "
                                                Style="text-align: center"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                ControlToValidate="txtRemamberDate" ErrorMessage="* تاريخ التذكير" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="VGAddRemamber" Font-Size="10px"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <h5>رسالة التذكير : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtDesc" runat="server" class="form-control" ValidationGroup="VGAddRemamber"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtDesc" ErrorMessage="* رسالة التذكير" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAddRemamber" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-12" align="left">
                                        <br />
                                        <asp:Button ID="btn_Add_To_" runat="server" Text="إضافة تذكير" CssClass="btn btn-info"
                                            ValidationGroup="VGAddRemamber" OnClick="btn_Add_To__Click" />
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <asp:Panel ID="pnlData_To" runat="server" Visible="false" CssClass="alert alert-info">
                                        <asp:Repeater ID="RPT_Remamber_" runat="server">
                                            <ItemTemplate>
                                                <div class="alert alert-<%# Library_CLS_Arn.Saddam.ClassSaddam.FCheckDateAgo((DateTime) (Eval("_Remamber_Date_"))) %> alert-dismissible" role="alert">
                                                    <span class="badge badge-pill badge-success">رسالة تذكير</span>
                                                    تم إنشاء تذكير في تاريخ : 
                                                     <%# Library_CLS_Arn.Saddam.ClassSaddam.FChangeDate((DateTime) (Eval("_Remamber_Date_"))) %>

                                                    <asp:LinkButton ID="LBDelete" runat="server" CssClass="close"
                                                        OnClick="LBDelete_Click" CommandArgument='<%# Eval("_ID_Item_") %>'
                                                        OnClientClick="return confirm('هل تريد الإستمرار ؟ ');" Style="color: #b10505"
                                                        data-toggle='tooltip' title='حذف العملية'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </div>
                                                
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNull_To" runat="server" Visible="false" CssClass="alert alert-warning">
                                        <div align="center">
                                            <br />
                                            <div class="alert  alert-warning alert-dismissible" role="alert">
                                                <span class="badge badge-pill badge-warning">تحذير</span>
                                                لا يوجد تذكير للداعم الحالي حالياً !!! 
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>


                                <div class="clearfix"></div>
                                <hr />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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

