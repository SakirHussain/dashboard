using System.Text;

namespace Invoice.WebModels
{
    public class ApiResponseModel
    {
        public int status { get; set; }
        public Error error { get; set; } = new Error();
        public string data { get; set; } = string.Empty;
    }
}
