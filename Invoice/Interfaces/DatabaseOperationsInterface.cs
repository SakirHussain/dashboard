using System.Text;

namespace Invoice.Interfaces
{
    public interface DatabaseOperationsInterface
    {
        string topNStates(int n);
        (string, string) ClientIdentityFetch(string client_id);
        (string, string) LoginDetailsFetch(string loginId);
        Guid TokenCheck(string loginId);
    }
}
