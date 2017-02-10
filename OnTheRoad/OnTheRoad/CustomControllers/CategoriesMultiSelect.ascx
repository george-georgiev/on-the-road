<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesMultiSelect.ascx.cs" Inherits="OnTheRoad.CustomControllers.CategoriesMultiSelect" %>

<asp:ListBox runat="server" ID="CategoriesListBox" CssClass="form-control"
    SelectionMode="Multiple"
    DataTextField="Name"
    DataValueField="Id"></asp:ListBox>

<asp:RequiredFieldValidator runat="server" ControlToValidate="CategoriesListBox" Enabled="<%# this.IsRequired %>"
    CssClass="text-danger" Display="Dynamic" ErrorMessage="<%# this.ErrorMessage %>" />