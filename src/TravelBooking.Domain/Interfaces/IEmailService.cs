namespace TravelBooking.Domain.Interfaces
{
    /// <summary>
    /// Interface for email sending services.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Asynchronously sends an email to the specified recipient.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendEmailAsync(string to, string subject, string body);
    }
}
