namespace Invoice.WebModels
{
    public class Error
    {
        public int errorCode {  get; set; } = 0;
        public string errorMessage { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.Now;

    }
}
