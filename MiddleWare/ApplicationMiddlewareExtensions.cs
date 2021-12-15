using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiddleWare
{
    public static class ApplicationMiddlewareExtensions
    {
        public static void UseExtensions(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<html><body>Response from first middleware.<br>");
                await next();
                await context.Response.WriteAsync("<br>Response after second middleware in first middleware.<br></body></html>");
            });

            app.UseWhen((context) => context.Request.Query.ContainsKey("role"),
                (a) =>
                {
                    a.Run(async context => await context.Response.WriteAsync($"<br>Role is {context.Request.Query["role"]}.<br>"));
                });

            app.Map("/map", a =>
            {
                a.Map("/branch", a => a.Run(async (HttpContext context) => await context.Response.WriteAsync("<br>New child branch.<br>")));

                a.Run(async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("<br>New branch map.<br>");
                });
            });

            app.MapWhen((HttpContext context) => context.Request.Query.ContainsKey("count"),
                a => a.Run(async (HttpContext context) => await context.Response.WriteAsync($"<br>count is: {context.Request.Query["count"]}.<br>")));

            app.Run(async context =>
            {
                await context.Response.WriteAsync("<br>Response from second middleware.<br>");
            });
        }
    }
}
