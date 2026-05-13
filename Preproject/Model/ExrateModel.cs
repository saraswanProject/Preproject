using System.ComponentModel.DataAnnotations;

namespace Preproject.Model
{

        public class exchangeRateModel
        {
            [Required]
            public string? skuId { get; set; }
        }

        public class rateResponseModel
        {
            public string? skuId { get; set; }
            public string? productName { get; set; }
            public string? currencyCode { get; set; }
            public string? exchangeRate { get; set; }
        }

        public class rateResponse
        {
        public bool process_result { get; set; }
        public rateResponseModel? payLoad { get; set; }
    }
}

