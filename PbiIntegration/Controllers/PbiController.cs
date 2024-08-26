using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PbiIntegration.Configs;
using PbiIntegration.Interfaces;
using PbiIntegration.Models;
using System.Text.Json;

namespace PbiIntegration.Controllers
{
    [ApiController]
    [Route("PowerBI")]
    public class PbiController : Controller
    {
        private readonly EntraID _entraID;
        private readonly Pbi _pbi;
        private readonly IPbiEmbedService _pbiEmbedService;

        public PbiController(IOptions<EntraID> entraID, IOptions<Pbi> pbi, IPbiEmbedService pbiEmbedService)
        {
            _entraID = entraID.Value;
            _pbi = pbi.Value;
            _pbiEmbedService = pbiEmbedService;
        }

        [HttpGet("Embed")]
        public string Embed()
        {
            try
            {
                var workspaceId = new Guid(_pbi.WorkspaceId);
                var reportId = new Guid(_pbi.ReportId);

                string validacaoParamConfig = PbiConfigValidator.ValidarConfigPbi(_entraID);    

                if (validacaoParamConfig is not null)
                {
                    HttpContext.Response.StatusCode = 400;
                    return validacaoParamConfig;
                }

                // Chamando o serviço da API do PowerBI para embutir os dados
                var embedParams = _pbiEmbedService.GetEmbedParams(workspaceId, reportId);
                return JsonSerializer.Serialize<PbiEmbedParams>(embedParams);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return ex.Message + "\n\n" + ex.StackTrace;
            }
        }
    }
}
