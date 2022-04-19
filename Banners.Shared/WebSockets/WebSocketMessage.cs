using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banners.Shared.WebSockets
{
    public class WebSocketMessage<T>
    {
        public string Action { get; set; }
        public T Model { get; set; }
    }
}
