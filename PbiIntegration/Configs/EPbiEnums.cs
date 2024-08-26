namespace PbiIntegration.Configs
{
    public static class EPbiEnums
    {
        #region Configurações do PowerBI

        public const string
            PBI_E_AUTHENTICATIONMODE = "Certifique-se que o AuthenticationMode está corretamente definido no arquivo launchSettings.json",
            PBI_E_AUTHORITY = "Certifique-se que o Authority está corretamente definido no arquivo launchSettings.json",
            PBI_E_CLIENTID = "Certifique-se que o ClientId está corretamente definido no arquivo launchSettings.json",
            PBI_E_TENANTID = "Certifique-se que o TenantId está corretamente definido no arquivo launchSettings.json",
            PBI_E_SCOPE = "Certifique-se que o Scope está corretamente definido no arquivo launchSettings.json",
            PBI_E_WORKSPACEID = "Certifique-se que o WorkspaceId está corretamente definido no arquivo launchSettings.json",
            PBI_E_REPORTID = "Certifique-se que o ReportId está corretamente definido no arquivo launchSettings.json",
            PBI_E_PBIUSERNAME = "Certifique-se que o PbiUsername está corretamente definido com um e-mail de usuário Mestre no arquivo launchSettings.json",
            PBI_E_PBIPASSWORD = "Certifique-se que o PbiPassword está corretamente definido com uma senha de usuário Mestre no arquivo launchSettings.json",
            PBI_E_CLIENTSECRET = "Certifique-se que o ClientSecret está corretamente definido no arquivo launchSettings.json";

        // Nomes relacionados ao PowerBI
        public const string
            PBI_N_NOME_API_SERVICE_ROOT = "https://api.powerbi.com";

        #endregion Configurações do PowerBI
    }
}
