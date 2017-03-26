﻿using OnTheRoad.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Logic.Contracts
{
    public interface IConversationHelper
    {
        IConversation CreateConversation(string firstUsername, string secondUername);

        void AddMessage(IConversation conversation, string text, string authorUsername);
    }
}
