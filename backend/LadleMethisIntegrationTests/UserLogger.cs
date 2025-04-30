using LadleMeThis.Models.AuthContracts;
using System.Net.Http.Json;

namespace LadleMethisIntegrationTests
{
    public class UserLogger
    {
        private readonly HttpClient _client;

        public UserLogger(HttpClient client)
        {
            _client = client;
        }

        public async Task LoginUser(string email, string password)
        {
            var loginRequest = new AuthRequest(email, password);

            var response = await _client.PostAsJsonAsync("/login", loginRequest);

            AttachAuthCookies(response);
        }

        private void AttachAuthCookies(HttpResponseMessage loginResponse)
        {
            var cookies = loginResponse.Headers.GetValues("Set-Cookie").ToList();
            foreach (var cookie in cookies)
            {
                _client.DefaultRequestHeaders.Add("Cookie", cookie);
            }
        }
    }
}
