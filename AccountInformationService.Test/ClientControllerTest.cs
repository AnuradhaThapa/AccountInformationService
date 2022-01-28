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
    public class ClientControllerTest
    {
        private readonly Mock<IClientRepository> service;
        public ClientControllerTest()
        {
            service = new Mock<IClientRepository>();
        }

        [Fact]
        public void GetClientByUserId_Client_ClientExistInDB()
        {
            //arrange
            var user = GetSampleUserData();
            var client = GetSampleClientData();
            service.Setup(x => x.GetClientByUserId("User1")).Returns(client);
            var controller = new ClientController(service.Object);

            //act
            var actionResult = controller.GetClientDetails(user);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as ClientDetailsEntity;

            //assert
            Assert.IsType<OkObjectResult>(result);
            result.Value.Equals(client);
        }

        [Fact]
        public void GetClientByUserId_Client_ClientDoesNotExistInDB()
        {
            //arrange
            var user = GetNegativeSampleUserData();
            var client = GetSampleClientData();
            service.Setup(x => x.GetClientByUserId("User1")).Returns(client);
            var controller = new ClientController(service.Object);

            //act
            var actionResult = controller.GetClientDetails(user);

            //assert
            var result = actionResult.Result;
            Assert.IsType<UnauthorizedResult>(result);
        }
        private async Task<List<ClientDetailsEntity>> GetSampleClientData()
        {
            List<ClientDetailsEntity> clientDetailsEntity = new List<ClientDetailsEntity>()
            {
                new ClientDetailsEntity()
                {
                ClientID = "Client1",
                FirstName = "FirstName1",
                LastName = "LastName1",
                Address = "Address1",
                Email = "xyz@123.com",
                Phone = "1234567891",
                ClientType = "Personal",
                UserId = "User1"
                }
            };
            return await Task.FromResult<List<ClientDetailsEntity>>(clientDetailsEntity);
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
