namespace Invoice.RecordModels
{
    public class StatesRecordModel
    {
        public Guid id { get; set; }
        public string state { get; set; } = string.Empty;
        public int grossIncome { get; set; }

    }
}
