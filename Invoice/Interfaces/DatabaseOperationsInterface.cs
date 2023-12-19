using Invoice.Web_Models;
using System.Text;

namespace Invoice.Interfaces
{
    public interface DatabaseOperationsInterface
    {
        string topNStates(string n);
        Dictionary<string, string> ClientIdentityFetch(AuthRequestHeaders request);
        bool LoginDetailsVerify(TokenRequestModel req);
        AuthResponseModel TokenCheck(string loginId);
    }
}
