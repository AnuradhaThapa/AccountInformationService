using AccountInformationService.API.Controllers;
using AccountInformationService.Core.Entities;
using AccountInformationService.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountInformationService.Test
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountRepository> service;
        public AccountControllerTest()
        {
            service = new Mock<IAccountRepository>();
        }

        [Fact]
        public void GetAccountByUserId_Account_AccountExistInDB()
        {
            //arrange
            var user = GetSampleUserData();
            var client = GetSampleClientAccountData();
            service.Setup(x => x.GetClientAccountByUserId("User1")).Returns(client);
            var controller = new AccountController(service.Object);

            //act
            var actionResult = controller.GetClientAccountDetails(user);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as ClientAccountDetailsEntity;

            //assert
            Assert.IsType<OkObjectResult>(result);
            result.Value.Equals(client);
        }

        [Fact]
        public void GetAccountByUserId_Account_AccountDoesNotExistInDB()
        {
            //arrange
            var user = GetNegativeSampleUserData();
            var client = GetSampleClientAccountData();
            service.Setup(x => x.GetClientAccountByUserId("User1")).Returns(client);
            var controller = new AccountController(service.Object);

            //act
            var actionResult = controller.GetClientAccountDetails(user);

            //assert
            var result = actionResult.Result;
            Assert.IsType<UnauthorizedResult>(result);
        }
        private async Task<List<ClientAccountDetailsEntity>> GetSampleClientAccountData()
        {
            List<ClientAccountDetailsEntity> clientAccountDetailsEntities = new List<ClientAccountDetailsEntity>()
             {
                new ClientAccountDetailsEntity()
                {
                    AccountId = "A1345",
         CustodianId ="AssetMark Trust",
         CustodianName = "GNW",
         RegisteredName ="Anu Thapa IRA",
         ClientId ="C35811",
         UserId ="User1",
         CustodialAccountNumber="12345678",
         MarketValue ="12345.00",
         ProgramId ="MF",
         ProgramName ="Mutual Funds",
         IsClosed ="1"
                }
            };
            return await Task.FromResult<List<ClientAccountDetailsEntity>>(clientAccountDetailsEntities);
        }

        private string GetSampleUserData()
        {
            UserDetailEntity userDetailEntity = new UserDetailEntity()
            {
                UserName = "Anu",
                UserGuid = new Guid("04A95C41-E98C-1910-E9B0-E7F9F2B609CD"),
                RoleName = "Agent",
                RoleId = 1,
                AplId = "User1",
                HasActiveRole = true
            };
            string userDetailsJson = JsonConvert.SerializeObject(userDetailEntity);
            var base64EncodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes(userDetailsJson));
            return base64EncodedString;
        }

        private string GetNegativeSampleUserData()
        {
            UserDetailEntity userDetailEntity = new UserDetailEntity()
            {
                UserName = "Anu",
                UserGuid = new Guid("04A95C41-E98C-1910-E9B0-E7F9F2B609CE"),
                RoleName = "Agent",
                RoleId = 1,
                AplId = "User2",
                HasActiveRole = true
            };
            string userDetailsJson = JsonConvert.SerializeObject(userDetailEntity);
            var base64EncodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes(userDetailsJson));
            return base64EncodedString;
        }
    }
}
