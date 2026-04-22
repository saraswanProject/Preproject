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

    public class mobiletopupResponse
    {
        public dynamic? payload { get; set; }
        //public List<mobileTopupModel> payLoad { get; set; }

    }







    public class Product
    {
        public string? skuId { get; set; }
        public string? productName { get; set; }
        public string? faceValue { get; set; }
        public string? instructions { get; set; }
        public string? productDescription { get; set; }
    }

    public class TopupDetail
    {
        public string? localCurrencyAmount { get; set; }
        public string? salesTaxAmount { get; set; }
        public string? localCurrencyAmountExcludingTax { get; set; }
        public string? destinationCurrency { get; set; }
        public string? operatorTransactionId { get; set; }
    }

}
