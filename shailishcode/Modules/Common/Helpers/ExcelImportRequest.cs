using Serenity.Services;
using System;
using System.Collections.Generic;

namespace AdvanceCRM
{
    public class ExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }

        internal void CheckNotNull()
        {
            throw new NotImplementedException();
        }
    }

    public class ExcelImportResponse : ServiceResponse
    {
        public int Inserted { get; set; }
        public int Updated { get; set; }
        public List<string> ErrorList { get; set; }
    }
}