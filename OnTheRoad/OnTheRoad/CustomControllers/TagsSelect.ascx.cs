using OnTheRoad.Mvp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnTheRoad.CustomControllers
{
    public partial class TagsSelect : System.Web.UI.UserControl
    {
        private const string SelectedTagsKey = "SelectedTags";

        public TagsSelect()
        {
            if (this.SelectedTags == null)
            {
                this.SelectedTags = new List<string>();
            }
        }

        public IEnumerable<string> SelectedTags
        {
            private set
            {
                this.ViewState[SelectedTagsKey] = value;
            }
            get
            {
                return (IEnumerable<string>)this.ViewState[SelectedTagsKey];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TagSelectButton_Click(object sender, EventArgs e)
        {
            var tagName = this.TagsTextBox.Text;
            if (tagName != string.Empty && !this.SelectedTags.Contains(tagName))
            {
                var tagNames = this.SelectedTags.ToList();
                tagNames.Add(tagName);
                this.SelectedTags = tagNames;
            }

            var tags = new List<TagModel>();
            foreach (var item in this.SelectedTags)
            {
                var tag = new TagModel() { Name = item };
                tags.Add(tag);
            }

            this.TagsRepeater.DataSource = tags;
            this.TagsRepeater.DataBind();

            this.TagsTextBox.Text = string.Empty;
        }
    }
}