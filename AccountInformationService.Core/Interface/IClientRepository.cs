using AccountInformationService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationService.Core.Interface
{
    public interface IClientRepository
    {
        /// <summary>
        /// Get Client by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ClientDetailsEntity>> GetClientByUserId(string userId);
    }
}
