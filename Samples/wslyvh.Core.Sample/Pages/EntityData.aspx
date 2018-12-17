<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EntityData.aspx.cs" Inherits="wslyvh.Core.Sample.Pages.EntityData" %>
<%@ Register Src="~/Views/CategoryList.ascx" TagPrefix="wslyvh" TagName="CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>wslyvh.Core | EF Data Access</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        wslyvh.Core | EF Data Access
    </h2>

    <h3>Category List</h3>
    
    <wslyvh:CategoryList runat="server" ID="CategoryList" />
</asp:Content>