using Microsoft.Extensions.Primitives;
using System;
using System.Threading;

namespace Demkin.Blog.WebApi
{
    public class IgnoreChangeToken : IChangeToken
    {
        private CancellationToken _cancellationToken { get; }

        public IgnoreChangeToken(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public bool HasChanged => _cancellationToken.IsCancellationRequested;

        public bool ActiveChangeCallbacks => true;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            return _cancellationToken.Register(callback, state);
        }
    }
}