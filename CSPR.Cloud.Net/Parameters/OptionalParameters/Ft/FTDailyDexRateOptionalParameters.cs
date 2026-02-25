using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Ft
{
    public class FTDailyDexRateOptionalParameters
    {
        [JsonProperty("ft_daily_rate")]
        public int? FtDailyRate { get; set; } = 0;
    }
}
