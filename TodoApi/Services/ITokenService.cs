using System;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITokenService
    {
        Task<OktaResponse> GetToken(string username, string password);
    }
}

