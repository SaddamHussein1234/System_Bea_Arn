<%@ Page Title="" Language="C#" MasterPageFile="~/CPBeneficiary/MPBeneficiary.master" AutoEventWireup="true" CodeFile="PageNotAccess.aspx.cs" Inherits="CPBeneficiary_PageNotAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="">غير مصرح</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-warning"></i>غير مصرح 
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:Panel ID="pnlData" runat="server">
                                <div align="center" class="w">
                                    <i class="fa fa-warning" style="font-size: 55px; color: #a00101"></i>
                                    <br />
                                    <br />
                                    <br />
                                    <h1>معذرة لقد طلبت صفحة غير مصرحة لك من قبل مسؤول النظام
                                    </h1>
                                    <br />
                                    <br />
                                    <br />
                                </div>
                            </asp:Panel>
                        </div>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">لا توجد نتائج
                                </h3>
                            </div>
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
        <br />
        <br />
        <br />
        <br />
</asp:Content>

