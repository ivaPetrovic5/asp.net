<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentView.aspx.cs" Inherits="StudentWebForms.StudentView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">
        <asp:GridView runat="server" ID="studentsGrid"
        ItemType="StudentWebForms.Models.Student" DataKeyNames="Id" 
        SelectMethod="studentsGrid_GetData"
        AutoGenerateColumns="false" OnSelectedIndexChanged="studentsGrid_SelectedIndexChanged">
            <Columns>
                <asp:DynamicField DataField="Id" />
                <asp:DynamicField DataField="FirstName" />
                <asp:DynamicField DataField="LastName" />
                <asp:DynamicField DataField="Age" />          
                <asp:DynamicField DataField="City"/>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDeleteStudent_Click" CommandArgument="<%#Item.Id %>" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>  
            </Columns>
        </asp:GridView>
    </form>
    <asp:HyperLink ID="createStudent" runat="server" NavigateUrl="StudentAdd.aspx" Text="Add Student" />
</body>
</html>
