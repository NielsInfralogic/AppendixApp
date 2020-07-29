using AppendixApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppendixApp.Services
{
    public class AppendixHttpClient : IDataService
    {
        private readonly HttpClient client;
        public AppendixHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            };
            client = new HttpClient(handler)
            {
                Timeout = new TimeSpan(0, 10, 0),
                BaseAddress = new Uri(Constants.ApiEndpoint)
            };
            var authenticationBytes = Encoding.ASCII.GetBytes(Constants.ApiUserName + ":" + Constants.ApiPassword);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public async Task<List<Account>> GetAccounts(int companyID)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"Account?id={companyID}");
                //              AnalyticsService.TrackEvent("Response: " + response.StatusCode.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string str = await response.Content.ReadAsStringAsync();
                    //AnalyticsService.TrackEvent(str);
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(str, settings);

                    return accounts ?? new List<Account>();
                }
                else
                {
                    string str = await response.Content.ReadAsStringAsync();

                }
            }
            catch (HttpRequestException hre)
            {

            }
            catch (TaskCanceledException hca)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                /*if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }*/
            }
            return new List<Account>();
        }
        public async Task<List<Appendix>> GetAppendices(int companyID, int userID)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"Appendix?id={companyID}&userID={userID}");
                //              AnalyticsService.TrackEvent("Response: " + response.StatusCode.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string str = await response.Content.ReadAsStringAsync();
                    //AnalyticsService.TrackEvent(str);
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    List<Appendix> appendices = JsonConvert.DeserializeObject<List<Appendix>>(str, settings);

                    return appendices ?? new List<Appendix>();
                }
                else
                {
                    string str = await response.Content.ReadAsStringAsync();

                }
            }
            catch (HttpRequestException hre)
            {

            }
            catch (TaskCanceledException hca)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                /*if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }*/
            }
            return new List<Appendix>();
        }

        public async Task<StatusResponse> SendAppendixAsync(Appendix appendix)
        {          
            try
            {
                string postBody = JsonConvert.SerializeObject(appendix);

                HttpResponseMessage response = await client.PostAsync("Appendix", new StringContent(postBody, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string str = await response.Content.ReadAsStringAsync();
                    //AnalyticsService.TrackEvent(str);
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    StatusResponse statusResponse = JsonConvert.DeserializeObject<StatusResponse>(str, settings);

                    if (statusResponse == null)
                        return new StatusResponse() { Status = 0, Message = "No response received", AppendixID = -1 };

                    return statusResponse;
                }
                else
                {
                    string str = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException hre)
            {
            }
            catch (TaskCanceledException hca)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                /*if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }*/
            }
            return new StatusResponse() { Status = 0, Message = "No response received", AppendixID = -1 };
        }

    }
}
