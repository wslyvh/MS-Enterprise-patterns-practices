<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleView.ascx.cs" Inherits="wslyvh.Core.Samples.BootMvp.Views.SampleView" %>

<asp:Panel ID="SamplePanel" runat="server">
    
    <asp:Panel ID="PostMessagePanel" runat="server">
        <asp:Label ID="PostMessageLabel" AssociatedControlID="PostMessageTextBox" runat="server" Text="Enter a message: " />
        <asp:TextBox ID="PostMessageTextBox" runat="server" />
        <asp:Button ID="PostMessageButton" runat="server" OnClick="PostMessageButton_Click" Text="Post" />
    </asp:Panel>
    
    <asp:Panel ID="GetMessagePanel" runat="server">
        <asp:Label ID="GetMessageLabel" AssociatedControlID="GetMessageButton" runat="server" Text="Get Message" />
        <asp:Button ID="GetMessageButton" runat="server" OnClick="GetMessageButton_Click" Text="Get" />
        <br/>
        <asp:Label ID="GetMessageResult" runat="server" />
    </asp:Panel>
</asp:Panel>