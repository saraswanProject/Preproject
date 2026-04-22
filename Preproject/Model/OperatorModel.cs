namespace Preproject.Model
{
    public class operatorModel
    {
        public string? operatorId { get; set; }
        public string? countryCode { get; set; }

    }
    public class operatorResponseModel
    {
        public string? operatorId { get; set; }
        public string? operatorName { get; set; }
        public string? imageurl { get; set; }

    }

    public class operatorResponse
    {
        public List<operatorResponseModel> payLoad { get; set; }

    }
}
