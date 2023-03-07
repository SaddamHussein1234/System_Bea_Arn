<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageBeneficiaryRegistrationAddBuy.aspx.cs" Inherits="ar_PageBeneficiaryRegistrationAddBuy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../MultiForm3/assets/css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body onload="window.scroll(0, 340 )">
        <div class="main_body">
            <!-- Begin page content -->
            <div class="page-header">
                <div class="container">
                    <span>بيانات أفراد الإسرة
                    </span>
                </div>
            </div>
            <div class="page_body">
                <div class="page_content container">
                    <asp:Panel ID="pnlMessage" runat="server">
                        <div runat="server" id="IDBuy">
                            <div class="form-top">
                                <div class="form-top-left">
                                    <h3>خطوة 5 / 5</h3>
                                    <p>
                                        <asp:Label ID="lbmsg" runat="server" Text="إضافة بيانات أفراد الإسرة :"></asp:Label>
                                    </p>
                                    <p runat="server" visible="false">
                                        <asp:TextBox ID="txtCountBoys" runat="server" ValidationGroup="g2" Width="150" Text="0" CssClass=""></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator28" runat="server"
                                            ControlToValidate="txtCountBoys" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtCountBoys"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                        <asp:LinkButton ID="LBUpdate" runat="server" data-toggle="tooltip" title="تحديث عدد أفراد الاسرة" OnClick="LBUpdate_Click" ValidationGroup="g2"><span class="tip-bottom"><i class="fa fa-edit" style="font-size:16px"></i></span></asp:LinkButton>
                                    </p>
                                </div>
                                <div class="form-top-right">
                                    <i class="fa fa-group"></i>
                                </div>
                            </div>
                            <div class="form-bottom">
                                <div class="container-fluid">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                الاسم :
                                            </label>
                                            <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>
                                            <i class="fa fa-minus"></i>
                                           <label> ملف  : </label>
                                            <asp:Label ID="lblFile" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                نوع المستفيد :
                                            </label>
                                            <asp:Label ID="lblTypeMostafeed" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                القرية :
                                            </label>
                                            <asp:Label ID="lblAlQariah" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                الجنس :
                                            </label>
                                            <asp:Label ID="lblGender" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                رقم الهاتف :
                                            </label>
                                            0<asp:Label ID="lblPhone" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                السجل المدني :
                                            </label>
                                            <asp:Label ID="lblNumberSigal" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <hr style="width: 100%" />
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                الاسم : <span style="color: red">*</span>
                                            </label>
                                            <asp:TextBox ID="txtName" runat="server" class="form-control" ValidationGroup="g1"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator24" runat="server"
                                                ControlToValidate="txtName" ErrorMessage="* إدخل الإسم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                القرابة : <span style="color: red">*</span>
                                            </label>
                                            <asp:DropDownList ID="DLAlQarabah" runat="server" ValidationGroup="g1" CssClass="form-control" Style="font-size: 13px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator25" runat="server"
                                                ControlToValidate="DLAlQarabah" ErrorMessage="* إدخل القرابة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                المستوى الدراسي : <span style="color: red">*</span>
                                            </label>
                                            <asp:DropDownList ID="txtAlmostawaAlDerasy" runat="server" ValidationGroup="g1" CssClass="form-control" Style="font-size: 13px;">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">الأول الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="2">الثاني الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="3">الثالث الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="4">الرابع الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="5">الخامس الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="6">السادس الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="7">الأول المتوسط</asp:ListItem>
                                                <asp:ListItem Value="8">الثاني المتوسط</asp:ListItem>
                                                <asp:ListItem Value="9">الثالث المتوسط</asp:ListItem>
                                                <asp:ListItem Value="10">الأول الثانوي</asp:ListItem>
                                                <asp:ListItem Value="11">الثاني الثانوي</asp:ListItem>
                                                <asp:ListItem Value="12">الثالث الثانوي</asp:ListItem>
                                                <asp:ListItem Value="13">جامعة</asp:ListItem>
                                                <asp:ListItem Value="0">غير ذلك</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator30" runat="server"
                                                ControlToValidate="txtAlmostawaAlDerasy" ErrorMessage="* المستوى الدراسي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                العام الدراسي :
                                            </label>
                                            <asp:DropDownList ID="DLYearStudy" runat="server" CssClass="form-control" Style="font-size: 13px;">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                رقم السجل المدني : <span style="color: red">*</span>
                                            </label>
                                            <asp:TextBox ID="txtNumberSigal" runat="server" class="form-control" ValidationGroup="g1"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtNumberSigal" ErrorMessage="* رقم السجل" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberSigal"
                                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g1" Font-Size="10px"
                                                Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                تاريخ الميلاد (هجري) : 
                                            </label>
                                            <table>
                                                <tr>
                                                    <td style="padding-left: 3px; width: 50%">
                                                        <asp:DropDownList ID="ddlYearsH" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding-left: 3px; width: 25%">
                                                        <asp:DropDownList ID="ddlMonthsH" runat="server" CssClass="form-control">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <asp:DropDownList ID="ddlDatesH" runat="server" CssClass="form-control">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                            <asp:ListItem Text="13" Value="13" />
                                                            <asp:ListItem Text="14" Value="14" />
                                                            <asp:ListItem Text="15" Value="15" />
                                                            <asp:ListItem Text="16" Value="16" />
                                                            <asp:ListItem Text="17" Value="17" />
                                                            <asp:ListItem Text="18" Value="18" />
                                                            <asp:ListItem Text="19" Value="19" />
                                                            <asp:ListItem Text="20" Value="20" />
                                                            <asp:ListItem Text="21" Value="21" />
                                                            <asp:ListItem Text="22" Value="22" />
                                                            <asp:ListItem Text="23" Value="23" />
                                                            <asp:ListItem Text="24" Value="24" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="26" Value="26" />
                                                            <asp:ListItem Text="27" Value="27" />
                                                            <asp:ListItem Text="28" Value="28" />
                                                            <asp:ListItem Text="29" Value="29" />
                                                            <asp:ListItem Text="30" Value="30" />
                                                            <asp:ListItem Text="31" Value="31" />
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>
                                                تاريخ الميلاد (ميلادي) : 
                                            </label>
                                            <table>
                                                <tr>
                                                    <td style="padding-left: 3px; width: 50%">
                                                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding-left: 3px; width: 50%">
                                                        <asp:DropDownList ID="ddlMonths" runat="server" CssClass="form-control">
                                                            <asp:ListItem />
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:DropDownList ID="ddlDates" runat="server" CssClass="form-control">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                            <asp:ListItem Text="13" Value="13" />
                                                            <asp:ListItem Text="14" Value="14" />
                                                            <asp:ListItem Text="15" Value="15" />
                                                            <asp:ListItem Text="16" Value="16" />
                                                            <asp:ListItem Text="17" Value="17" />
                                                            <asp:ListItem Text="18" Value="18" />
                                                            <asp:ListItem Text="19" Value="19" />
                                                            <asp:ListItem Text="20" Value="20" />
                                                            <asp:ListItem Text="21" Value="21" />
                                                            <asp:ListItem Text="22" Value="22" />
                                                            <asp:ListItem Text="23" Value="23" />
                                                            <asp:ListItem Text="24" Value="24" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="26" Value="26" />
                                                            <asp:ListItem Text="27" Value="27" />
                                                            <asp:ListItem Text="28" Value="28" />
                                                            <asp:ListItem Text="29" Value="29" />
                                                            <asp:ListItem Text="30" Value="30" />
                                                            <asp:ListItem Text="31" Value="31" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlDates" ErrorMessage="* إدخل العمر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                            ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>
                                                العمر : <span style="color: red">*</span> <a href="http://dirarab.net/dateconversion" target="_blank">الذهاب لموقع التحويل</a>
                                            </label>
                                            <asp:TextBox ID="txtAge" runat="server" class="form-control" ValidationGroup="g1" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator27" runat="server"
                                                ControlToValidate="txtAge" ErrorMessage="* إدخل العمر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                المهنة الحالية : <span style="color: red">*</span>
                                            </label>
                                            <asp:DropDownList ID="DLAlMehnah" runat="server" CssClass="form-control">
                                                <asp:ListItem></asp:ListItem>
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
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator29" runat="server"
                                                ControlToValidate="DLAlMehnah" ErrorMessage="* المهنة الحالية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>الحالة الصحية : <span style="color: red">*</span>
                                            </label>
                                            <asp:DropDownList ID="txtAlHalahAlSehe" runat="server" ValidationGroup="g1" CssClass="form-control">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="سليم">سليم</asp:ListItem>
                                                <asp:ListItem Value="سليمة">سليمة</asp:ListItem>
                                                <asp:ListItem Value="مريض">مريض</asp:ListItem>
                                                <asp:ListItem Value="مريضة">مريضة</asp:ListItem>
                                                <asp:ListItem Value="غير ذلك">غير ذلك</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator31" runat="server"
                                                ControlToValidate="txtAlHalahAlSehe" ErrorMessage="* الحالة الصحية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div align="left">
                                    <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px;"
                                        class="btn btn-info btn-fill" ValidationGroup="g1" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnFinish" runat="server" Text="إرسال الملف" class="btn btn-warning" OnClick="btnFinish_Click" />
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlData" runat="server" Visible="False">
                                        <hr style="width:100%" />
                                        <table class="table table-bordered table-hover table-responsive table-condensed">
                                            <tr>
                                                <th>
                                                    م 
                                                </th>
                                                <th>
                                                    الإسم 
                                                </th>
                                                <th>
                                                    القرابة 
                                                </th>
                                                <th>
                                                    السجل المدني 
                                                </th>
                                                <th>
                                                    تاريخ الميلاد 
                                                </th>
                                                <th>
                                                    العمر 
                                                </th>
                                                <th>
                                                    المهنة الحالية 
                                                </th>
                                                <th>
                                                    العام الدراسي 
                                                </th>
                                                <th>
                                                    المستوى الدراسي 
                                                </th>
                                                <th>
                                                    الحالة الصحية 
                                                </th>
                                                <th>

                                                </th>
                                            </tr>
                                            <asp:Repeater ID="RPBuys" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Name") %>
                                                        </td>
                                                        <td>
                                                            <%# Library_CLS_Arn.ClassOutEntity.ClassQuaem.FQarabah((Int32) Eval("AlQarabah"))%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("A2") %>
                                                        </td>
                                                        <td>
                                                            <%# Library_CLS_Arn.Saddam.ClassSaddam.FCheckNullDate(Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.FChangeF((DateTime) Eval("DateBrith")))%>
                                                        </td>
                                                        <td>
                                                            <%# Library_CLS_Arn.Saddam.ClassSaddam.FGetAge((DateTime) (Eval("DateBrith")))%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("AlMehnahAlHaliah")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("A1")%>
                                                        </td>
                                                        <td>
                                                            <%# Library_CLS_Arn.Saddam.ClassSaddam.FCheckAlmostawaAlDerasy(Convert.ToString(Eval("AlmostawaAlDerasy")))%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("AlHalahAlseHeyah")%>
                                                        </td>
                                                        <td>
                                                            <%# FGetLink(Convert.ToString(Eval("IDUniq"))) %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                        <hr />
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
                                        <hr />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlOK" runat="server" Visible="false">
                        <div class="contact-form-cont">
                            <h2 class="text-center">تأكيد الإرسال</h2>
                            <div id="contact-form" class="contact-form">
                                <div class="user-info-from-cookie contact-form">
                                    <div>
                                        <hr />
                                        <br />
                                        <br />
                                        <br />
                                        <h2 class="text-center">تم إرسال ملفك بنجاح 
                                            <br />
                                            سيتم الإطلاع علية من قبل الإدارة
                                            <br />
                                            شكراً لك
                                             <i class="fa fa-check"></i>
                                        </h2>
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnOk" runat="server" Text="حسناً" Style="border-radius: 8px" class="btn btn-info" OnClick="btnOk_Click" />
                                        <br />
                                        <br />
                                        <br />
                                        <hr />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </body>
</asp:Content>

