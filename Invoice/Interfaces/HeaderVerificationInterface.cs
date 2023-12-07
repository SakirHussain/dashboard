using Invoice.Web_Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Invoice.Interfaces
{
    public interface HeaderVerificationInterface
    {
        public bool clientVerification(AuthRequestHeaders requestHeaders);

    }
}
