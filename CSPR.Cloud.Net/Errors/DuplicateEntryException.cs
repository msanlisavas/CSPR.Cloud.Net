using Microsoft.Extensions.Logging;
using System;

namespace CSPR.Cloud.Net.Errors
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message, ILogger? logger) : base(message)
        {
            logger?.LogError(message);
        }
    }
}
