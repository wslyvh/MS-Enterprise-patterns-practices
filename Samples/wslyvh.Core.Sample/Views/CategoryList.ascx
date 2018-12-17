<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" Inherits="wslyvh.Core.Sample.Views.CategoryList" %>

<asp:ListView  ID="CategoriesListView" runat="server">
    <LayoutTemplate>
        <table border="0">
            <tr>
                <td>Id</td>
                <td>Title</td>
                <td>Created</td>
                <td>Created by</td>
                <td>Modified</td>
                <td>Modified by</td>
            </tr>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            <tr>
                <td colspan="6">
                    <asp:Button ID="AddItem" runat="server" Text="Add Item" CommandName="Add" OnClick="AddItemClick" />
                </td>
            </tr>
        </table>
    </LayoutTemplate>

    <ItemTemplate>
            <tr>
                <td><%#Eval("Id") %></td>
                <td><%#Eval("Title") %></td>
                <td><%#Eval("Created") %></td>
                <td><%#Eval("CreatedBy") %></td>
                <td><%#Eval("Modified") %></td>
                <td><%#Eval("ModifiedBy") %></td>
            </tr>
    </ItemTemplate>

    <EmptyDataTemplate>
        <tr>
            <td colspan="6">No Categories found.</td>
        </tr>
    </EmptyDataTemplate>

    <InsertItemTemplate>
        <asp:Label runat="server" AssociatedControlID="Title" Text="Title">
            <asp:TextBox ID="Title" runat="server" ></asp:TextBox>
        </asp:Label>
    </InsertItemTemplate>

    <ItemTemplate></ItemTemplate>
</asp:ListView>
