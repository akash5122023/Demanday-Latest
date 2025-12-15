namespace StagingAPI.Models
{
    public class JobTable
    {
        public string JobDescription { get; set; }
        public string ModuleName { get; set; }
        public string ObjType { get; set; }
        public string Direction { get; set; }
        public string SourceType { get; set; }
        public string SourceTable { get; set; }
        public string SourceStatusField { get; set; }
        public string SourceKeyName { get; set; }
        public string DestinationType { get; set; }
        public string DestinationTable { get; set; }
        public string DestinationKeyName { get; set; }
        public string Parameter { get; set; }
        public string Query { get; set; }
        public string API_Address { get; set; }
        public string API_id { get; set; }
        public string Target_Connection_id { get; set; }
        public string Source_Connection_id { get; set; }
        public string SuccessStatus { get; set; }
        public string FailStatus { get; set; }
        public string CronJobString { get; set; }
        public string Mode { get; set; }//A-Add , U-Update
        public string FilterExpression { get; set; }//it will filter the datatable once it is polulated with sap values.
        public bool IsActive { get; set; }
        
    }
}