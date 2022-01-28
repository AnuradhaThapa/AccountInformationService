using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountInformationService.Infrastructure.Model
{
    public class ClientDetail
    {
        [Key]
        public int Id { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ClientType { get; set; }
        public string UserId { get; set; }
    }
}
