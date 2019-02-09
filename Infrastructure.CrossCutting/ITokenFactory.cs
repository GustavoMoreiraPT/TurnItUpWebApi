using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrossCutting
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
