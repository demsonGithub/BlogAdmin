using Microsoft.AspNetCore.Builder;
using System;

namespace Demkin.Blog.Extensions.Middlewares
{
    /// <summary>
    /// swagger 中间件
    /// </summary>
    public static class SwaggerMiddleware
    {
        public static void UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Cms Api");
                c.RoutePrefix = "api";
            });
        }
    }
}