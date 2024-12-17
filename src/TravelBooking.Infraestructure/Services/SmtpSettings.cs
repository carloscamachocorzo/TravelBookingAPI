namespace TravelBooking.Infraestructure.Services
{
    /// <summary>
    /// Represents the settings for configuring the SMTP client.
    /// </summary>
    public class SmtpSettings
    {
        /// <summary>
        /// Gets or sets the SMTP server host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port used by the SMTP server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SSL is enabled for the SMTP connection.
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the username for the SMTP authentication.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for the SMTP authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the default sender email address.
        /// </summary>
        public string FromEmail { get; set; }
    }

}
