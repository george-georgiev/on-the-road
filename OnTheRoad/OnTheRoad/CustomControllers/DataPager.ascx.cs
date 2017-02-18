using System;
using System.Web.UI;

namespace OnTheRoad.CustomControllers
{
    public partial class DataPager : UserControl
    {
        private const string PageKey = "Page";
        private const string TotalKey = "Total";
        private const int FirstPageIndex = 1;

        public int PageNumber
        {
            get
            {
                if (this.ViewState[PageKey] == null)
                {
                    this.ViewState[PageKey] = FirstPageIndex;
                }

                return (int)this.ViewState[PageKey];
            }
            set
            {
                this.ViewState[PageKey] = value;
            }
        }

        public int? Total
        {
            get
            {
                return (int?)this.ViewState[TotalKey];
            }
            set
            {
                this.ViewState[TotalKey] = value;
            }
        }

        public int PageSize { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.PageNumber == FirstPageIndex)
            {
                this.ButtonPrev.Visible = false;
            }
            else
            {
                this.ButtonPrev.Visible = true;
            }

            var count = this.PageSize * this.PageNumber;
            if (count >= this.Total)
            {
                this.ButtonNext.Visible = false;
            }
            else
            {
                this.ButtonNext.Visible = true;
            }
        }

        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            this.PageNumber++;
        }

        protected void ButtonPrev_Click(object sender, EventArgs e)
        {
            if (this.PageNumber > FirstPageIndex)
            {
                this.PageNumber--;
            }
        }
    }
}