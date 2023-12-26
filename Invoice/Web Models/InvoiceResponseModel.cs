using System.ComponentModel.DataAnnotations;

namespace Invoice.Web_Models
{
    public class InvoiceResponseModel
    {
        [Key]
        public int perdYear { get; set; }
        public string mon { get; set; }
        public decimal assessVal { get; set; }
        public decimal sgstValue { get; set; }
        public decimal cgstValue { get; set; }
        public decimal igstValue { get; set; }
        public decimal cessValue { get; set; }
    }
}
