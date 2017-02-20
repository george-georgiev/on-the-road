<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagsSelect.ascx.cs" Inherits="OnTheRoad.CustomControllers.TagsSelect" %>
<%@ Import Namespace="OnTheRoad.Mvp.Models" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<div class="float-left">
    <asp:UpdatePanel ID="TagsUpdatePanel" runat="server">
        <ContentTemplate>
            <div class="col-md-offset-2">
                <asp:Repeater ID="TagsRepeater" runat="server" ItemType="TagModel">
                    <ItemTemplate>
                        <div class="tags-select-item">
                            <%#: Item.Name %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="form-group">
                <asp:Panel runat="server" DefaultButton="TagSelectButton">
                    <asp:Label runat="server" AssociatedControlID="TagsTextBox" CssClass="col-md-2 control-label input-labels"><%# this.TagsLabel %></asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" CssClass="form-control tags-select-textbox" ID="TagsTextBox" />
                        <asp:Button runat="server" ID="TagSelectButton" CssClass="btn btn-small btn-success"
                            OnClick="TagSelectButton_Click" Text="Добави" CausesValidation="False" />
                    </div>
                </asp:Panel>
            </div>

            <ajax:AutoCompleteExtender ID="TagsAutoComplete"
                ServicePath="~/WebServices/Tags.asmx"
                ServiceMethod="GetTagsByPrefix"
                TargetControlID="TagsTextBox"
                MinimumPrefixLength="1"
                CompletionInterval="300" EnableCaching="false" CompletionSetCount="10"
                runat="server">
            </ajax:AutoCompleteExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TagSelectButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
