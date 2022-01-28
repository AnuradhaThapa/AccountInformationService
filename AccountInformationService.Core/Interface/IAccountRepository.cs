using AccountInformationService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationService.Core.Interface
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Get Client account details by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ClientAccountDetailsEntity>> GetClientAccountByUserId(string userId);
    }
}
