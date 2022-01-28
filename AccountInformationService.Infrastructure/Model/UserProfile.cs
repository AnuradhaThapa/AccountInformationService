using AccountInformationService.Core.Entities;
using AccountInformationService.Infrastructure.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.Infrastructure.Model
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ClientDetail, ClientDetailsEntity>();
            CreateMap<ClientDetailsEntity, ClientDetail>();

            CreateMap<ClientAccountDetail, ClientAccountDetailsEntity>();
            CreateMap<ClientAccountDetailsEntity, ClientAccountDetail>();
        }
    }
}
