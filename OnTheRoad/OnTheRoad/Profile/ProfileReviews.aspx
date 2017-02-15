<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileReviews.aspx.cs" Inherits="OnTheRoad.Profile.ProfileReviews" %>

<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileReviews" runat="server">
    <div class="row text-center">
        <div class="col-md-12">
            <h2 class="page-headers">Коментари</h2>

            <asp:Button Text="ДОВАВИ КОМЕНТАР" ID="ButtonAddComment" CssClass="btn btn-info" data-toggle="modal" data-target="#myModal" runat="server" />

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
                            <input type="button" class="btn btn-default" data-dismiss="modal" value="ОТКАЖИ" />
                            <asp:Button Text="ИЗПРАТИ" runat="server" ID="ButtonSend" CssClass="btn btn-success"
                                OnClick="ButtonSend_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:ListView ID="ListViewComments" runat="server" ItemType="OnTheRoad.Domain.Models.IReview">
                <ItemTemplate>
                    <div class="row comment-wrapper">
                        <div class="col-md-1">
                            <img src='<%# "data:image/jpeg;base64," + Convert.ToBase64String(Item.FromUser.Image) %>'
                                alt="Снимка на потребителя" class="comment-user-image img-circle" />
                        </div>
                        <div class="col-md-10 right-wrapper">
                            <div class="comment-header">
                                <strong class="comment-rating"><%# Item.Rating %></strong>
                                <span class="from-user">
                                    <asp:Literal Text='<%# "from " + Item.FromUser.Username %>' runat="server" />
                                </span>
                            </div>
                            <div class="comment-body">
                                <asp:Literal Text='<%# Item.ReviewContent %>' runat="server" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>

            <asp:DataPager ID="DataPagerComments" runat="server"
                PagedControlID="ListViewComments" PageSize="5"
                QueryStringField="page">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="true"
                        ShowNextPageButton="false" ShowPreviousPageButton="false" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowLastPageButton="true"
                        ShowNextPageButton="false" ShowPreviousPageButton="false" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</asp:Content>
