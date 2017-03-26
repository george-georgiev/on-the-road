using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class ChatViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }

        public string TargetUser { get; set; }
    }
}