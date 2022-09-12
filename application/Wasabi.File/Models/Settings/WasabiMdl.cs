namespace Wasabi.File.Models.Settings
{
    /// <summary>
    /// Wasabi settings
    /// </summary>
    internal class WasabiMdl
    {
        /// <summary>
        /// Wasabi end point url
        /// </summary>
        public string URL { get; set; } = string.Empty;
        /// <summary>
        /// Wasab credentials
        /// </summary>
        public WasabiCredentialsMdl Credentials { get; set; } = new WasabiCredentialsMdl();
    }

    /// <summary>
    /// Wasab credentials
    /// </summary>
    internal class WasabiCredentialsMdl
    {
        /// <summary>
        /// Wasabi credentials access key
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        /// Wasabi credentials secret key
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;
    }
}
