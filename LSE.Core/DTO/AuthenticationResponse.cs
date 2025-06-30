using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSE.Core.DTO
{
    public class AuthenticationResponse
    {

        public Guid? UserId { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Token { get; set; }
        public bool Success { get; set; }
    }
}
