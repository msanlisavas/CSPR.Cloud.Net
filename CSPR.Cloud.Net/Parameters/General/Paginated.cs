namespace CSPR.Cloud.Net.Parameters.General
{
    /// <summary>
    /// Represents pagination information for a request.
    /// </summary>
    public class Paginated
    {
        /// <summary>
        /// Gets or sets the page number for the request.
        /// Default is 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// Gets or sets the page size for the request.
        /// Default is 10. Max is 250.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
