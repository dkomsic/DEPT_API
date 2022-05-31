using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_midleware
{
    public class YoutubeApi
    {
        public static async Task<YoutubeModel> LoadYoutubeTrailer(string search)
        {
            string url = "https://imdb-api.com/API/Search/k_f0v2atkw/";

            HeaderModel header;
            using (HttpResponseMessage response = await ApiHelper.httpClient.GetAsync(url + search))
            {
                if (response.IsSuccessStatusCode)
                {
                    header = await response.Content.ReadAsAsync<HeaderModel>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }    
            }

            if (header.Results != null)
            {
                url = "https://imdb-api.com/API/YouTubeTrailer/k_f0v2atkw/" + header.Results[0].Id;
                using (HttpResponseMessage response = await ApiHelper.httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        YoutubeModel result = await response.Content.ReadAsAsync<YoutubeModel>();
                        return result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            else
            {
                throw new Exception("Header results returned null");
            }


        }
    }
}
