using System.ComponentModel.DataAnnotations;

namespace Preproject.Model
{
    public class operatorModel
    {
        public string operatorId { get; set; }
        public string countryCode { get; set; }

    }



    public class productModel
    {
        public string operatorId { get; set; }
        public string countryCode { get; set; }
        public string categoryId { get; set; }

    }

    public class giftidModel
    {
        public string productId { get; set; }
        public string skuId { get; set; }
        public string countryCode { get; set; }
    }

    public class giftcardtxnModel
    {
        public string amount { get; set; }
        public string correlationId { get; set; }
        public string skuId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string recipient { get; set; }
        public string transactionCurrencyCode { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
    }


    public class skuModel
    {
        public string productId { get; set; }
        public string skuId { get; set; }
        public string countryCode { get; set; }
        public string categoryId { get; set; }
    }

    public class mobileTopupModel
    {
        [Required]
        public string skuId { get; set; }
        [Required]
        public string amount { get; set; }
        [Required]
        public string mobile { get; set; }
        public string correlationId { get; set; }
        public string senderMobile { get; set; }
        public string boostPin { get; set; }
        public string numberOfPlanMonths { get; set; }
        public string transactionCurrencyCode { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }

    }

    public class pinTransaction
    {
        public string correlationId { get; set; }
        public string skuId { get; set; }
        public string recipient { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
    }

    public class billPayment
    {
        public string accountNumber { get; set; }
        public string amount { get; set; }
        public string correlationId { get; set; }
        public string skuId { get; set; }
        public string mobileNumber { get; set; }
        public string correlacheckDigitstionId { get; set; }
        public string senderMobile { get; set; }
        public string senderName { get; set; }
        public string transactionCurrencyCode { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
    }



    public class esimtxn
    {
        public int correlationId { get; set; }
        public string skuId { get; set; }
        //public List<AdditionalInfo> AdditionalInfos { get; set; }
    }

    public class AdditionalInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
      
    }

    public class ApiResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<PayLoadModel> payLoad { get; set; }
    }

    public class PayLoadModel
    {
        public string countryCode { get; set; }
        public string category { get; set; }
    }



    public class CountryApiResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<CountryModel> payLoad { get; set; }
    }

    public class CountryModel
    {
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string region { get; set; }
        public string internationalCountryCode { get; set; }
        public string areaCodes { get; set; }
        public string numberLength { get; set; }
    }
}

