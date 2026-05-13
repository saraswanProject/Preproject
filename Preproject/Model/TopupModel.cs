using System.ComponentModel.DataAnnotations;

namespace Preproject.Model
{
    public class mobileTopupModel
    {
        [Required]
        public string? skuId { get; set; }

        [Required]
        public string? amount { get; set; }

        [Required]
        public string? mobile { get; set; }

        public string? correlationId { get; set; }
        public string? senderMobile { get; set; }
        public string? boostPin { get; set; }
        public string? numberOfPlanMonths { get; set; }
        public string? transactionCurrencyCode { get; set; }

        public List<AdditionalInfo>? AdditionalInfos { get; set; }
    }

    public class TopupResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public PayLoad? PayLoad { get; set; }
    }

    public class PayLoad
    {
        public long TransactionId { get; set; }
        public string? TransactionDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal FaceValue { get; set; }
        public decimal Discount { get; set; }
        public decimal Fee { get; set; }

        public ProductResponseModel? Product { get; set; }
        public TopupDetailModel? TopupDetail { get; set; }

        public object? Pins { get; set; }
        public object? GiftCardDetail { get; set; }
        public object? SimInfo { get; set; }
        public object? BillPaymentDetail { get; set; }
        public object? EsimDetail { get; set; }
    }

    public class ProductResponseModel
    {
        public int SkuId { get; set; }
        public string? ProductName { get; set; }
        public decimal FaceValue { get; set; }
        public string? Instructions { get; set; }
        public string? ProductDescription { get; set; }
    }
    public class TopupDetailModel
    {
        public decimal LocalCurrencyAmount { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public decimal LocalCurrencyAmountExcludingTax { get; set; }
        public string? DestinationCurrency { get; set; }
        public string? OperatorTransactionId { get; set; }
    }


}
