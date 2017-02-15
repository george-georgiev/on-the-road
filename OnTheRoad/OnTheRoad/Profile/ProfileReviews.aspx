<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileReviews.aspx.cs" Inherits="OnTheRoad.Profile.ProfileReviews" %>


<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileReviews" runat="server">
    <div class="row text-center">
        <div class="col-md-12">
            <h2 class="page-headers">Коментари</h2>

            <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">ДОВАВИ КОМЕНТАР</button>

            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Добави коментар на <strong class="input-labels"><%# this.Request.QueryString["name"] %></strong></h4>
                        </div>
                        <div class="modal-body review-body">
                            <div class="form-group">
                                <asp:TextBox runat="server" Rows="6" ID="TextBoxAddReviewText" TextMode="MultiLine" CssClass="form-control" />
                            </div>
                            <div class="form-group text-center">
                                <asp:RadioButtonList runat="server" ID="RadioButtonsRating" CellPadding="20" RepeatDirection="Horizontal" CssClass="input-labels radio-btns-review">
                                    <asp:ListItem Text="Позитивен" Value="Positive" />
                                    <asp:ListItem Text="Неутрален" Value="Neutral" />
                                    <asp:ListItem Text="Негативен" Value="Negative" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn btn-default" data-dismiss="modal"  value="ОТКАЖИ" />
                            <asp:Button Text="ИЗПРАТИ" runat="server" ID="ButtonSend" CssClass="btn btn-success" 
                                OnClick="ButtonSend_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
