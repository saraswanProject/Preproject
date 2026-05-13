using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Preproject.Helpers;
using Preproject.Model;
using System;
using System.Net.Http;
using System.Text;
using TransactionRepository;
using static Preproject.Model.operatorResponse;

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
        [Route("balanceCheck")]
        public async Task<IActionResult> getbalance()
        {
            Result res = new Result();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://sandbox.valuetopup.com/api/v2/account/balance");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<balanceResponse>(jsonString);

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.process_result = false;
                res.result = ex.Message;
                return BadRequest(res);
            }

        }



        [HttpGet]
        [Route("countryList")]
        public async Task<IActionResult> getCountryList()
        {
            Result res = new Result();

            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/countries";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                // Optional: strongly typed model if you have one
                var data = JsonConvert.DeserializeObject<countryResponse>(jsonString);

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.process_result = false;
                res.result = ex.Message;
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("errorList")]
        public async Task<IActionResult> getErrorList()
        {
            Result res = new Result();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://sandbox.valuetopup.com/api/v2/catalog/errors");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                // Optional: replace object with a proper OperatorResponse model if available
                var data = JsonConvert.DeserializeObject<operatorResponse>(jsonString);

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.process_result = false;
                res.result = ex.Message;

                return BadRequest(res);
            }
        }



        [HttpPost]
        [Route("operatorlist")]
        public async Task<IActionResult> getoperatorListt(operatorModel operatorModel)
        {
            Result res = new Result();

            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/operators";

                string requestUrl =
                    $"{url}?operatorId={operatorModel.operatorId}&countryCode={operatorModel.countryCode}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                // Optional: replace object with a proper OperatorResponse model if available
                var data = JsonConvert.DeserializeObject<operatorResponse>(jsonString);

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.process_result = false;
                res.result = ex.Message;

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("catwisecountry")]
        public async Task<IActionResult> getcountrybycatgList(productModel catbycountryModel)
        {
            Result res = new Result();
            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/getproducts";
                string requestUrl = $"{url}?categoryId={catbycountryModel.categoryId}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

                var distinctCountryCodes = apiResponse.payLoad
                                            .Select(x => x.countryCode)
                                            .Distinct()
                                            .ToList();

                // Second API call for countries
                var clientcountry = new HttpClient();
                var requestcountry = new HttpRequestMessage(HttpMethod.Get, "https://sandbox.valuetopup.com/api/v2/catalog/countries");
                requestcountry.Headers.Add("Accept", "application/json");
                requestcountry.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var responsecountry = await clientcountry.SendAsync(requestcountry);
                responsecountry.EnsureSuccessStatusCode();

                var countryJson = await responsecountry.Content.ReadAsStringAsync();
                var countryResponse = JsonConvert.DeserializeObject<CountryApiResponse>(countryJson);

                var matchedCountries = countryResponse.payLoad
                                        .Where(c => distinctCountryCodes.Contains(c.countryCode))
                                        .ToList();

                // ✅ Match your pattern
                res.process_result = true;
                res.result = matchedCountries;

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("productlist")]
        public async Task<IActionResult> getProductList(productModel productModel)
        {
            Result res = new Result();

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

                var jsonString = await response.Content.ReadAsStringAsync();

                // ✅ Deserialize properly
                var data = JsonConvert.DeserializeObject<ProductResponse>(jsonString);
                

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("giftId")]
        public async Task<IActionResult> getgiftId(giftidModel giftidModel)
        {
            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/skus/giftcards";

                string requestUrl = $"{url}?operatorId={giftidModel.productId}&countryCode={giftidModel.countryCode}&categoryId={giftidModel.skuId}";

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



        [HttpPost]
        [Route("skulist")]
        public async Task<IActionResult> getSkuList(skuModel skuModel)
        {
            Result res = new Result();

            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/skus";

                string requestUrl =
                    $"{url}?productId={skuModel.productId}&skuId={skuModel.skuId}" +
                    $"&countryCode={skuModel.countryCode}&categoryId={skuModel.categoryId}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                // Replace object with strongly typed model if available
                var data = JsonConvert.DeserializeObject<skuResponse>(jsonString);

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.process_result = false;
                res.result = ex.Message;

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("exchangerate")]
        public async Task<IActionResult> getexchangeRate(exchangeRateModel exchangeRateModel)
        {
            Result res = new Result();

            try
            {
                string url = "https://sandbox.valuetopup.com/api/v2/catalog/sku/exchangeRate";
                string requestUrl = $"{url}/{exchangeRateModel.skuId}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                // ✅ FIXED: GET request
                var response = await client.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<rateResponse>(jsonString);

                res.process_result = true;
                res.result = data.payLoad;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.process_result = false;
                res.result = ex.Message;

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("mobileTopup")]
        public async Task<IActionResult> getTopup([FromBody] mobileTopupModel mobileTopupModel)
        {
            Result res = new Result();

            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, "https://sandbox.valuetopup.com/api/v2/transaction/topup");

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var payload = new
                {
                    skuId = mobileTopupModel.skuId,
                    amount = mobileTopupModel.amount,
                    mobile = mobileTopupModel.mobile,
                    correlationId = mobileTopupModel.correlationId,
                    senderMobile = mobileTopupModel.senderMobile,
                    boostPin = mobileTopupModel.boostPin,
                    transactionCurrencyCode = mobileTopupModel.transactionCurrencyCode,
                    numberOfPlanMonths = mobileTopupModel.numberOfPlanMonths,
                    additionalInfos = mobileTopupModel.AdditionalInfos ?? new List<AdditionalInfo>()
                };

                string strJSON = JsonConvert.SerializeObject(payload);
                request.Content = new StringContent(strJSON, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TopupResponse>(jsonString);

            
                if(data.PayLoad is null)
                {
                    res.process_result = false;
                    res.result = data.ResponseMessage;
                }else
                {
                    res.process_result = true;
                    res.result = data.PayLoad;
                }
               
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("catagorylist")]
        public async Task<IActionResult> categoryList()
        {
            object categoryList = new object();
            object response = new object();
            try
            {
                categoryList = Enum.GetValues(typeof(CategoryType))
                     .Cast<CategoryType>()
                     .Select(e => new
                     {
                         Key = (int)e,
                         Value = e.ToString()
                     })
                     .ToList();
                response = new
                {
                    process_result = true,
                    catalogue_list = categoryList
                };
            }
            catch (Exception ex)
            {
                response = new
                {
                    process_result = false,
                    catalogue_list = categoryList
                };
            }


            return Ok(response);
        }


        [HttpPost]
        [Route("billPayment")]
        public async Task<IActionResult> billPay(billPayment billPayment)
        {
            Result res = new Result();
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Post, "https://sandbox.valuetopup.com/api/v2/transaction/billpay");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var billpay = new
                {
                    accountNumber = billPayment.accountNumber,
                    amount = billPayment.amount,
                    correlationId = billPayment.correlationId,
                    skuId = billPayment.skuId,
                    mobileNumber = billPayment.mobileNumber,
                    senderMobile = billPayment.senderMobile,
                    senderName = billPayment.senderName,
                    transactionCurrencyCode = billPayment.transactionCurrencyCode,
                    AdditionalInfo = billPayment.AdditionalInfos
                };

                string strJSON = JsonConvert.SerializeObject(billpay);
                var content = new StringContent(strJSON, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BillPay>(jsonString);

                res.process_result = true;
                res.result = data.billpay;

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("pinTransaction")]
        public async Task<IActionResult> pintxn(pinTransaction pinTransaction)
        {
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Post, "https://sandbox.valuetopup.com/api/v2/transaction/pin");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var payload = new
                {
                    correlationId = pinTransaction.correlationId,
                    skuId = pinTransaction.skuId,
                    recipient = pinTransaction.recipient,
                    AdditionalInfo = pinTransaction.AdditionalInfos
                };

                string strJSON = JsonConvert.SerializeObject(payload);
                var content = new StringContent(strJSON, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("giftcardtxn")]
        public async Task<IActionResult> gifttxn(giftcardtxnModel giftcardtxnModel)
        {
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Post, "https://sandbox.valuetopup.com/api/v2/transaction/giftcard/order");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var payload = new
                {
                    amount = giftcardtxnModel.amount,
                    correlationId = giftcardtxnModel.correlationId,
                    skuId = giftcardtxnModel.skuId,
                    firstName = giftcardtxnModel.firstName,
                    lastName = giftcardtxnModel.lastName,
                    recipient = giftcardtxnModel.recipient,
                    transactionCurrencyCode = giftcardtxnModel.transactionCurrencyCode,
                    AdditionalInfo = giftcardtxnModel.AdditionalInfos
                };

                string strJSON = JsonConvert.SerializeObject(payload);
                var content = new StringContent(strJSON, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }







        [HttpPost]
        [Route("esimTxn")]
        public async Task<IActionResult> esim(esimtxn esimtxn)
        {
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Post, "https://sandbox.valuetopup.com/api/v2/esim/order");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic aW5maWNhcGk6bCRIc0hsY0YyNA==");

                var payload = new
                {
                    correlationId = esimtxn.correlationId,
                    skuId = esimtxn.skuId,
                };

                string strJSON = JsonConvert.SerializeObject(payload);
                var content = new StringContent(strJSON, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }




    public enum CategoryType
    {
        Pin = 1,
        Rtr = 2,
        BillPay = 4,
        Sim = 5,
        GiftCard = 6,
        eSim = 7
    }

    //public class categorylist
    //{
    //    public string process_result { get; set; }

    //}

    public class Result
    {
        public bool process_result { get; set; } = false;
        public dynamic result { get; set; }
    }


}

