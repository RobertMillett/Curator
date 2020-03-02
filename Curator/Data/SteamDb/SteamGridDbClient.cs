using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
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

            var bigPictureFilePath = Path.Combine(ImageLocation, Path.GetFileNameWithoutExtension(rom.FileName).TrimEnd(' '), "Big Picture").TrimEnd(' ');
            var libraryPictureFilePath = Path.Combine(ImageLocation, Path.GetFileNameWithoutExtension(rom.FileName).TrimEnd(' '), "Library").TrimEnd(' ');

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
            request.AddParameter("dimensions", "legacy");

            var response = await _steamGridDbClient.ExecuteTaskAsync(request);
            var bigPictureGridUrls = JsonConvert.DeserializeObject<GridUrlsResponse>(response.Content).Data;
            
            await SaveImagesToDisc(bigPictureFilePath, bigPictureGridUrls);

            request.AddOrUpdateParameter("dimensions", "600x900");

            response = await _steamGridDbClient.ExecuteTaskAsync(request);
            var libraryPictureUrls = JsonConvert.DeserializeObject<GridUrlsResponse>(response.Content).Data;

            await SaveImagesToDisc(libraryPictureFilePath, libraryPictureUrls);

            if (bigPictureGridUrls.Count > 0)
                rom.GridPicture = Directory.GetFiles(bigPictureFilePath)[0];

            if (libraryPictureUrls.Count > 0)
                rom.LibraryPicture = Directory.GetFiles(libraryPictureFilePath)[0];
        }

        private static async Task SaveImagesToDisc(string bigPictureFilePath, List<GridUrlData> bigPictureGridUrls)
        {
            foreach (var gridUrl in bigPictureGridUrls)
            {
                await SaveGridImageToDisk(bigPictureFilePath, gridUrl.Url);
            }
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
