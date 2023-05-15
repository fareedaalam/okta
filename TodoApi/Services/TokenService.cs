using System;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TokenService:ITokenService
    {
        private readonly IOptions<OktaTokenSettings> _options;
        public TokenService(IOptions<OktaTokenSettings> options)
        {
            _options = options;
        }

        public IOptions<OktaTokenSettings> Options { get; }

        public async Task<OktaResponse> GetToken(string username, string password)
        {
            var token = new OktaResponse();
            var client = new HttpClient();
            var client_id = _options.Value.ClientId;
            var client_secret = _options.Value.ClientSecret;
            var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{client_id}:{client_secret}");

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var postMessage = new Dictionary<string, string>();
            postMessage.Add("grant_type", "password");
            postMessage.Add("username", username);
            postMessage.Add("password", password);
            postMessage.Add("scope", "openid");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.Value.Domain}/oauth2/default/v1/token")
            {
                Content = new FormUrlEncodedContent(postMessage)
            };

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonSerializerSetting = new JsonSerializerSettings();
                var json = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<OktaResponse>(json, jsonSerializerSetting);
                token.ExpiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
            }
            else
            {
                var error = await response.Content?.ReadAsStringAsync();
            }


            return token;
        }
    }
}

