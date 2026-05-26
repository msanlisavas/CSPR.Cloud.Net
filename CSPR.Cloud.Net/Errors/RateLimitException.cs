using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class RateLimitException : Exception
    {
        public RateLimitException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }
}
