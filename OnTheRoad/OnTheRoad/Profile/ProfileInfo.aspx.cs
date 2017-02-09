﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Profile.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.CustomControllers;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.Profile
{
    [PresenterBinding(typeof(ProfileInfoPresenter))]
    public partial class ProfileInfo : MvpPage<ProfileInfoModel>, IProfileInfoView
    {
        public event EventHandler<ProfileInfoEventArgs> GetProfileInfo;
        public event EventHandler<ProfileInfoEventArgs> UpdateProfileInfo;
        public event EventHandler<ProfileInfoEventArgs> CheckIfUserExists;

        public string GetUsername { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetProfileInfo?.Invoke(this, new ProfileInfoEventArgs());
            this.FormViewProfileInfo.DataSource = new List<ProfileInfoModel>() { this.Model };
            this.FormViewProfileInfo.DataBind();

            if (this.GetUsername == null)
            {
                this.GetUsername = this.Model.Username;
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            this.FormViewProfileInfo.ChangeMode(FormViewMode.Edit);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            TextBox firstName = this.FormViewProfileInfo.FindControl("FirstName") as TextBox;
            TextBox lastName = this.FormViewProfileInfo.FindControl("Lastname") as TextBox;
            CitiesDropDown city = this.FormViewProfileInfo.FindControl("City") as CitiesDropDown;
            TextBox username = this.FormViewProfileInfo.FindControl("Username") as TextBox;
            TextBox phoneNumber = this.FormViewProfileInfo.FindControl("PhoneNumber") as TextBox;
            TextBox info = this.FormViewProfileInfo.FindControl("Info") as TextBox;

            if (city.SelectedCityId == 0)
            {
                city.SelectedCityId = 1;
            }

            this.UpdateProfileInfo?.Invoke(this, new ProfileInfoEventArgs()
            {
                CityId = city.SelectedCityId,
                FirstName = firstName.Text,
                LastName = lastName.Text,
                PhoneNumber = phoneNumber.Text,
                Info = info.Text,
                Username = username.Text
            });

            this.PanelError.Visible = false;
            this.FormViewProfileInfo.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void Username_TextChanged(object sender, EventArgs e)
        {
            TextBox username = this.FormViewProfileInfo.FindControl("Username") as TextBox;
            var selectedUsername = username.Text.Trim();
            if (selectedUsername == string.Empty)
            {
                this.ShowErrorMessage("Трябва да въведете потребителско име.");
                (this.FormViewProfileInfo.FindControl("Username") as TextBox).Focus();
                return;
            }

            this.CheckIfUserExists?.Invoke(this, new ProfileInfoEventArgs() { Username = username.Text });

            if (this.Model.DoesUserExist)
            {
                this.ShowErrorMessage("Потребителското име вече е заето.");
                (this.FormViewProfileInfo.FindControl("Username") as TextBox).Focus();
            }
            else
            {
                this.GetUsername = selectedUsername;
                this.PanelError.Visible = false;
            }
        }

        private void ShowErrorMessage(string msg)
        {
            this.FailureText.Text = msg;
            this.PanelError.Visible = true;
        }
    }
}