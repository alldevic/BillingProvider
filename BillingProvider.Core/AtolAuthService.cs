using System;
using BillingProvider.Core.KKMDrivers;
using RestSharp;

namespace BillingProvider.Core
{
    public class TokenStruct
    {
        public DateTime Expired { get; }

        public string Token { get; }

        public TokenStruct(DateTime expired, string token)
        {
            Expired = expired;
            Token = token;
        }
    }

    public static class AtolAuthService
    {
        public static TokenStruct GetToken(string host, string login, string password)
        {
            var client = new RestClient(host);
            var request = new RestRequest("getToken", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(new {login = login, pass = password});

            var res = client.Execute<AtolOnlineDriver.AuthResponse>(request);
            // _token = res.Data.Token;
            if (string.IsNullOrEmpty(res.ErrorMessage))
            {
                var expired = DateTime.Parse(res.Data.Timestamp) + TimeSpan.FromHours(24);
                return new TokenStruct(expired, res.Data.Token);
            }

            return new TokenStruct(DateTime.Now, string.Empty);
        }
    }
}