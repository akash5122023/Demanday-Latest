
namespace AdvanceCRM.Products.Repositories
{
    using Administration;
    using Products;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Microsoft.AspNetCore.Hosting;
    using AdvanceCRM.Web.Helpers;
    using AdvanceCRM;
    using System;
    using System.Data;
    using System.IO;
    using MyRow = ProductsRow;
    using Serenity.Extensions.DependencyInjection;
    using System.Linq;

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

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {

            public MySaveHandler(IRequestContext context) : base(context) { }

            protected override void BeforeSave()
            {
                if (!IsCreate)
                {
                    // If image is already permanent, prevent Serenity from throwing
                    if (!string.IsNullOrEmpty(Row.Image) && !Row.Image.StartsWith("temporary/"))
                    {
                        // Temporarily nullify the image so Serenity's ImageUploadBehavior won't throw
                        Row.Image = null;
                    }
                }
                if (this.IsCreate)
                {
                    Row.CompanyId = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                }
                base.BeforeSave();
            }
            protected override void AfterSave()
            {
                base.AfterSave();

                var env = Dependency.Resolve<IWebHostEnvironment>();
                string tempFolderPath = Path.Combine(env.ContentRootPath, "App_Data", "upload", "temporary");
                string productFolderPath = Path.Combine(env.ContentRootPath, "App_Data", "upload", "Products", "00000");

                // Ensure the product folder exists
                if (!Directory.Exists(productFolderPath))
                    Directory.CreateDirectory(productFolderPath);

                // Only move file if it's in temporary folder
                if (!string.IsNullOrEmpty(Row.Image) && Row.Image.StartsWith("temporary/"))
                {
                    string fileName = Path.GetFileName(Row.Image);
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                    string ext = Path.GetExtension(fileName);

                    // Move main image
                    string tempPath = Path.Combine(tempFolderPath, fileName);
                    string newPath = Path.Combine(productFolderPath, fileName);
                    if (File.Exists(tempPath))
                        File.Move(tempPath, newPath, overwrite: true);

                    // Move thumbnail if exists
                    string thumbFile = nameWithoutExt + "_t" + ext;
                    string tempThumbPath = Path.Combine(tempFolderPath, thumbFile);
                    string newThumbPath = Path.Combine(productFolderPath, thumbFile);
                    if (File.Exists(tempThumbPath))
                        File.Move(tempThumbPath, newThumbPath, overwrite: true);

                    // Update permanent path in database
                    UnitOfWork.Connection.Execute(
                        "UPDATE Products SET Image = @Image WHERE Id = @Id",
                        new { Image = "Products/00000/" + fileName, Id = Row.Id }
                    );

                    // Update Row.Image so inventory gets correct path
                    Row.Image = "Products/00000/" + fileName;
                }

                var uow = this.UnitOfWork;
                var inventoryRepo = new InventorySaveHandler(Context);

                if (IsCreate)
                {
                    // New product → create inventory
                    var inventoryRow = new InventoryRow
                    {
                        ProductsId = Row.Id,
                        Name = Row.Name,
                        Code = Row.Code,
                        Hsn = Row.HSN,
                        DivisionId = Row.DivisionId,
                        UnitId = Row.UnitId,
                        GroupId = Row.GroupId,
                        RawMaterial = Row.RawMaterial,
                        BranchId = Row.BranchId,
                        Description = Row.Description,
                        SellingPrice = Row.SellingPrice,
                        Mrp = Row.Mrp,
                        PurchasePrice = Row.PurchasePrice,
                        TaxId1 = Row.TaxId1,
                        TaxId2 = Row.TaxId2,
                        ChannelCustomerPrice = Row.ChannelCustomerPrice,
                        ResellerPrice = Row.ResellerPrice,
                        WholesalerPrice = Row.WholesalerPrice,
                        DealerPrice = Row.DealerPrice,
                        DistributorPrice = Row.DistributorPrice,
                        StockiestPrice = Row.StockiestPrice,
                        NationalDistributorPrice = Row.NationalDistributorPrice,
                        MinimumStock = Row.MinimumStock,
                        MaximumStock = Row.MaximumStock,
                        TechSpecs = Row.TechSpecs,
                        Image = Row.Image,
                        OpeningStock = Row.OpeningStock ?? 0 // cannot be null
                        
                    };
                    //inventoryRow.CompanyId = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                   
                    inventoryRepo.Create(uow, new SaveRequest<InventoryRow> { Entity = inventoryRow });
                }
                else if (IsUpdate)
                {
                    // Product updated → update or insert inventory
                    var fld = InventoryRow.Fields;

                    var existingInventory = uow.Connection.TryFirst<InventoryRow>(q => q
                        .Select(fld.ProductsId)
                        .Where(fld.ProductsId == Row.Id.Value));

                    if (existingInventory != null)
                    {
                        // Update existing inventory
                        string sqlUpdate = @"
                UPDATE Inventory
                SET 
                    Name = @Name,
                    Code = @Code,
                    Hsn = @HSN,
                    DivisionId = @DivisionId,
                    UnitId = @UnitId,
                    GroupId = @GroupId,
                    RawMaterial = @RawMaterial,
                    BranchId = @BranchId,
                    Description = @Description,
                    SellingPrice = @SellingPrice,
                    Mrp = @Mrp,
                    PurchasePrice = @PurchasePrice,
                    TaxId1 = @TaxId1,
                    TaxId2 = @TaxId2,
                    ChannelCustomerPrice = @ChannelCustomerPrice,
                    ResellerPrice = @ResellerPrice,
                    WholesalerPrice = @WholesalerPrice,
                    DealerPrice = @DealerPrice,
                    DistributorPrice = @DistributorPrice,
                    StockiestPrice = @StockiestPrice,
                    NationalDistributorPrice = @NationalDistributorPrice,
                    MinimumStock = @MinimumStock,
                    MaximumStock = @MaximumStock,
                    TechSpecs = @TechSpecs,
                    Image = @Image,
                    OpeningStock = @OpeningStock
                WHERE ProductsId = @ProductsId";

                        uow.Connection.Execute(sqlUpdate, new
                        {
                            Name = Row.Name,
                            Code = Row.Code,
                            Hsn = Row.HSN,
                            DivisionId = Row.DivisionId,
                            UnitId = Row.UnitId,
                            GroupId = Row.GroupId,
                            RawMaterial = Row.RawMaterial,
                            BranchId = Row.BranchId,
                            Description = Row.Description,
                            SellingPrice = Row.SellingPrice,
                            Mrp = Row.Mrp,
                            PurchasePrice = Row.PurchasePrice,
                            TaxId1 = Row.TaxId1,
                            TaxId2 = Row.TaxId2,
                            ChannelCustomerPrice = Row.ChannelCustomerPrice,
                            ResellerPrice = Row.ResellerPrice,
                            WholesalerPrice = Row.WholesalerPrice,
                            DealerPrice = Row.DealerPrice,
                            DistributorPrice = Row.DistributorPrice,
                            StockiestPrice = Row.StockiestPrice,
                            NationalDistributorPrice = Row.NationalDistributorPrice,
                            MinimumStock = Row.MinimumStock,
                            MaximumStock = Row.MaximumStock,
                            TechSpecs = Row.TechSpecs,
                            Image = Row.Image,
                            OpeningStock = Row.OpeningStock ?? 0,
                            ProductsId = Row.Id
                        });
                    }
                    else
                    {
                        // Insert new inventory if not exists
                        var newInventory = new InventoryRow
                        {
                            ProductsId = Row.Id,
                            Name = Row.Name,
                            Code = Row.Code,
                            Hsn = Row.HSN,
                            DivisionId = Row.DivisionId,
                            UnitId = Row.UnitId,
                            GroupId = Row.GroupId,
                            RawMaterial = Row.RawMaterial,
                            BranchId = Row.BranchId,
                            Description = Row.Description,
                            SellingPrice = Row.SellingPrice,
                            Mrp = Row.Mrp,
                            PurchasePrice = Row.PurchasePrice,
                            TaxId1 = Row.TaxId1,
                            TaxId2 = Row.TaxId2,
                            ChannelCustomerPrice = Row.ChannelCustomerPrice,
                            ResellerPrice = Row.ResellerPrice,
                            WholesalerPrice = Row.WholesalerPrice,
                            DealerPrice = Row.DealerPrice,
                            DistributorPrice = Row.DistributorPrice,
                            StockiestPrice = Row.StockiestPrice,
                            NationalDistributorPrice = Row.NationalDistributorPrice,
                            MinimumStock = Row.MinimumStock,
                            MaximumStock = Row.MaximumStock,
                            TechSpecs = Row.TechSpecs,
                            Image = Row.Image,
                            OpeningStock = Row.OpeningStock ?? 0
                            //CompanyId = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId
                        };

                        inventoryRepo.Create(uow, new SaveRequest<InventoryRow> { Entity = newInventory });
                    }
                }

                // Clear cache
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