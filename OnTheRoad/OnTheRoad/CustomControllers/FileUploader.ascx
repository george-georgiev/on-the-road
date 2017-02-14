<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploader.ascx.cs" Inherits="OnTheRoad.CustomControllers.FileUploader" %>

<div>
    <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="btn-upload" Width="95" />
    <asp:Label AssociatedControlID="FileUploadImage" runat="server" CssClass="label-upload btn btn-xs btn-default">
        ИЗБЕРИ СНИМКА
    </asp:Label>
    <asp:Button runat="server" ID="ButtonUploadImage" Text="КАЧИ" CssClass="btn btn-xs btn-success" OnClick="ButtonUploadImage_Click" />
</div>
