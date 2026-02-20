using Preproject.Helpers;
using Microsoft.AspNetCore.Mvc;
using TransactionRepository;

namespace Preproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreprojectController : ControllerBase
    {
        private readonly ITransactionRepository _trnRepo;
        private IConfiguration _configuration;
        private IDbHelperService _dbHelperService;
        private IMaintainenceService _maintainenceService;

        public PreprojectController(
          ITransactionRepository trnRepo,
          IConfiguration configuration,
          IDbHelperService dbHelperService,
          IMaintainenceService maintainenceService)
        {
            this._trnRepo = trnRepo;
            this._configuration = configuration;
            this._dbHelperService = dbHelperService;
            this._maintainenceService = maintainenceService;
        }



    }
}
