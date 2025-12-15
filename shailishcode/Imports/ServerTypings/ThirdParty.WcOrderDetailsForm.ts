namespace AdvanceCRM.ThirdParty {
    export interface WcOrderDetailsForm {
        Wcoid: Serenity.StringEditor;
        ParentId: Serenity.StringEditor;
        Status: Serenity.StringEditor;
        Currency: Serenity.StringEditor;
        IncludedTax: Serenity.StringEditor;
        DateCreated: Serenity.StringEditor;
        DateModified: Serenity.StringEditor;
        DiscountTotal: Serenity.StringEditor;
        DiscountTax: Serenity.StringEditor;
        ShippingTotal: Serenity.StringEditor;
        ShipppingTax: Serenity.StringEditor;
        CartTax: Serenity.StringEditor;
        Total: Serenity.StringEditor;
        TotalTax: Serenity.StringEditor;
        CustomerId: Serenity.StringEditor;
        OrderKey: Serenity.StringEditor;
        BFirstName: Serenity.StringEditor;
        BLastName: Serenity.StringEditor;
        BCompany: Serenity.StringEditor;
        BEmail: Serenity.StringEditor;
        BPhone: Serenity.StringEditor;
        BPostCode: Serenity.StringEditor;
        BAddress1: Serenity.StringEditor;
        BAddress2: Serenity.StringEditor;
        BCity: Serenity.StringEditor;
        BState: Serenity.StringEditor;
        BCountry: Serenity.StringEditor;
        SFirstName: Serenity.StringEditor;
        SLastName: Serenity.StringEditor;
        SCompany: Serenity.StringEditor;
        SPhone: Serenity.StringEditor;
        SPostCode: Serenity.StringEditor;
        SAddress1: Serenity.StringEditor;
        SAddress2: Serenity.StringEditor;
        SCity: Serenity.StringEditor;
        SState: Serenity.StringEditor;
        SCountry: Serenity.StringEditor;
        PaymentMethod: Serenity.StringEditor;
        TransactionId: Serenity.StringEditor;
        CustomerIp: Serenity.StringEditor;
        ItemId: Serenity.StringEditor;
        ItemName: Serenity.StringEditor;
        ProductId: Serenity.StringEditor;
        VariationId: Serenity.StringEditor;
        Quantity: Serenity.StringEditor;
        TaxClass: Serenity.StringEditor;
        SubTotal: Serenity.StringEditor;
        SubTotaltax: Serenity.StringEditor;
        ItemTotal: Serenity.StringEditor;
        ItemTotaltax: Serenity.StringEditor;
        TaxRateCode: Serenity.StringEditor;
        TaxRateId: Serenity.StringEditor;
        TaxLabel: Serenity.StringEditor;
        TaxCompound: Serenity.StringEditor;
        TaxTotal: Serenity.StringEditor;
        TaxShippingTotal: Serenity.StringEditor;
        TaxRatePercent: Serenity.StringEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class WcOrderDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WcOrderDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WcOrderDetailsForm.init)  {
                WcOrderDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.BooleanEditor;

                Q.initFormType(WcOrderDetailsForm, [
                    'Wcoid', w0,
                    'ParentId', w0,
                    'Status', w0,
                    'Currency', w0,
                    'IncludedTax', w0,
                    'DateCreated', w0,
                    'DateModified', w0,
                    'DiscountTotal', w0,
                    'DiscountTax', w0,
                    'ShippingTotal', w0,
                    'ShipppingTax', w0,
                    'CartTax', w0,
                    'Total', w0,
                    'TotalTax', w0,
                    'CustomerId', w0,
                    'OrderKey', w0,
                    'BFirstName', w0,
                    'BLastName', w0,
                    'BCompany', w0,
                    'BEmail', w0,
                    'BPhone', w0,
                    'BPostCode', w0,
                    'BAddress1', w0,
                    'BAddress2', w0,
                    'BCity', w0,
                    'BState', w0,
                    'BCountry', w0,
                    'SFirstName', w0,
                    'SLastName', w0,
                    'SCompany', w0,
                    'SPhone', w0,
                    'SPostCode', w0,
                    'SAddress1', w0,
                    'SAddress2', w0,
                    'SCity', w0,
                    'SState', w0,
                    'SCountry', w0,
                    'PaymentMethod', w0,
                    'TransactionId', w0,
                    'CustomerIp', w0,
                    'ItemId', w0,
                    'ItemName', w0,
                    'ProductId', w0,
                    'VariationId', w0,
                    'Quantity', w0,
                    'TaxClass', w0,
                    'SubTotal', w0,
                    'SubTotaltax', w0,
                    'ItemTotal', w0,
                    'ItemTotaltax', w0,
                    'TaxRateCode', w0,
                    'TaxRateId', w0,
                    'TaxLabel', w0,
                    'TaxCompound', w0,
                    'TaxTotal', w0,
                    'TaxShippingTotal', w0,
                    'TaxRatePercent', w0,
                    'IsMoved', w1
                ]);
            }
        }
    }
}
