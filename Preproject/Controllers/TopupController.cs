using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Preproject.Helpers;
using Preproject.Model;
using System;
using System.Net.Http;
using System.Text;
using TransactionRepository;

namespace Preproject.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TopupController : ControllerBase
    {
        private readonly ITransactionRepository _trnRepo;
        private IConfiguration _configuration;
        private IDbHelperService _dbHelperService;
        private IMaintainenceService _maintainenceService;

        public TopupController(
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

        [HttpGet]
        [Route("countryList")]
        public async Task<IActionResult> getCountryList()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://sandbox.valuetopup.com/api/v2/catalog/countries");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        [HttpGet]
        [Route("operatorlist")]
        public async Task<IActionResult> geOperatorListt(operatorModel operatorModel)
        {
            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/operators";

                string requestUrl = $"{url}?operatorId={operatorModel.operatorId}&countryCode={operatorModel.countryCode}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            
        }


        [HttpGet]
        [Route("productlist")]
        public async Task<IActionResult> getProductList(productModel productModel)
        {
            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/getproducts";

                string requestUrl = $"{url}?operatorId={productModel.operatorId}&countryCode={productModel.countryCode}&categoryId={productModel.categoryId}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        [HttpGet]
        [Route("skulist")]
        public async Task<IActionResult> getSkuList(skuModel skuModel)
        {
            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/skus";

                string requestUrl = $"{url}?productId={skuModel.productId}&skuId={skuModel.skuId}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://sandbox.valuetopup.com/api/v2/catalog/skus");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

         
        }

        [HttpPost]
        [Route("topup")]
        public async Task<IActionResult> getTopup(mobileTopupModel mobileTopupModel) 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sandbox.valuetopup.com/api/v2/transaction/topup");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

            var payload = new 
            {
                skuId=mobileTopupModel.skuId,
                amount=mobileTopupModel.amount,
                mobilw=mobileTopupModel.mobile,
                correlationId=mobileTopupModel.correlationId,
                senderMobile=mobileTopupModel.senderMobile,
                transactionCurrencyCode=mobileTopupModel.transactionCurrencyCode,
                numberOfPlanMonths=mobileTopupModel.numberOfPlanMonths,


            };
            string strJSON = JsonConvert.SerializeObject(payload);
            var content = new StringContent(strJSON, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }


    }
}