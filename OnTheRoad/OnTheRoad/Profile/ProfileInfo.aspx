<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileInfo.aspx.cs" Inherits="OnTheRoad.Profile.ProfileInfo" %>

<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileInfo" runat="server">

    <h1>Prfile here!</h1>
    <h3><%# Model.FirstName %></h3>
</asp:Content>