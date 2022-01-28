using AccountInformationService.Core.Entities;
using AccountInformationService.Core.Interface;
using AccountInformationService.Infrastructure.Data;
using AccountInformationService.Infrastructure.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationService.Infrastructure.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly AMDBContext context;
        private readonly IMapper imapper;
        public AccountRepository(AMDBContext context, IMapper mapper)
        {
            this.context = context;
            imapper = mapper;
        }
        public async Task<List<ClientAccountDetailsEntity>> GetClientAccountByUserId(string userId)
        {
            List<ClientAccountDetailsEntity> clientAccountDetailsEntity = null;
            List<ClientAccountDetail> clientAccountDetail = await context.ClientAccountDetails.FromSql<ClientAccountDetail>($"select * from dbo.udfClientAccDetailsByUserId({userId}) as ClientAccountDetails").ToListAsync();
            if (clientAccountDetail != null)
            {
                clientAccountDetailsEntity = imapper.Map<List<ClientAccountDetailsEntity>>(clientAccountDetail);
            }
            return clientAccountDetailsEntity;
        }
    }
}
