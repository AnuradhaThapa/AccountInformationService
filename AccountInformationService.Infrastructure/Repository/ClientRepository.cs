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
    public class ClientRepository: IClientRepository
    {
        private readonly AMDBContext context;
        private readonly IMapper imapper;
        public ClientRepository(AMDBContext context,IMapper mapper)
        {
            this.context = context;
            imapper = mapper;
        }
        public async Task<List<ClientDetailsEntity>> GetClientByUserId(string userId)
        {
            List<ClientDetailsEntity> clientDetailsEntity = null;
            List<ClientDetail> clientDetail = await context.ClientDetails.FromSql<ClientDetail>($"select * from dbo.udfClientDetailsByUserId({userId}) as ClientDetails").ToListAsync();
            if (clientDetail != null)
            {
                clientDetailsEntity = imapper.Map<List<ClientDetailsEntity>>(clientDetail);
            }
            return clientDetailsEntity;
        }
    }
}
