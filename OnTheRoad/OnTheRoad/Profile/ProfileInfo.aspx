<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileInfo.aspx.cs" Inherits="OnTheRoad.Profile.ProfileInfo" %>

<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileInfo" runat="server">


    <div class="row text-center">
        <div class="col-md-12">
            <asp:FormView ID="FormViewProfileInfo" runat="server"
                ItemType="OnTheRoad.Models.ProfileInfoModel">
                <ItemTemplate>
                    <h2>
                        <asp:Literal Text="<%# this.Model.Username %>" runat="server" />
                    </h2>
                    <p>

                        <i class="fa fa-user-circle font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.FirstName + " " + Item.LastName %>' runat="server" />
                    </p>

                    <p>
                        <i class="fa fa-envelope-o font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.Email %>' runat="server" />
                    </p>
                    <p>
                        <i class="fa fa-map-marker font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.City + " " + Item.Country%>' runat="server" />
                    </p>
                    <p>
                        <i class="fa fa-mobile font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%#Item.PhoneNumber%>' runat="server" />
                    </p>
                    <p>
                        <i class="fa fa-id-card-o font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.Info %>' runat="server" />
                    </p>
                </ItemTemplate>

                <%--  <EditItemTemplate>
        </EditItemTemplate>--%>
            </asp:FormView>
        </div>
    </div>

    <asp:Button Text="Btn" runat="server" />

</asp:Content>
