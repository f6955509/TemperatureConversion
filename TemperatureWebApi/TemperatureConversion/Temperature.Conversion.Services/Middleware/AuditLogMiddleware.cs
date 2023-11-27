using System.Text;

namespace Temperature.Conversion.Services.Middleware
{
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.Path.StartsWithSegments("/api"))
            {
                request.RouteValues.TryGetValue("action", out var actionValue);
                var actionName = (string)(actionValue ?? string.Empty);

                var requestContent = await GetRequestContent(request).ConfigureAwait(false);

                string clientIp = GetIPAddress(request);

                WriteAuditLog($"User from ip '{{' '{clientIp}' '}}' is trying to {request.Method} for method '{request.Path}/{actionName}' with data: {requestContent}");
            }

            await _next(context);
        }

        public static void WriteAuditLog(string message)
        {
            try
            {
                using (StreamWriter file = new StreamWriter("audit.log", true))
                {
                    file.WriteLine($"{DateTime.Now}, {message}");
                }
            }
            catch (Exception e)
            {
                // Log the error
                Console.WriteLine($"Error writing to audit log: {e.Message}");
            }
        }

        private static async Task<string> GetRequestContent(HttpRequest request)
        {
            var changedValue = string.Empty;
            switch (request.Method)
            {
                case "POST":
                case "PUT":
                    changedValue = await ReadRequestBody(request, Encoding.UTF8).ConfigureAwait(false);
                    break;
                default:
                    break;
            }
            return changedValue;
        }
        private static async Task<string> ReadRequestBody(HttpRequest request, Encoding? encoding = null)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestContent = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;  //rewinding the stream to 0

            return requestContent;
        }

        private string GetIPAddress(HttpRequest request)
        {
            var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress != null)
            {
                // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                // This usually only happens when the browser is on the same machine as the server.
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                   var  ip = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
                    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    return ip.ToString();
                }
                return remoteIpAddress.ToString();
            }
            return "";
        }
    }
}
