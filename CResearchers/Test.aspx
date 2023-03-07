<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="CResearchers_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtcookie" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnsendSet" runat="server" Text="Set" OnClick="btnsendSet_Click"  />
        <br />
        <asp:Button ID="btnsendGet" runat="server" Text="Get" OnClick="btnsendGet_Click"  />
    </div>
    </form>
</body>
</html>
