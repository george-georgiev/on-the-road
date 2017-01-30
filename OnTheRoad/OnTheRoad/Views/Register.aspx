<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnTheRoad.Views.Register" %>

<%@ Register Src="~/CustomControllers/RegisterController.ascx" TagPrefix="uc" TagName="RegisterController" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <uc:RegisterController runat="server" ID="RegisterController" />
</asp:Content>

