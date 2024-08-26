using PbiIntegration.Models;

namespace PbiIntegration.Configs
{
    public class PbiConfigValidator
    {
        public static string ValidarConfigPbi(EntraID entraID)
        {
            string message = null;

            bool autorizacaoViaMasterUser = entraID.AuthenticationMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase);
            bool autorizacaoViaServicePrincipal = entraID.AuthenticationMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase);

            if (string.IsNullOrWhiteSpace(entraID.AuthenticationMode))
            {
                message = EPbiEnums.PBI_E_AUTHENTICATIONMODE;
            }
            else if (string.IsNullOrWhiteSpace(entraID.AuthorityUri))
            {
                message = EPbiEnums.PBI_E_AUTHORITY;
            }
            else if (string.IsNullOrWhiteSpace(entraID.ClientId))
            {
                message = EPbiEnums.PBI_E_CLIENTID;
            }
            else if (autorizacaoViaServicePrincipal && string.IsNullOrWhiteSpace(entraID.TenantId))
            {
                message = EPbiEnums.PBI_E_TENANTID;
            }
            else if (entraID.Scope is null || entraID.Scope.Length == 0)
            {
                message = EPbiEnums.PBI_E_SCOPE;
            }
            else if (autorizacaoViaMasterUser && string.IsNullOrWhiteSpace(entraID.PbiUsername))
            {
                message = EPbiEnums.PBI_E_PBIUSERNAME;
            }
            else if (autorizacaoViaMasterUser && string.IsNullOrWhiteSpace(entraID.PbiPassword))
            {
                message = EPbiEnums.PBI_E_PBIPASSWORD;
            }
            else if (autorizacaoViaServicePrincipal && string.IsNullOrWhiteSpace(entraID.ClientSecret))
            {
                message = EPbiEnums.PBI_E_CLIENTSECRET;
            }
            else
            {
                return message;
            }

            return message;
        }
    }
}
