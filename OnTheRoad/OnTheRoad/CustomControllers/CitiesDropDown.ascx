<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CitiesDropDown.ascx.cs" Inherits="OnTheRoad.CustomControllers.CitiesDropDown" %>

<asp:DropDownList ID="DropDownCities" runat="server" CssClass="form-control"
    DataTextField="Name"
    DataValueField="Id"
    OnSelectedIndexChanged="DropDownCities_SelectedIndexChanged">
</asp:DropDownList>