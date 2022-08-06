using Demkin.Blog.DTO;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
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
            //var jsonContent = new JsonErrorResponse();
            //jsonContent.Message = context.Exception.Message;
            //var specificErr = "Unable to resolve service";
            //if (!string.IsNullOrEmpty(jsonContent.Message) && jsonContent.Message.Contains(specificErr))
            //{
            //    jsonContent.Message = specificErr;
            //}

            // 如果异常没有被处理则进行处理
            if (context.ExceptionHandled == false)
            {
                // 获取需要返回的错误信息
                var exceptionDetail = new ExceptionDetail
                {
                    errorMsg = context.Exception.Message,
                    errorStackTrace = _env.IsDevelopment() ? context.Exception.StackTrace : ""
                };

                //定义返回信息
                var responseResult = new CustomExceptionResult
                {
                    code = ApiErrorCode.Server_Error.GetDescription(),
                    msg = "发生了错误",
                    data = exceptionDetail
                };

                //写入日志
                //_loggerHelper.Error(context.HttpContext.Request.Path, context.Exception);

                context.Result = new ContentResult
                {
                    // 返回状态码设置为200，表示成功
                    StatusCode = StatusCodes.Status200OK,
                    // 设置返回格式
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(responseResult)
                };
            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;
        }
    }

    public class CustomExceptionResult
    {
        public string code { get; set; }

        public string msg { get; set; }

        public ExceptionDetail data { get; set; }
    }

    public class ExceptionDetail
    {
        public string errorMsg { get; set; }

        public string errorStackTrace { get; set; }
    }
}