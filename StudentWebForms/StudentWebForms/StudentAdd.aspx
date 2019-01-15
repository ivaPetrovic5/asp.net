<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAdd.aspx.cs" Inherits="StudentWebForms.StudentAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:label runat="server" text="First Name: "></asp:label>
            <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:label runat="server" text="Last Name: "></asp:label>
            <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:label runat="server" text="Age: "></asp:label>
            <asp:TextBox ID="tbAge" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:label runat="server" text="City: "></asp:label>
            <asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="createStudent" runat="server" Text="Add Student" OnClick="createStudent_Click" />
        </div>
    </form>
</body>
</html>
