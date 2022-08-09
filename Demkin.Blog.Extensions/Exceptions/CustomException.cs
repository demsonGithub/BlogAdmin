using System;

namespace Demkin.Blog.Extensions.Exceptions
{
    public class CustomException : Exception, IKnownException
    {
        public string code { get; private set; }

        public string msg { get; private set; }

        public object data { get; private set; }

        public CustomException(string code, string msg, object data = null)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
    }
}