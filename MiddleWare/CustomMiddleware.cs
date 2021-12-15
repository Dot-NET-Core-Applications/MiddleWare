using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddleWare
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IPrint printer;

        public CustomMiddleware(RequestDelegate next, IPrint printer)
        {
            this.next = next;
            this.printer = printer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Inside the new custom middleware.");
            await next(context);
            printer.Print();
            await context.Response.WriteAsync("Exiting the new custom middleware.");
        }
    }
}
