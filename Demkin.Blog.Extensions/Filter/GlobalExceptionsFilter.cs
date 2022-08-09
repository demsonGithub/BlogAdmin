using Demkin.Blog.Extensions.Exceptions;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Demkin.Blog.Extensions.Filter
{
    /// <summary>
    /// 全局异常错误
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        public GlobalExceptionsFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            // 获取需要返回的错误信息
            var controllerName = context.ActionDescriptor.DisplayName;

            if (context.Exception is IKnownException knownException)
            {
                knownException = KnownException.FromKnownException(knownException);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                // 未捕获的错误，日志记录下来
                Log.Error(context.Exception, context.Exception.Message);

                knownException = KnownException.UnKnown(ConfigSetting.SiteInfo.IsDisplayStack ? context.Exception : null);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.Result = new JsonResult(knownException)
            {
                ContentType = "application/json; charset=utf-8"
            };
        }
    }
}