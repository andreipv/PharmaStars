using MVC.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Services
{
    public class AuthenticationService
    {
        readonly string accountUri = "http://localhost:51306/api/account";
        readonly string tokenUri = "http://localhost:51306/token";

        public async Task<string> Login(LoginModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage requestToken = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(tokenUri),
                    Content = new StringContent(
                        string.Format("grant_type=password&username={0}&password={1}", model.UserName, model.Password)
                    )
                };
                requestToken.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
                
                var bearerResult = await httpClient.SendAsync(requestToken);
                var bearerData = await bearerResult.Content.ReadAsStringAsync();
                var bearerToken = JObject.Parse(bearerData)["access_token"].ToString();
                if(bearerResult.IsSuccessStatusCode)
                {
                    return bearerToken;
                }
                else
                {
                    throw new Exception("Invalid credentials");
                }
            }
        }

        public async Task Register(RegisterModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(accountUri + "/register"),
                    Content = new StringContent(
                        string.Format("username={0}&email={1}&password={2}&confirmpassword={3}",
                            model.UserName, model.Email, model.Password, model.ConfirmPassword)
                    )
                };
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };

                var result = await httpClient.SendAsync(request);
                
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong");
                }
            }
        }

        public async Task ForgotPassword(ForgotPasswordModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(accountUri + "/ForgotPassword"),
                    Content = new StringContent(
                        string.Format("email={0}", model.Email)
                    )
                };
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };

                var result = await httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong");
                }
            }
        }
    }
}