using System.ComponentModel.DataAnnotations;

namespace Invoice.Web_Models
{
    public class InvoiceRequestModel
    {
        [Key]
        public int Id { get; set; }
        public int stateCode { get; set; }
        public string supType { get; set; } = string.Empty;
        public int perdYear { get; set; }
        public int perdMon { get; set; }
        public string outIn { get; set; } = string.Empty;
        public string for_upto { get; set; } = string.Empty;
    }
}
