
namespace AdvanceCRM.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.CompanyDetails")]
    [BasedOnRow(typeof(CompanyDetailsRow), CheckNames = true)]
    public class CompanyDetailsForm
    {
        [Category("General")]
        [HalfWidth]
        public String Name { get; set; }
        [HalfWidth]
        public String Slogan { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String Address { get; set; }
        [HalfWidth, MaskedEditor(Mask = "9999999999")]
        public String Phone { get; set; }
        [HalfWidth]
        public String GSTIN { get; set; }
        [HalfWidth]
        public String PANNo { get; set; }
        [HalfWidth]
        public Int32 StateId { get; set; }
        [HalfWidth]
        public Masters.CountryMaster? Country { get; set; }
        [HalfWidth]
        public Boolean? PassportDetails { get; set; }
        [HalfWidth]
        public Boolean? HSN { get; set; }
        [HalfWidth]
        public Boolean? Code { get; set; }
        [HalfWidth]
        public Boolean? Unit { get; set; }
        [HalfWidth]
        public Boolean? OpeningStock { get; set; }
        [HalfWidth]
        public Boolean? RawMaterial { get; set; }
        [HalfWidth]
        public Boolean? Group { get; set; }
        [HalfWidth]
        public Boolean? ToInvoice { get; set; }
        [HalfWidth]
        public Boolean? ToPerforma { get; set; }
        [HalfWidth]
        public Boolean? Capacity { get; set; }
        [HalfWidth]
        public Boolean? MRP { get; set; }
        [HalfWidth]
        public Boolean? SellingPrice { get; set; }
        public Boolean? Travels { get; set; }

        [Category("Logo")]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String Logo { get; set; }
        [HalfWidth]
        public Int32 LogoHeight { get; set; }
        [HalfWidth]
        public Int32 LogoWidth { get; set; }
        [MultipleImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String AdditionalImages { get; set; }
        [Category("Printing Details")]
        [HalfWidth]
  [BooleanSwitchEditor]
        public Boolean CompanyDetails { get; set; }
        [HalfWidth,DefaultValue(1)]
        public Masters.YearInPrefix? YearInPrefix { get; set; }
                
        [Category("Enquiry")]
        [HalfWidth]
        public String EnquirySuffix { get; set; }
        [HalfWidth]
        public String EnquiryPrefix { get; set; }
          [HalfWidth]
        
        public Int32 EnqStartNo { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean EnqEditNo { get; set; }

        [Category("Quotation")]
        [HalfWidth]
        public Masters.PrintTemplates? QuotationTemplate { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean QuotationTaxColumnIncluded { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean QuotationDiscountedPriceIncluded { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean QuotationContactIncluded { get; set; }
        [HalfWidth]
        public String QuotationSuffix { get; set; }
        [HalfWidth]
        public String QuotationPrefix { get; set; }
        [HalfWidth]
        public Int32 QuoStartNo { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean QuoEditNo { get; set; }
        [HtmlContentEditor,DisplayName("Quotation Header Content"),FullWidth]
        public String HeaderContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Quotation Header Image")]
        public String HeaderImage { get; set; }
        [HalfWidth(UntilNext = true)]
        [DisplayName("Quotation Header Height")]
        public Int32 HeaderHeight { get; set; }
        [DisplayName("Quotation Header Width")]
        public Int32 HeaderWidth { get; set; }
        [FullWidth]
        [HtmlContentEditor, DisplayName("Quotation Footer Content")]
        public String FooterContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Quotation Footer Image")]
        public String FooterImage { get; set; }
        [HalfWidth(UntilNext = true), DisplayName("Quotation Footer Height")]
        public Int32 FooterHeight { get; set; }
        [DisplayName("Quotation Footer Width")]
        public Int32 FooterWidth { get; set; }
        [Category("Invoice")]
        public Masters.PrintTemplates? InvoiceTemplate { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean InvoiceTaxColumnIncluded { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean InvoiceDiscountedPriceIncluded { get; set; }
        [HalfWidth]
        public String InvoiceSuffix { get; set; }
        [HalfWidth]
        public String InvoicePrefix { get; set; }
        [HalfWidth]
        public Int32 InvStartNo { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean InvEditNo { get; set; }

        [HtmlContentEditor,DisplayName("Invoice Header Content"),FullWidth]
        public String InvoiceHeaderContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Invoice Header Image"),FullWidth]
        public String InvoiceHeaderImage { get; set; }
        [HalfWidth(UntilNext = true), DisplayName("Invoice Header Height")]
        public Int32 InvoiceHeaderHeight { get; set; }
        [DisplayName("Invoice Header Width"),HalfWidth]
        public Int32 InvoiceHeaderWidth { get; set; }
        [FullWidth]
        [HtmlContentEditor, DisplayName("Invoice Footer Content")]
        public String InvoiceFooterContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Invoice Footer Image")]
        public String InvoiceFooterImage { get; set; }
        [HalfWidth(UntilNext = true), DisplayName("Invoice Footer Height")]
        public Int32 InvoiceFooterHeight { get; set; }
        [DisplayName("Invoice Footer Width")]
        public Int32 InvoiceFooterWidth { get; set; }

        [Category("CMS")]
        public String CmsSuffix { get; set; }
        public String CmSprefix { get; set; }
        [HalfWidth]
        public Int32 CmsStartNo { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean CmsEditNo { get; set; }

        [Category("DC")]
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean ChallanTaxColumnIncluded { get; set; }
        [HalfWidth]
        public String ChallanSuffix { get; set; }
        [HalfWidth]
        public String ChallanPrefix { get; set; }
        [HalfWidth]
        public Int32 DcStartNo { get; set; }
        [HalfWidth]
        [BooleanSwitchEditor]
        public Boolean DcEditNo { get; set; }

        [HtmlContentEditor,DisplayName("Challan Header Content"),FullWidth]
        public String DcHeaderContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Challan Header Image"),FullWidth]
        public String DcHeaderImage { get; set; }
        [HalfWidth(UntilNext = true), DisplayName("Challan Header Height")]
        public Int32 DcHeaderHeight { get; set; }
        [DisplayName("Challan Header Width")]
        public Int32 DcHeaderWidth { get; set; }
        [FullWidth]
        [HtmlContentEditor, DisplayName("Challan Footer Content")]
        public String DcFooterContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Challan Footer Image")]
        public String DcFooterImage { get; set; }
        [HalfWidth(UntilNext = true)]
        [DisplayName("Challan Footer Height")]
        public Int32 DcFooterHeight { get; set; }
        [DisplayName("Challan Footer Width")]
        public Int32 DcFooterWidth { get; set; }

        [Category("Itinerary")]
       // public Masters.PrintTemplates? ItineraryTemplate { get; set; }
        //[HalfWidth]
        //public Boolean InvoiceTaxColumnIncluded { get; set; }
        //[HalfWidth]
        //public Boolean InvoiceDiscountedPriceIncluded { get; set; }
        //[HalfWidth]
        //public String ItinerarySuffix { get; set; }
        //[HalfWidth]
        //public String ItineraryPrefix { get; set; }
        //[HalfWidth]
        //public Int32 ItineraryStartNo { get; set; }
        //[HalfWidth]
        //[BooleanSwitchEditor]
        //public Boolean ItineraryEditNo { get; set; }

        //[HtmlContentEditor, DisplayName("Itinerary Header Content"), FullWidth]
        //public String ItineraryHeaderContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Itinerary Header Image"), FullWidth]
        public String ItineraryHeaderImage { get; set; }
        [HalfWidth(UntilNext = true), DisplayName("Itinerary Header Height")]
        public Int32 ItineraryHeaderHeight { get; set; }
        [DisplayName("Itinerary Header Width"), HalfWidth]
        public Int32 ItineraryHeaderWidth { get; set; }
        //[FullWidth]
        //[HtmlContentEditor, DisplayName("Itinerary Footer Content")]
        //public String ItineraryFooterContent { get; set; }
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true), DisplayName("Itinerary Footer Image")]
        public String ItineraryFooterImage { get; set; }
        [HalfWidth(UntilNext = true), DisplayName("Itinerary Footer Height")]
        public Int32 ItineraryFooterHeight { get; set; }
        [DisplayName("Itinerary Footer Width")]
        public Int32 ItineraryFooterWidth { get; set; }


       // public Int32 ItineraryTempleate { get; set; }

    }
}