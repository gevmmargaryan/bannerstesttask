namespace Banners.Websockets
{
    public class WebSocketMiddleware
    {
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

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
            var socketId = Guid.NewGuid().ToString();

            _sockets.TryAdd(socketId, currentSocket);

            while (true)
            {
                if (ct.IsCancellationRequested || currentSocket.State != WebSocketState.Open)
                {
                    WebSocket dummy;
                    _sockets.TryRemove(socketId, out dummy);

                    await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
                    currentSocket.Dispose();
                }
            }
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