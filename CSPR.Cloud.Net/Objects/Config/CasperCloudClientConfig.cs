using System;

namespace CSPR.Cloud.Net.Objects.Config
{
    public class CasperCloudClientConfig
    {
        public string ApiKey { get; set; }

        public CasperCloudClientConfig(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("API key is required.", nameof(apiKey));

            ApiKey = apiKey;
        }

    }

}
