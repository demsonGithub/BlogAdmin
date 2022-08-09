namespace Demkin.Blog.Extensions.Exceptions
{
    public interface IKnownException
    {
        string code { get; }

        string msg { get; }

        object data { get; }
    }
}