using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class MessageViewModel : IMapFrom<IMessage>
    {
        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuthorUsername { get; set; }

        public byte[] AuthorImage { get; set; }
    }
}