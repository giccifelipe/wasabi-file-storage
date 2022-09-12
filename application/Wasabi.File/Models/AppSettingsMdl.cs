using Microsoft.Extensions.Configuration;
using Wasabi.File.Models.Enum;
using Wasabi.File.Models.Settings;

namespace Wasabi.File.Models
{
    /// <summary>
    /// App settings model to mirror the appsettings.json
    /// </summary>
    internal class AppSettingsMdl
    {
        private readonly IConfigurationRoot configuration;

        internal AppSettingsMdl(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Wasabi settings
        /// </summary>
        public WasabiMdl? Wasabi { get; set; }

        /*
         * Add extra property if you decide to expand the appsettings.json
         */





        /*
         * Methods
         */

        /// <summary>
        /// Build settings from the appsettings.json
        /// </summary>
        /// <param name="settingsTypes"></param>
        public void BuildSettings(SettingsTypes settingsTypes)
        {
            switch (settingsTypes)
            {
                case SettingsTypes.Wasabi:
                    WasabiSetting();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Build wasabi settings
        /// </summary>
        private void WasabiSetting()
        {
            Wasabi = null;

            IConfigurationSection _wasabi = configuration.GetSection("Wasabi");
            if (_wasabi != null)
            {
                string WasabiURL = _wasabi.GetSection("URL")?.Value ?? string.Empty;
                string WasabiCredentialsAccessKey = string.Empty;
                string WasabiCredentialsSecretKey = string.Empty;
                IConfigurationSection WasabiCredentials = _wasabi.GetSection("Credentials");

                if (WasabiCredentials != null)
                {
                    WasabiCredentialsAccessKey = WasabiCredentials.GetSection("AccessKey").Value;
                    WasabiCredentialsSecretKey = WasabiCredentials.GetSection("SecretKey").Value;
                }

                if (!string.IsNullOrEmpty(WasabiURL) && !string.IsNullOrEmpty(WasabiCredentialsAccessKey) && !string.IsNullOrEmpty(WasabiCredentialsSecretKey))
                {
                    Wasabi = new WasabiMdl
                    {
                        Credentials = new WasabiCredentialsMdl
                        {
                            AccessKey = WasabiCredentialsAccessKey,
                            SecretKey = WasabiCredentialsSecretKey
                        },
                        URL = WasabiURL
                    };
                }
            }
        }
    }
}
