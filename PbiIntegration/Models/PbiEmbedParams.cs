using Microsoft.PowerBI.Api.Models;
using System.Collections.Generic;

namespace PbiIntegration.Models
{
    public class PbiEmbedParams
    {
        public string Type { get; set; }

        public List<PbiEmbedReport> EmbedReport { get; set; }

        public EmbedToken EmbedToken { get; set; }
    }
}
