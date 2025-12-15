namespace AdvanceCRM.ThirdParty {
    export interface WcOrderDetailsRow {
        Id?: number;
        Wcoid?: string;
        ParentId?: string;
        Status?: string;
        Currency?: string;
        IncludedTax?: string;
        DateCreated?: string;
        DateModified?: string;
        DiscountTotal?: string;
        DiscountTax?: string;
        ShippingTotal?: string;
        ShipppingTax?: string;
        CartTax?: string;
        Total?: string;
        TotalTax?: string;
        CustomerId?: string;
        OrderKey?: string;
        BFirstName?: string;
        BLastName?: string;
        BCompany?: string;
        BEmail?: string;
        BPhone?: string;
        BPostCode?: string;
        BAddress1?: string;
        BAddress2?: string;
        BCity?: string;
        BState?: string;
        BCountry?: string;
        SFirstName?: string;
        SLastName?: string;
        SCompany?: string;
        SPhone?: string;
        SPostCode?: string;
        SAddress1?: string;
        SAddress2?: string;
        SCity?: string;
        SState?: string;
        SCountry?: string;
        PaymentMethod?: string;
        TransactionId?: string;
        CustomerIp?: string;
        ItemId?: string;
        ItemName?: string;
        ProductId?: string;
        VariationId?: string;
        Quantity?: string;
        TaxClass?: string;
        SubTotal?: string;
        SubTotaltax?: string;
        ItemTotal?: string;
        ItemTotaltax?: string;
        TaxRateCode?: string;
        TaxRateId?: string;
        TaxLabel?: string;
        TaxCompound?: string;
        TaxTotal?: string;
        TaxShippingTotal?: string;
        TaxRatePercent?: string;
        IsMoved?: boolean;
    }

    export namespace WcOrderDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Wcoid';
        export const localTextPrefix = 'ThirdParty.WcOrderDetails';
        export const deletePermission = 'Woocommerce:Inbox';
        export const insertPermission = 'Woocommerce:Inbox';
        export const readPermission = 'Woocommerce:Inbox';
        export const updatePermission = 'Woocommerce:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Wcoid = "Wcoid",
            ParentId = "ParentId",
            Status = "Status",
            Currency = "Currency",
            IncludedTax = "IncludedTax",
            DateCreated = "DateCreated",
            DateModified = "DateModified",
            DiscountTotal = "DiscountTotal",
            DiscountTax = "DiscountTax",
            ShippingTotal = "ShippingTotal",
            ShipppingTax = "ShipppingTax",
            CartTax = "CartTax",
            Total = "Total",
            TotalTax = "TotalTax",
            CustomerId = "CustomerId",
            OrderKey = "OrderKey",
            BFirstName = "BFirstName",
            BLastName = "BLastName",
            BCompany = "BCompany",
            BEmail = "BEmail",
            BPhone = "BPhone",
            BPostCode = "BPostCode",
            BAddress1 = "BAddress1",
            BAddress2 = "BAddress2",
            BCity = "BCity",
            BState = "BState",
            BCountry = "BCountry",
            SFirstName = "SFirstName",
            SLastName = "SLastName",
            SCompany = "SCompany",
            SPhone = "SPhone",
            SPostCode = "SPostCode",
            SAddress1 = "SAddress1",
            SAddress2 = "SAddress2",
            SCity = "SCity",
            SState = "SState",
            SCountry = "SCountry",
            PaymentMethod = "PaymentMethod",
            TransactionId = "TransactionId",
            CustomerIp = "CustomerIp",
            ItemId = "ItemId",
            ItemName = "ItemName",
            ProductId = "ProductId",
            VariationId = "VariationId",
            Quantity = "Quantity",
            TaxClass = "TaxClass",
            SubTotal = "SubTotal",
            SubTotaltax = "SubTotaltax",
            ItemTotal = "ItemTotal",
            ItemTotaltax = "ItemTotaltax",
            TaxRateCode = "TaxRateCode",
            TaxRateId = "TaxRateId",
            TaxLabel = "TaxLabel",
            TaxCompound = "TaxCompound",
            TaxTotal = "TaxTotal",
            TaxShippingTotal = "TaxShippingTotal",
            TaxRatePercent = "TaxRatePercent",
            IsMoved = "IsMoved"
        }
    }
}
