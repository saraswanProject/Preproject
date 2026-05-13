using System.ComponentModel.DataAnnotations;

namespace Preproject.Model
{

    public class catbycountryModel
    {
        public string? categoryId { get; set; }
    }

    public class giftidModel
    {
        public string? productId { get; set; }
        public string? skuId { get; set; }
        public string? countryCode { get; set; }
    }


    public class giftidResponse
    {
        public List<giftidModel> payLoad { get; set; }
    }

    public class giftcardtxnModel
    {
        public string? amount { get; set; }
        public string? correlationId { get; set; }
        public string? skuId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? recipient { get; set; }
        public string? transactionCurrencyCode { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
    }

    public class giftcardtxnResponse
    {
        public List<giftcardtxnModel> payLoad { get; set; }
    }

      public class pinTransaction
    {
        public string? correlationId { get; set; }
        public string? skuId { get; set; }
        public string? recipient { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
    }
    public class pintxnResponse
    {
       public List<pinTransaction> payLoad { get; set; }
    }

   
    public class esimtxn
    {
        public int correlationId { get; set; }
        public string? skuId { get; set; }
        //public List<AdditionalInfo> AdditionalInfos { get; set; }
    }
    public class esimtxnResponse
    {
        public List<esimtxn> payLoad { get; set; }
    }

    public class AdditionalInfo
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
      
    }

    public class ApiResponse
    {
      //  public string? responseCode { get; set; }
      //  public string? responseMessage { get; set; }
        public List<PayLoadModel> payLoad { get; set; }
    }

    public class PayLoadModel
    {
        public string? countryCode { get; set; }
        public string? category { get; set; }
    }



    public class CountryApiResponse
    {
       // public string? responseCode { get; set; }
       // public string? responseMessage { get; set; }
        public List<CountryModel> payLoad { get; set; }
    }

    public class CountryModel
    {
        public string? countryCode { get; set; }
        public string? countryName { get; set; }
        public string? region { get; set; }
        public string? internationalCountryCode { get; set; }
        public string? areaCodes { get; set; }
        public string? numberLength { get; set; }
    }
    public class countryResponseModel
    {
        public string? countryCode { get; set; }
        public string? countryName { get; set; }
        public string? region { get; set; }
        public string? internationalCountryCode { get; set; }
        public string? areaCodes { get; set; }
        public string? numberLength { get; set; }

    }
        public class countryResponse{
        public List<CountryModel> payLoad { get; set; }
    }


    public class balanceResponseModel
    {
        public string? balance { get; set; }
    }

    public class balanceResponse
    {
        public bool process_result { get; set; }
        public balanceResponseModel? payLoad { get; set; }
    }




}

