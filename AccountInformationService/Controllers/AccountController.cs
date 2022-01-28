using AccountInformationService.Core.Entities;
using AccountInformationService.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationService.API.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/accountInfo/v1")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository iaccountRepository;
        /// <summary>
        /// Account Controller
        /// </summary>
        /// <param name="accountRepository"></param>
        public AccountController(IAccountRepository accountRepository)
        {
            iaccountRepository = accountRepository;
        }

        /// <summary>
        /// Get Client account details by User Id if authorized
        /// </summary>
        /// <param name="base64EncodedString"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("accounts")]
        public async Task<IActionResult> GetClientAccountDetails([FromHeader(Name = "X-CustomHeader")] string base64EncodedString)
        {
            List<ClientAccountDetailsEntity> clientAccountDetailsEntity = null;
            byte[] byteArray = Convert.FromBase64String(base64EncodedString);
            string json = Encoding.UTF8.GetString(byteArray);

            UserDetailEntity userDetailEntity = JsonConvert.DeserializeObject<UserDetailEntity>(json);
            
            if (userDetailEntity != null)
            {
                clientAccountDetailsEntity = await iaccountRepository.GetClientAccountByUserId(userDetailEntity.AplId);
                if (clientAccountDetailsEntity == null)
                {
                    return Unauthorized();
                }
            }
            else
                return Unauthorized();

            return Ok(clientAccountDetailsEntity);
        }
    }
}
