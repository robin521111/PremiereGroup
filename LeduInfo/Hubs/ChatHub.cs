using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LeduInfo.Hubs
{
    public class ChatHub : Hub
    {
        public void Sent(string name, string message)
        {
            Clients.All.addnewmessagetopage(name, message); 
        }
    }

}