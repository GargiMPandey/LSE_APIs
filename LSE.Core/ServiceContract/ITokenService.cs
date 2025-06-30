﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSE.Core.ServiceContract
{

    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, string name);
    }
}
