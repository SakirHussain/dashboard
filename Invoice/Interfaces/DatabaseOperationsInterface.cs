using Invoice.Web_Models;
using System.Text;

namespace Invoice.Interfaces
{
    public interface DatabaseOperationsInterface
    {
        string topNStates(string n);
        (string, string, string) ClientIdentityFetch(AuthRequestHeaders request);
        (string, string) LoginDetailsFetch(string loginId);
        AuthResponseModel TokenCheck(string loginId);
    }
}
