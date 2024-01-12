using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class InvalidParamException : Exception
    {
        public InvalidParamException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }

}
