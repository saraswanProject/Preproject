namespace Preproject.Model
{
    public class skuModel
    {
        public string? productId { get; set; }
        public string? skuId { get; set; }
        public string? countryCode { get; set; }
        public string? categoryId { get; set; }
    }

    public class skuResponseModel
    {
        public string? skuId { get; set; }
        public string? skuName { get; set; }

        public skuresponseCommon? min { get; set; }
        public skuresponseCommon? max { get; set; }

        public string? exchangeRate { get; set; }
        public string? loanAmountInFaceValueCurrency { get; set; }
        public string? loanAmountInDeliveryCurrency { get; set; }
        public string? loanAmountInWalletCurrency { get; set; }
        public string? productId { get; set; }
        public string? productName { get; set; }
        public string? category { get; set; }
        public string? isSalesTaxCharged { get; set; }
        public string? salesTax { get; set; }
        public string? countryCode { get; set; }
        public string? benefitType { get; set; }
        public string? validity { get; set; }
        public string? productDescription { get; set; }
        public string? localPhoneNumberLength { get; set; }
        public object? internationalCountryCode { get; set; }
        public string? allowDecimal { get; set; }
        public string? fee { get; set; }
        public string? operatorId { get; set; }
        public string? operatorName { get; set; }
        public string? imageUrl { get; set; }
        public string? additionalInformation { get; set; }
        public string? region { get; set; }
        public string? deliveryAmountType { get; set; }
        public string? subCategory { get; set; }
        public string? requireFetchBundle { get; set; }
    }

    public class skuresponseCommon
    {
        public string? faceValue { get; set; }
        public string? faceValueCurrency { get; set; }
        public string? deliveredAmount { get; set; }
        public string? deliveryCurrencyCode { get; set; }
        public string? cost { get; set; }
        public string? costCurrency { get; set; }
        public string? faceValueInWalletCurrency { get; set; }
    }


    public class skuResponse
    {
        public List<skuResponseModel> payLoad { get; set; } = new List<skuResponseModel>();
    }

}