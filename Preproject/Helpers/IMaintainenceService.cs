namespace Preproject.Helpers
{
    public interface IMaintainenceService
    {
        public string AddAPILog(string flag, string partnerId, string partnerTranId, string reqxml, string userId, string methodName, string logSno);

    }
}
