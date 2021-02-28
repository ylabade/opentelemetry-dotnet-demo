// Licensed under the MIT License. See License in the project root for license information.

namespace BackEnd
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class RequestIdMiddleware
    {
        private const string HeaderName = "X-Request-Id";

        private readonly RequestDelegate _next;

        public RequestIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers[HeaderName] = Activity.Current?.Id ?? context.TraceIdentifier;
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}