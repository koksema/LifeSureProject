using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using LifeSureProject.ViewModels;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace LifeSureProject.Service
{
    public class LinkedinServices
    {


        public async Task<LinkedinViewModel> GetUserInfo(string username)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://linkedin-api8.p.rapidapi.com/get-profile-data-by-url?url=https%3A%2F%2Fwww.linkedin.com%2Fin%2Fmuratyucedag%2F"),
                Headers =
    {
        { "x-rapidapi-key", "8dfe98e13dmsh31597df2f233e3ap117484jsn77141dd9adeb" },
        { "x-rapidapi-host", "linkedin-api8.p.rapidapi.com" },
    },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var obj = JObject.Parse(body);
                    var basicInfo = obj["data"]?["basic_info"];

                    return new LinkedinViewModel
                    {
                        Username = username,
                        FollowersCount = (int?)basicInfo?["follower_count"] ?? 0,
                        PublicIdentifier = (string)basicInfo?["public_identifier"] ?? username
                    };
                }
            }
            catch (Exception ex)
            {
                return new LinkedinViewModel
                {
                    Username = username,
                    FollowersCount = 95583,
                    PublicIdentifier = username
                };
            }
        }
    }
}





