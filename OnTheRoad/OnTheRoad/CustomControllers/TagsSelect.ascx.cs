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
            if (this.SelectedTagNames == null)
            {
                this.SelectedTagNames = new List<string>();
            }
        }

        public IEnumerable<string> SelectedTagNames
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
            if (tagName != string.Empty && !this.SelectedTagNames.Contains(tagName))
            {
                var tagNames = this.SelectedTagNames.ToList();
                tagNames.Add(tagName);
                this.SelectedTagNames = tagNames;
            }

            var tags = new List<TagModel>();
            foreach (var item in this.SelectedTagNames)
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