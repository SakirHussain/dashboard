using System.ComponentModel.DataAnnotations;

namespace Invoice.Web_Models
{
    public class InvoiceResponseModel
    {
        [Key]
        public int perd_year { get; set; }
        public string mon { get; set; }
        public decimal assess_val { get; set; }
        public decimal sgst_value { get; set; }
        public decimal cgst_value { get; set; }
        public decimal igst_value { get; set; }
        public decimal cess_value { get; set; }
    }
}
