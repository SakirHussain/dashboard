using System.ComponentModel.DataAnnotations;

namespace Invoice.Record_Models
{
    public class ForIdSix
    {
        [Key]
        public string gstin { get; set; } = string.Empty;
        public string? trade_name { get; set; }
        public decimal assess_curval { get; set; }
        public decimal sgst_curval { get; set; }
        public decimal cgst_curval { get; set; }
        public decimal igst_curval { get; set; }
        public decimal cess_curval { get; set; }
        public decimal assess_preval { get; set; }
        public decimal sgst_preval { get; set; }
        public decimal cgst_preval { get; set; }
        public decimal igst_preval { get; set; }
        public decimal cess_preval { get; set; }
    }
}
