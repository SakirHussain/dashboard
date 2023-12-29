using Invoice.Web_Models;
using System.Collections.Generic;

namespace Invoice.Interfaces
{
    /// <summary>
    /// Interface for defining database operations.
    /// </summary>
    public interface DatabaseOperationsInterface
    {
        /// <summary>
        /// Retrieves a report based on the provided invoice request.
        /// </summary>
        /// <param name="requestModel">The model containing information for the report.</param>
        /// <returns>An object representing the generated report.</returns>
        object GetReport(InvoiceRequestModel requestModel);

        /// <summary>
        /// Fetches client identity information based on the provided request headers.
        /// </summary>
        /// <param name="request">The request headers containing client information.</param>
        /// <returns>A dictionary containing client identity information.</returns>
        Dictionary<string, string> ClientIdentityFetch(AuthRequestHeaders request);

        /// <summary>
        /// Verifies login details based on the provided token request.
        /// </summary>
        /// <param name="req">The token request model containing login details.</param>
        /// <returns>True if login details are valid; otherwise, false.</returns>
        bool LoginDetailsVerify(TokenRequestModel req);

        /// <summary>
        /// Checks the validity of a token based on the provided login ID.
        /// </summary>
        /// <param name="loginId">The login ID for which the token is checked.</param>
        /// <returns>An authentication response model containing token information.</returns>
        AuthResponseModel TokenCheck(string loginId);
    }
}
