using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;


namespace SignalR.StockTicker
{
    public static class RegisterHubs
    {
        public static void Start()
        {
            // Register the default hubs route: ~/signalr
            RouteTable.Routes.MapHubs();
        }
    }
}
