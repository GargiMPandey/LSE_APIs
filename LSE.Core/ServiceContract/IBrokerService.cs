using LSE.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSE.Core.ServiceContract
{
    public interface IBrokerService
    {
        Task<AuthenticationResponse> Login(LoginRequest loginRequest);
        Task<AuthenticationResponse> Register(RegisterRequest registerRequest);
    }
}
