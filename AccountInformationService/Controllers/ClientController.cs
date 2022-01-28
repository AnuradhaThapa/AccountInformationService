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
    /// Client controller
    /// </summary>
    [Route("api/accountInfo/v1")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository iclientRepository;
        /// <summary>
        /// Client Controller
        /// </summary>
        /// <param name="clientRepository"></param>
        public ClientController(IClientRepository clientRepository)
        {
            iclientRepository = clientRepository;
        }

        /// <summary>
        /// Get Client details by User Id if authorized
        /// </summary>
        /// <param name="base64EncodedString"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("clients")]
        public async Task<IActionResult> GetClientDetails([FromHeader(Name="X-CustomHeader")] string base64EncodedString)
        {
            List<ClientDetailsEntity> clientDetailsEntity = null;
            byte[] byteArray = Convert.FromBase64String(base64EncodedString);
            string json = Encoding.UTF8.GetString(byteArray);

            UserDetailEntity userDetailEntity = JsonConvert.DeserializeObject<UserDetailEntity>(json);

            if (userDetailEntity != null)
            {
                clientDetailsEntity = await iclientRepository.GetClientByUserId(userDetailEntity.AplId);
                if (clientDetailsEntity == null)
                {
                    return Unauthorized();
                }
            }
            else
                return Unauthorized();

            return Ok(clientDetailsEntity);
        }
    }
}
