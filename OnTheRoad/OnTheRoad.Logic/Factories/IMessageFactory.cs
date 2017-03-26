﻿using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Logic.Factories
{
    public interface IMessageFactory
    {
        IMessage CreateMessage(IUser author, IConversation conversation, DateTime createDate, string text);
    }
}
