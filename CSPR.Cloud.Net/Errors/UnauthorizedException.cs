using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }
}
