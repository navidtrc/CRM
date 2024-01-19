using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRM.WebFramework.Middlewares
{
    public static class JwtTokenInCookieMiddlewareExtension
    {
        public static void UseJwtTokenInCookieHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<JWTInHeaderMiddleware>();
        }
    }

    public class JWTInHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTInHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var authenticationCookieName = "access_token";
            var cookie = context.Request.Cookies[authenticationCookieName];
            if (cookie != null)
                context.Request.Headers.Append("Authorization", "Bearer " + cookie);
            await _next.Invoke(context);
        }
    }
}


