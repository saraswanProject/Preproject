using Dapper;
using Microsoft.Extensions.Configuration;

namespace Preproject.Helpers
{
    public class MaintainenceService : IMaintainenceService
    {
        private readonly IDbHelperService _dbHelperService;

        public MaintainenceService(IDbHelperService dbHelperService)
        {
            _dbHelperService = dbHelperService;
        }
        public string AddAPILog(string flag, string partnerId, string partnerTranId, string reqxml, string userId, string methodName, string logSno)
        {

            string result = string.Empty;
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", flag);
            param.Add("@sno", logSno);
            if (flag == "i")
            {
                param.Add("@reqxml", reqxml);
            }
            else
            {
                param.Add("@resxml", reqxml);
            }
            param.Add("@partnerTranId", partnerTranId);
            param.Add("@tranId", partnerTranId);
            param.Add("@partnerId", partnerId);
            param.Add("@userId", userId);
            param.Add("@funcName", methodName);
            dynamic objDynamic = _dbHelperService.ExecuteQueryDynamic("spa_soapLog", param, true);

            if (objDynamic != null && flag == "i")
            {
                result = objDynamic.Result[0].sno;
            }

            return result;

        }
    }
}
