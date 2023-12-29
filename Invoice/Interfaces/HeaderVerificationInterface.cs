using Invoice.Web_Models;

namespace Invoice.Interfaces
{
    /// <summary>
    /// Interface for verifying headers related to client authentication.
    /// </summary>
    public interface HeaderVerificationInterface
    {
        /// <summary>
        /// Performs client verification based on the provided request headers.
        /// </summary>
        /// <param name="requestHeaders">The request headers containing client authentication information.</param>
        /// <returns>True if client verification is successful; otherwise, false.</returns>
        bool ClientVerification(AuthRequestHeaders requestHeaders);
    }
}
