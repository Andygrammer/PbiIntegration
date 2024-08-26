using PbiIntegration.Models;
using System.Runtime.InteropServices;

namespace PbiIntegration.Interfaces
{
    public interface IPbiEmbedService
    {
        PbiEmbedParams GetEmbedParams(Guid workspaceId, Guid reportId, [Optional] Guid additionalDatasetId);
    }
}
