using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Products.InventoryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Products.InventoryRow;

namespace AdvanceCRM.Products
{
    public interface IInventorySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class InventorySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IInventorySaveHandler
    {
        public InventorySaveHandler(IRequestContext context)
             : base(context)
        {
        }
        protected override void BeforeSave()
        {
            if (IsCreate)
            {
                // Automatically set CompanyId for new inventory rows
                Row.CompanyId = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
            }
            base.BeforeSave();
        }
    }
}