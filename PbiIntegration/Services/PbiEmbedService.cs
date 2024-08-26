using System.Runtime.InteropServices;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using PbiIntegration.Configs;
using PbiIntegration.Interfaces;
using PbiIntegration.Models;

namespace PbiIntegration.Services
{
    public class PbiEmbedService : IPbiEmbedService
    {
        private readonly IEntraIDService _entraIDService;

        public PbiEmbedService(IEntraIDService entraIDService)
        {
            _entraIDService = entraIDService;
        }

        public PowerBIClient GetPowerBIClient()
        {
            var tokenCredentials = new TokenCredentials(_entraIDService.GetAccessToken(), "Bearer");
            return new PowerBIClient(new Uri(EPbiEnums.PBI_N_NOME_API_SERVICE_ROOT), tokenCredentials);
        }

        /// <summary>
        /// Obtém os parâmetros necessários para se fazer embed de um relatório do PBI (report)
        /// </summary>
        /// <returns> Wrapper object contendo Embed token, Embed URL, Report Id e Report Name </returns>
        public PbiEmbedParams GetEmbedParams(Guid workspaceId, Guid reportId, [Optional] Guid additionalDatasetId)
        {
            PowerBIClient pbiClient = GetPowerBIClient();
            EmbedToken embedToken;

            var pbiReport = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);

            var datasetIds = new List<Guid>
            {
                Guid.Parse(pbiReport.DatasetId)
            };

            if (additionalDatasetId != Guid.Empty)
            {
                datasetIds.Add(additionalDatasetId);
            }

            embedToken = GetEmbedToken(reportId, datasetIds, workspaceId);

            var embedReports = new List<PbiEmbedReport>
            {
                new() {
                    ReportId = pbiReport.Id, ReportName = pbiReport.Name, EmbedUrl = pbiReport.EmbedUrl
                }
            };

            var embedParams = new PbiEmbedParams
            {
                EmbedReport = embedReports,
                Type = "Report",
                EmbedToken = embedToken
            };

            return embedParams;
        }

        /// <summary>
        /// Obtém um token de embed para trabalhar com recursos do PBI
        /// </summary>
        /// <returns> Token de embed </returns>
        public EmbedToken GetEmbedToken(Guid reportId, IList<Guid> datasetIds, [Optional] Guid targetWorkspaceId)
        {
            PowerBIClient pbiClient = GetPowerBIClient();

            // Criando uma requisição para obter o token de Embed
            var tokenRequest = new GenerateTokenRequestV2(

                reports: [new(reportId)],

                datasets: datasetIds.Select(datasetId => new GenerateTokenRequestV2Dataset(datasetId.ToString())).ToList(),

                targetWorkspaces: targetWorkspaceId != Guid.Empty
                ? new List<GenerateTokenRequestV2TargetWorkspace> { new(targetWorkspaceId) }
                : null
            );

            var embedToken = pbiClient.EmbedToken.GenerateToken(tokenRequest);

            return embedToken;
        }
    }
}
