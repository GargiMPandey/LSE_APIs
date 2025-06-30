using LSE.Core.DTO;
using LSE.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using LSE.Core.RepositoryContracts;
using LSE.Core.Entities;

namespace LSE.Core.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IBrokerRepository _brokerRepository;
        private readonly ITokenService _tokenService;
        public BrokerService(IBrokerRepository brokerRepository, ITokenService tokenService)
        {
            _brokerRepository = brokerRepository;
            _tokenService = tokenService;
        }
        public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
        {
            Broker? broker = await _brokerRepository.GetBrokerByEmailId(loginRequest.EmailId, loginRequest.Password);
            if (broker == null)
            {
                return null;
            }
            var token = _tokenService.GenerateToken(broker.Id, broker.EmailId, broker.Name);
            var authenticationResponse = new AuthenticationResponse
            {
                UserId = broker.Id,
                Name = broker.Name,
                EmailId = broker.EmailId,
                Success = true,
                Token = token 
            };

            return authenticationResponse;
        }

        public async Task<AuthenticationResponse> Register(RegisterRequest registerRequest)
        {
            Broker? broker = new Broker
            {
                Id = Guid.NewGuid(),
                Name = registerRequest.Name,
                EmailId = registerRequest.EmailId,
                Password = registerRequest.Password // In a real application, ensure to hash the password
            };

            Broker? registeredBroker = await _brokerRepository.AddBroker(broker);
            if (registeredBroker == null)
            {
                return null;
            }
           var token = _tokenService.GenerateToken(registeredBroker.Id, registeredBroker.EmailId, registeredBroker.Name);
            return new AuthenticationResponse
            {
                UserId = registeredBroker.Id,
                Name = registeredBroker.Name,
                EmailId = registeredBroker.EmailId,
                Success = true,
                Token = token 
            };

        }
    }
}
