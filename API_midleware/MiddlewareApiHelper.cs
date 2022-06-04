using Middleware_Api_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Middleware_Api_Lib
{
    public class MiddlewareApiHelper
    {
        public async Task<Trailer> GetTrailer(string search)
        {
            Trailer res = new Trailer();
            HeaderModel header;
            using (HttpResponseMessage response = await MiddlewareApi.httpClient.GetAsync(Constants.SearchUrl + search))
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

            List<string> apiUrls = new List<string> { Constants.ImdbUrl + header.Results[0].Id, Constants.YoutubeUrl + header.Results[0].Id };

            return res = await GetTrailerVideos(apiUrls);
        }
        private async Task<Trailer> GetTrailerVideos(List<string> searches)
        {
            HttpResponseMessage[] responses = await Task.WhenAll(searches.Select(x => GetResponseAsync(x)));
            var results = await Task.WhenAll(responses.Select(r => ProcessResponseAsync(r)));

            var result = AggregateResults(results);

            return result;
        }

        private Trailer AggregateResults(Trailer[] results)
        {
            Trailer result = new Trailer();
            foreach (var trailer in results)
            {
                foreach (PropertyInfo prop in typeof(Trailer).GetProperties())
                {
                    var value = prop.GetValue(trailer, null);
                    if (value != null)
                    {
                        prop.SetValue(result, value);
                    }
                }
            }
            return result;
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string search)
        {
            return await MiddlewareApi.httpClient.GetAsync(search);
        }

        private async Task<Trailer> ProcessResponseAsync(HttpResponseMessage response)
        {
            Trailer res = new Trailer();
            using (response)
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.RequestMessage.RequestUri.ToString().Contains(Constants.YoutubeUrl))
                    {
                        YoutubeModel result = await response.Content.ReadAsAsync<YoutubeModel>();

                        res.YtVideoUrl = result.VideoUrl;
                        res.Title = result.Title;
                        return res;
                    }
                    else if (response.RequestMessage.RequestUri.ToString().Contains(Constants.ImdbUrl))
                    {
                        ImdbModel result = await response.Content.ReadAsAsync<ImdbModel>();

                        res.ImdbVideoUrl = result.Link;
                        res.LinkEmbed = result.LinkEmbed;
                        res.FullTitle = result.FullTitle;

                        return res;
                    }
                    else
                    {
                        throw new Exception("Request type not handled!");
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
