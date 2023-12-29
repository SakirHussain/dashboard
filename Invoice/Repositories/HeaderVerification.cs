using System;
using System.Collections.Generic;
using Invoice.Data;
using Invoice.Interfaces;
using Invoice.Web_Models;

namespace Invoice.Repositories
{
    /// <summary>
    /// Repository class implementing HeaderVerificationInterface for verifying client headers.
    /// </summary>
    public class HeaderVerification : HeaderVerificationInterface
    {
        private readonly DatabaseOperationsInterface _interDbOp;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderVerification"/> class.
        /// </summary>
        /// <param name="interDbOp">The database operations interface.</param>
        public HeaderVerification(DatabaseOperationsInterface interDbOp)
        {
            _interDbOp = interDbOp;
        }

        /// <summary>
        /// Performs client verification based on the provided request headers.
        /// </summary>
        /// <param name="request">The request headers containing client authentication information.</param>
        /// <returns>True if client verification is successful; otherwise, false.</returns>
        public bool ClientVerification(AuthRequestHeaders request)
        {
            Dictionary<string, string> clientDetails = _interDbOp.ClientIdentityFetch(request);

            try
            {
                // Check if the token is null or empty
                if (string.IsNullOrEmpty(clientDetails["token"]))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            // Check if the required headers are not null
            if (request.ClientId != null && request.ClientSecret != null && request.LoginId != null && request.AuthToken != null)
            {
                // Check if client ID and client secret match the fetched client details
                if (request.ClientId == clientDetails["client_id"] && request.ClientSecret == clientDetails["client_secret"])
                {
                    // Check if the token is valid based on expiry
                    if (DateTime.Parse(clientDetails["expiry"]) > DateTime.Now)
                    {
                        // Check if the provided auth token matches the fetched token
                        return (String.Equals(request.AuthToken, clientDetails["token"], StringComparison.OrdinalIgnoreCase));
                    }
                    else { return false; }
                }
                else { return false; }
            }
            else { return false; }
        }
    }
}
