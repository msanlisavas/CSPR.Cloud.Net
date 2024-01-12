using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }
}
