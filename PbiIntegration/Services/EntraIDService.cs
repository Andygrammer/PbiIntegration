using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using PbiIntegration.Interfaces;
using PbiIntegration.Models;

namespace PbiIntegration.Services
{
    public class EntraIDService : IEntraIDService
    {
        private readonly EntraID _entraID;

        public EntraIDService(IOptions<EntraID> entraID)
        {
            _entraID = entraID.Value;
        }

        public string GetAccessToken()
        {
            AuthenticationResult authenticationResult;

            // Autenticação Entra ID via usuário Mestre 
            if (_entraID.AuthenticationMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase))
            {
                // Criando um cliente público para autorizar e conectar a app ao Entra ID
                IPublicClientApplication clientApp = PublicClientApplicationBuilder.Create(_entraID.ClientId).WithAuthority(_entraID.AuthorityUri).Build();
                var userAccounts = clientApp.GetAccountsAsync().Result;

                try
                {
                    // Retornando o token de acesso do cache, se disponível
                    authenticationResult = clientApp.AcquireTokenSilent(_entraID.Scope, userAccounts.FirstOrDefault()).ExecuteAsync().Result;
                }
                catch (MsalUiRequiredException)
                {
                    authenticationResult = clientApp.AcquireTokenByUsernamePassword(_entraID.Scope, _entraID.PbiUsername, _entraID.PbiPassword).ExecuteAsync().Result;
                }
            }

            // Autenticação Entra ID via Service Principal (mais seguro e recomendado pela Microsoft) - nossa abordagem
            else if (_entraID.AuthenticationMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase))
            {
                // Neste caso, devemos passar o TenantId no AuthorityUri
                var tenantSpecificUrl = _entraID.AuthorityUri.Replace("organizations", _entraID.TenantId);

                // Criando um client confidencial para autorizar a app a integrar o PowerBI com o Entra ID
                IConfidentialClientApplication clientApp = ConfidentialClientApplicationBuilder
                                                                                .Create(_entraID.ClientId)
                                                                                .WithClientSecret(_entraID.ClientSecret)
                                                                                .WithAuthority(tenantSpecificUrl)
                                                                                .Build();

                // Retornando o token de acesso
                authenticationResult = clientApp.AcquireTokenForClient(_entraID.Scope).ExecuteAsync().Result;
            }

            else
            {
                // Tratar conforme a necessidade
                return null;
            }

            return authenticationResult.AccessToken;
        }
    }
}
