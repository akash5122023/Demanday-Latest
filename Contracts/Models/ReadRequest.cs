
using Newtonsoft.Json.Linq;

namespace StagingAPI.Models
{
    public class ReadRequest
    {
        public string ModuleName { get; set; }
        public string CompanyName { get; set; }

    }
    public class ReadResponse
    {
        public string ModuleName { get; set; }
        public string CompanyName { get; set; }
         public JObject Data { get; set; }
    }
    public class UpdateStatusRequest
    {
        public string ModuleName { get; set; }
        public string CompanyName { get; set; }
        public string PKName { get; set; }
        public string PKValue { get; set; }
        public string Status { get; set; }
    }
    public class WriteRequest
    {
        public string ModuleName { get; set; }
        public string TableName { get; set; }
        public string CompanyName { get; set; }
        public string TargetDatabaseType{get;set;}
        public int targetConnectionId { get; set; }
        public JArray Data { get; set; }
        public string Query { get; set; }
    }
   
}

 

