<%@ Page Title="" Language="C#" MasterPageFile="~/ITSupport/CRM/MPITSupportCRM.master" AutoEventWireup="true" CodeFile="PageDetailsVideo.aspx.cs" Inherits="ITSupport_CRM_PageDetailsVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <div id="prod_wrapper">
        <div align="right">
                <h4>
                   : <asp:Label ID="lblTitle" runat="server"></asp:Label> *
                    <br /><br />
                </h4>
        </div>
        <div class="content">
            <div id="command">
                <a class="lightSwitcher" href="#">Lights</a></div>
            <div id="movie" style="margin-top:-25px;">
                <iframe runat="server" id="IDVideo" class="widthVideo"
                    frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
            </div>
        </div>
        <div style="clear: both">
        </div>
        <div style="clear: both; height: 40px">
        </div>
        </div>
    </div>
</asp:Content>

