using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Record_Models
{
    public class ActualTaxCollectionRecordModel
    {
        [Key]
        public int state_cd { get; set; }
        public int perd_year { get; set; }
        public int perd_mon { get; set; }
        public decimal cgst_val { get; set; }
        public decimal sgst_val { get; set; }
        public decimal igst_val { get; set; }
        public decimal cess_val { get; set; }
        public decimal imp_val { get; set; }
        public DateTime upd_dt { get; set; }
        public decimal pred_tax { get; set; }
        public decimal total_act_tax { get; set; }
        public decimal prediction_val{ get; set; }
    }
}
