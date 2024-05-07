using System;
using System.Collections.Generic;
using System.Text;

namespace MotorBikeRetals.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
