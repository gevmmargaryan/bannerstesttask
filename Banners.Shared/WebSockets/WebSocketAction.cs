using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banners.Shared.WebSockets
{
    public class WebSocketAction
    {
        public static string AddBanner = "AddBanner";
        public static string DeleteBanner = "DeleteBanner";

        public static implicit operator string(WebSocketAction v)
        {
            throw new NotImplementedException();
        }
    }
}
