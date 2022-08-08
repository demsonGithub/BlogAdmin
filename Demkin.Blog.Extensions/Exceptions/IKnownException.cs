namespace Demkin.Blog.Extensions.Exceptions
{
    public interface IKnownException
    {
        string code { get; }

        string msg { get; }

        ExceptionDetail data { get; }
    }

    public class ExceptionDetail
    {
        public string errorMsg { get; set; }

        public string errorStackTrace { get; set; }
    }
}