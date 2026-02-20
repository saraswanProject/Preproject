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

    public class skuModel
    {
        public string productId { get; set; }
        public string skuId { get; set; }
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

    }
   
}
