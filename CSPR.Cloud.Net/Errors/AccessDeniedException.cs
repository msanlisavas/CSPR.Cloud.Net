using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }
}
