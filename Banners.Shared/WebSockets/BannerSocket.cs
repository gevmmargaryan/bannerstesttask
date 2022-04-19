using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Banners.Shared.WebSockets
{
    public class BannerSocket
    {
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public static string Add(WebSocket currentSocket)
        {
            var socketId = Guid.NewGuid().ToString();
            _sockets.TryAdd(socketId, currentSocket);

            return socketId;
        }

        public static void Remove(string? socketId)
        {
            WebSocket dummy;
            _sockets.TryRemove(socketId, out dummy);

        }


        public static async Task SendToAllAsync(string message)
        {
            foreach (var socket in _sockets)
            {
                await SendMessageAsync(socket.Value, message);
            }
        }

        private static Task SendMessageAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }
    }
}
