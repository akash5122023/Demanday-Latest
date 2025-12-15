
namespace AdvanceCRM.Products.Repositories
{
    using Administration;
    using Products;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Microsoft.AspNetCore.Hosting;
    using AdvanceCRM.Web.Helpers;
    using AdvanceCRM;
    using System;
    using System.Data;
    using System.IO;
    using MyRow = ProductsRow;
    using Serenity.Extensions.DependencyInjection;

    public class ProductsRepository : BaseRepository
    {
    public ProductsRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            var response = new MyDeleteHandler(Context).Process(uow, request);

            var companyId = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
            var cacheKey = $"MultiCompanyLookup:Products.Products:{companyId}:{MyRow.Fields.GenerationKey}";
            LocalCache.Remove(cacheKey);
            LocalCache.ExpireGroupItems(MyRow.Fields.GenerationKey);

            return response;
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
             return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow> {

            public MySaveHandler(IRequestContext context) : base(context) { }

            protected override void BeforeSave()
            {
                if (this.IsCreate)
                {
                    Row.CompanyId = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                }

                //base.BeforeSave();

                //var connection = SqlConnections.NewByKey("Default");

                //int Company = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;

                //var e = ProductsRow.Fields;

                //int products = connection.Count<ProductsRow>(e.CompanyId == Company && e.Name == Request.Entity.Name);

                //if (this.IsCreate)
                //{
                //    if (products > 0)
                //    {
                //        throw new Exception("Already Exists Same Products in current Company");
                //    }
                //}

                base.BeforeSave();


                var env = Dependency.Resolve<IWebHostEnvironment>();
                string tempFolderPath = Path.Combine(env.ContentRootPath, "App_Data", "upload", "temporary");
                string productFolderPath = Path.Combine(env.ContentRootPath, "App_Data", "upload", "Products", "00000");
                // Ensure the product folder exists
                if (!Directory.Exists(productFolderPath))
                {
                    Directory.CreateDirectory(productFolderPath);
                }
                // Extract the base name from the Row.Image property (e.g., "abc.png" becomes "abc")
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Row.Image);
                var fileExtension = Path.GetExtension(Row.Image);

                // Construct the expected file names
                string primaryFileName = fileNameWithoutExtension + fileExtension; // e.g., "abc.png"
                string secondaryFileName = fileNameWithoutExtension + "_t" + ".jpg"; // e.g., "abc_t.png"

                // Find the primary and secondary file paths
                string primaryFilePath = Path.Combine(tempFolderPath, primaryFileName);
                string secondaryFilePath = Path.Combine(tempFolderPath, secondaryFileName);

                // Check if the primary file exists and copy it to the product folder
                if (File.Exists(primaryFilePath))
                {
                    var newPrimaryFilePath = Path.Combine(productFolderPath, primaryFileName);
                    File.Copy(primaryFilePath, newPrimaryFilePath, overwrite: true);
                }

                // Check if the secondary file exists and copy it to the product folder
                if (File.Exists(secondaryFilePath))
                {
                    var newSecondaryFilePath = Path.Combine(productFolderPath, secondaryFileName);
                    File.Copy(secondaryFilePath, newSecondaryFilePath, overwrite: true);
                }

                // Update the database path
                Row.Image = "Products/00000/" + primaryFileName; // This should reflect the primary file's new path


            }

            protected override void AfterSave()
            {
                base.AfterSave();

                var companyId = Row.CompanyId ?? ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                var cacheKey = $"MultiCompanyLookup:Products.Products:{companyId}:{MyRow.Fields.GenerationKey}";
                LocalCache.Remove(cacheKey);
                LocalCache.ExpireGroupItems(MyRow.Fields.GenerationKey);
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow>
         {
             public MyDeleteHandler(IRequestContext context) : base(context) { }

         }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
    }
}