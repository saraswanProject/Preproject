namespace Preproject.Model
{
    public class productModel
    {
        public string? operatorId { get; set; }
        public string? countryCode { get; set; }
        public string? categoryId { get; set; }

    }
    public class productResponseModel
    {
        public string? requireFetchBundle { get; set; }
        public string? description { get; set; }
        public string? productId { get; set; }
        public string? category { get; set; }
        public string? productName { get; set; }
        public string? countryCode { get; set; }
        public string? operatorId { get; set; }
        public string? operatorName { get; set; }
        public string? imageUrl { get; set; }
    }

    public class ProductResponse
    {
        public List<productResponseModel> payLoad { get; set; }
    }
}
