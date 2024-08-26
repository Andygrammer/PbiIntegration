namespace PbiIntegration.Models
{
    public class EntraID
    {
        // 'masteruser' ou 'serviceprincipal'
        public string AuthenticationMode { get; set; }

        // URL para disparar o request de autorização
        public string AuthorityUri { get; set; }

        // Client Id (Application Id) da app no Entra ID
        public string ClientId { get; set; }

        // Id do inquilino (tenant) no Entra ID no qual a app está configurada. Requer AuthenticationMode 'serviceprincipal'
        public string TenantId { get; set; }

        // Escopo de permissões que a app possui no Entra ID
        public string[] Scope { get; set; }

        // E-mail de usuário mestre. Requer AuthenticationMode 'masteruser' (não recomendado)
        public string PbiUsername { get; set; }

        // Senha de usuário mestre. Requer AuthenticationMode 'masteruser' (não recomendado)
        public string PbiPassword { get; set; }

        // Client Secret (App Secret) da app no Entra ID. Requer AuthenticationMode 'serviceprincipal'
        public string ClientSecret { get; set; }
    }
}
