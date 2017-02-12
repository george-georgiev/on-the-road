<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagsSelect.ascx.cs" Inherits="OnTheRoad.CustomControllers.TagsSelect" %>
<%@ Import Namespace="OnTheRoad.Mvp.Models" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<div class="float-left">
    <asp:UpdatePanel ID="TagsUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="TagsRepeater" runat="server" ItemType="TagModel">
                <ItemTemplate>
                    <%#: Item.Name %>
                </ItemTemplate>
            </asp:Repeater>

            <div>
                <asp:TextBox runat="server" ID="TagsTextBox" />

                <asp:Button runat="server" ID="TagSelectButton" OnClick="TagSelectButton_Click" Text="Добави" CausesValidation="False" />
            </div>

            <ajax:AutoCompleteExtender ID="TagsAutoComplete"
                ServicePath="~/WebServices/Tags.asmx"
                ServiceMethod="GetTagsByPrefix"
                TargetControlID="TagsTextBox"
                MinimumPrefixLength="1"
                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                runat="server">
            </ajax:AutoCompleteExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TagSelectButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
