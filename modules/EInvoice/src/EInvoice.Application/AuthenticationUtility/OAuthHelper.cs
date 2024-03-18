using EInvoice.Api;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Validation;

namespace EInvoice.AuthenticationUtility
{
    public class OAuthHelper
    {
        private static string url = "";
        private static string clientid = "";
        private static string clientsecret = "";
        private static string requestApiUrl = "";

        public OAuthHelper()
        {
        }

        /// <summary>
        /// The header to use for OAuth2.
        /// </summary>
        public const string OAuthHeader = "client_credentials";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        //public async Task<string> GetAuthenticationHeader()
        //{
        //    string bearerToken = await this.getBearerAccessToken();
        //    if (bearerToken == "")
        //        throw new UserFriendlyException("Unable to authenticate connection!");

        //    HttpClient requestHttpClient = new HttpClient();
        //    requestHttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
        //    var requestResponse = await requestHttpClient.GetAsync(requestApiUrl);
        //    if (requestResponse.IsSuccessStatusCode)
        //        return await requestResponse.Content.ReadAsStringAsync();
        //    else
        //        throw new UserFriendlyException("Error from sending request: " + requestResponse.StatusCode.ToString());

        //    //if (d365ClientConfiguration == null)
        //    //    d365ClientConfiguration = D365ClientConfiguration.Default;
        //    //bool useWebAppAuthentication = d365ClientConfiguration.UseWebAppAuthentication;
        //    //string aadTenant = d365ClientConfiguration.ActiveDirectoryTenant;
        //    //string aadClientAppId = d365ClientConfiguration.ActiveDirectoryClientAppId;
        //    //string aadResource = d365ClientConfiguration.ActiveDirectoryResource;
        //    //// OAuth through username and password.
        //    //string username = d365ClientConfiguration.UserName;
        //    //string password = d365ClientConfiguration.Password;
        //    //string aadClientAppSecret = d365ClientConfiguration.ActiveDirectoryClientAppSecret;

        //    //AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, d365ClientConfiguration.ValidateAuthority);
        //    //AuthenticationResult authenticationResult;

        //    //if (useWebAppAuthentication)
        //    //{
        //    //    var creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
        //    //    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, creadential).Result;
        //    //}
        //    //else
        //    //{
        //    //    // Get token object
        //    //    // var userCredential = new UserPasswordCredential(username, password);// for .net 4.5 above
        //    //    var userCredential = new UserCredential(username);// for .net 4

        //    //    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, aadClientAppId, userCredential).Result;
        //    //}

        //    //// Create and get JWT token
        //    //return authenticationResult.CreateAuthorizationHeader();
        //}

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader()
        {
            HttpClient httpClient = new HttpClient();
            var content = new StringContent("grant_type="+ OAuthHeader + "&scope=https://api.businesscentral.dynamics.com/.default&client_id="
            + HttpUtility.UrlEncode(clientid) + "&client_secret=" + HttpUtility.UrlEncode(clientsecret));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = httpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                JObject result = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                return result["access_token"].ToString();
            }
            else
            {
                Console.WriteLine("Error from getting access token: " + response.StatusCode.ToString());
                return "";
            }
        }
    }
}
