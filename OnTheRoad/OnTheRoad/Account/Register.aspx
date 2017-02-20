<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnTheRoad.Account.Register" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolderCSSMaster">
    <link href="/Css/LoginRegister.css" rel="stylesheet" />
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class=" form-containter">

        <h2 class="page-headers" id="registration-form">Регистрация</h2>

        <p class="text-danger">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Username" CssClass="col-md-2 control-label input-labels">Потребителско име</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Username" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                        CssClass="text-danger" ErrorMessage="Моля въведете потребителско име." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label input-labels">Име</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                        CssClass="text-danger" ErrorMessage="Моля въведете име." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label input-labels">Фамилия</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="LastName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                        CssClass="text-danger" ErrorMessage="Моля въведете фамилия." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label input-labels">Имейл</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="Моля въведете имейл." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label input-labels">Парола</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                        CssClass="text-danger" ErrorMessage="Моля въведете парола." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label input-labels">Потвърдете паролата</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="Моля потвърдете паролата. " />
                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="Паралото не съвпада с горенаписаната." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" OnClick="CreateUser_Click" Text="Регистрирай се" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

