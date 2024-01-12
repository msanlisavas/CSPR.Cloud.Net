using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }
}
