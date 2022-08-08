using Demkin.Blog.DTO;
using Demkin.Blog.Utils.ClassExtension;

namespace Demkin.Blog.Extensions.Exceptions
{
    public class KnownException : IKnownException
    {
        public string code { get; private set; }

        public string msg { get; private set; }

        public ExceptionDetail data { get; private set; }

        public static readonly IKnownException UnKnown = new KnownException { code = ApiErrorCode.Server_Error.GetDescription(), msg = "未知错误" };

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