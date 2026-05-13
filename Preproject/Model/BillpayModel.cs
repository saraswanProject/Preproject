namespace Preproject.Model
{
    public class billPayment
    {
        public string? accountNumber { get; set; }
        public string? amount { get; set; }
        public string? correlationId { get; set; }
        public string? skuId { get; set; }
        public string? mobileNumber { get; set; }
        public string? correlacheckDigitstionId { get; set; }
        public string? senderMobile { get; set; }
        public string? senderName { get; set; }
        public string? transactionCurrencyCode { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
    }

    public class BillPay
    {
        public BillpayResponse?billpay{ get; set; }
    }


 
    public class BillpayResponse
    {
        public long TransactionId { get; set; }
        public string TransactionDate { get; set; }

        public decimal InvoiceAmount { get; set; }
        public decimal FaceValue { get; set; }
        public decimal Discount { get; set; }
        public decimal Fee { get; set; }

        public billProductModel Product { get; set; }
        public billAmountDetailModel billAmountDetailModel { get; set; }

        public object Pins { get; set; }
        public object GiftCardDetail { get; set; }
        public object SimInfo { get; set; }

        public billPaymentDetailModel billPaymentDetail { get; set; }
        public object EsimDetail { get; set; }
    }

    public class billProductModel
    {
        public int SkuId { get; set; }
        public string ProductName { get; set; }
        public decimal FaceValue { get; set; }
        public string Instructions { get; set; }
        public string ProductDescription { get; set; }
    }

    public class billAmountDetailModel
    {
        public decimal LocalCurrencyAmount { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public decimal LocalCurrencyAmountExcludingTax { get; set; }
        public string DestinationCurrency { get; set; }
        public string OperatorTransactionId { get; set; }
    }

    public class billPaymentDetailModel
    {
        public string AccountNumber { get; set; }
        public string Token { get; set; }
        public string Unit { get; set; }
        public string ReceivedNarration { get; set; }
    }

}
