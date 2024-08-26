using System;

namespace PbiIntegration.Models
{
    public class PbiEmbedReport
    {
        public Guid ReportId { get; set; }

        public string ReportName { get; set; }

        public string EmbedUrl { get; set; }
    }
}
