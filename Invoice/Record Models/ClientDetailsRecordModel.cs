using System.ComponentModel.DataAnnotations;

namespace Invoice.RecordModels
{
    public class ClientDetailsRecordModel
    {
        [Key]
        [Required]
        public string client_id { get; set; } = string.Empty;
        [Required]
        public string client_secret { get; set;} = string.Empty;
    }
}
