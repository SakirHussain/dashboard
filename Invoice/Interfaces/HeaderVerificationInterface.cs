using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Invoice.Interfaces
{
    public interface HeaderVerificationInterface
    {
        public object clientVerification(string client_id, string client_secret);

    }
}
