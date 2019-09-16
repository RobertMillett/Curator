using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RestSharp;
using RestSharp.Extensions;

namespace Curator.Data.SteamDb
{
    public static class SteamGridDbClient
    {
        public static string ImageLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Curator", "Images");

        private static RestSharp.RestClient _steamGridDbClient = new RestClient
        {
            BaseUrl = new Uri("https://www.steamgriddb.com/api/v2")
        };

        private static Dictionary<string, int> _gamesDictionary = new Dictionary<string, int>();
        public static List<string> _gamesNotFound = new List<string>();

        private static async Task PopulateGamesList(string romName)
        {
            if (_gamesDictionary.ContainsKey(romName))
                return;

            var request = new RestRequest {
                Method = Method.GET,
                Resource = $"/search/autocomplete/{romName}"
            };
            request.AddHeader("Authorization", "Bearer 850ea77df1c635de67acc76bbb197bd7");

            var response = await _steamGridDbClient.ExecuteTaskAsync(request);

            foreach (var game in JsonConvert.DeserializeObject<SearchResponse>(response.Content).Data)
            {
                if (GetUrlEncodedROMName(game.Name) == romName)
                {
                    _gamesDictionary.Add(romName, game.Id);
                    return;
                }
            }
        }

        public static async Task FetchGamePictures(CuratorDataSet.ROMRow rom)
        {
            var urlEncodedROMName = GetUrlEncodedROMName(rom.Name);

            await PopulateGamesList(urlEncodedROMName);

            var filePath = Path.Combine(ImageLocation, Path.GetFileNameWithoutExtension(rom.FileName)).TrimEnd(' ');

            if (!_gamesDictionary.Where(x => x.Key == urlEncodedROMName).Any())
            {
                _gamesNotFound.Add(rom.Name);
                return;
            }

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"/grids/game/{ _gamesDictionary[urlEncodedROMName]}"                
            };
            request.AddHeader("Authorization", "Bearer 850ea77df1c635de67acc76bbb197bd7");

            var response = await _steamGridDbClient.ExecuteTaskAsync(request);
            var gridUrls = JsonConvert.DeserializeObject<GridUrlsResponse>(response.Content).Data;

            foreach (var gridUrl in gridUrls)
            {
                await SaveGridImageToDisk(filePath, gridUrl.Url);
            }

            if (gridUrls.Count > 0)
                rom.GridPicture = Directory.GetFiles(filePath)[0];
        }

        private static string GetUrlEncodedROMName(string name)
        {
            string urlEncodedROMName;
            using (StringWriter sw = new StringWriter())
            {
                var x = new HtmlTextWriter(sw);
                x.WriteEncodedUrlParameter(name);
                urlEncodedROMName = sw.ToString();
            }
            return urlEncodedROMName;
        }

        private static async Task SaveGridImageToDisk(string path, string grid_url)
        {
            var file = new FileInfo(Path.Combine(path, Path.GetFileName(grid_url)));
            file.Directory.Create();
            
            var client = new RestClient();
            await Task.Run(() => client.DownloadData(new RestRequest { Resource = grid_url }).SaveAs(file.FullName));
        }
    }


    
}
