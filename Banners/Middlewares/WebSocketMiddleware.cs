using Banners.Shared.WebSockets;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Banners.Middlewares
{
    public class WebSocketMiddleware
    {

        

        private readonly RequestDelegate _next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            CancellationToken ct = context.RequestAborted;
            WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();

            var socketId = BannerSocket.Add(currentSocket);

            while (true)
            {
                if (ct.IsCancellationRequested || currentSocket.State != WebSocketState.Open)
                {
                    BannerSocket.Remove(socketId);

                    await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
                    currentSocket.Dispose();
                }
            }
        }
    }
}
