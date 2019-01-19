<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentEdit.aspx.cs" Inherits="StudentWebForms.StudentEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <asp:HiddenField ID="hfId" runat="server" />
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
            <asp:Button ID="editStudent" runat="server" Text="Save" OnClick="editStudent_Click" />
        </div>
    </form>
</body>
</html>
