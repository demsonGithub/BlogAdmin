using Demkin.Blog.DTO;
using Demkin.Blog.Utils.ClassExtension;
using System;

namespace Demkin.Blog.Extensions.Exceptions
{
    public class KnownException : IKnownException
    {
        public string code { get; private set; }

        public string msg { get; private set; }

        public object data { get; private set; }

        public static IKnownException UnKnown(object data = null)
        {
            return new KnownException { code = ApiErrorCode.Server_Error.GetDescription(), msg = "未知错误", data = data };
        }

        public static IKnownException FromKnownException(IKnownException knownException)
        {
            return new KnownException
            {
                code = knownException.code,
                msg = knownException.msg,
                data = knownException.data
            };
        }
    }
}