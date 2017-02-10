<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesDropDown.ascx.cs" Inherits="OnTheRoad.CustomControllers.CategoriesDropDown" %>

<asp:DropDownList ID="DropDownCategories" runat="server" CssClass="form-control"
    DataTextField="Name"
    DataValueField="Id"
    OnSelectedIndexChanged="DropDownCategories_SelectedIndexChanged">
</asp:DropDownList>