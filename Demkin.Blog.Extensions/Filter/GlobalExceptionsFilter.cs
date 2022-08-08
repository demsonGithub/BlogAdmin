using Demkin.Blog.DTO;
using Demkin.Blog.Extensions.Exceptions;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Extensions.Filter
{
    /// <summary>
    /// 全局异常错误
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionsFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            IKnownException knownException = context.Exception as IKnownException;
            // 获取需要返回的错误信息
            var exceptionDetail = new ExceptionDetail
            {
                errorMsg = context.Exception.Message,
                errorStackTrace = _env.IsDevelopment() ? context.Exception.StackTrace : ""
            };

            if (knownException == null)
            {
                Log.Error(context.Exception, context.Exception.Message);

                knownException = KnownException.UnKnown;
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            else
            {
                knownException = KnownException.FromKnownException(knownException);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            context.Result = new JsonResult(knownException)
            {
                ContentType = "application/json; charset=utf-8"
            };
        }
    }
}