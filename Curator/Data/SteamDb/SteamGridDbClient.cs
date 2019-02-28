using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Curator.Data.SteamDb
{
    public static class SteamGridDbClient
    {
        private static HttpClient _steamGridDbClient = new HttpClient { BaseAddress = new Uri("http://www.steamgriddb.com/api/") };

        private static List<string> _gamesList = new List<string>();

        private static async Task PopulateGamesList()
        {
            if (_gamesList.Count > 0)
                return;

            var response = await _steamGridDbClient.GetAsync("games").Result.Content.ReadAsStringAsync();

            _gamesList = JsonConvert.DeserializeObject<GamesResponse>(response).Games;
        }

        public static async Task FetchGamePictures(string gameName)
        {
            await PopulateGamesList();

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Curator", "Images", gameName);

            if (!_gamesList.Where(x => x == gameName).Any())
                return;

            var response = await _steamGridDbClient.GetAsync($"grids?game={gameName}&fields=grid_url").Result.Content.ReadAsStringAsync();

            foreach (var gridUrl in JsonConvert.DeserializeObject<GridUrlsResponse>(response).Data)
            {
                await SaveGridImageToDisk(filePath, gridUrl.Grid_url);
            }
        }

        private static async Task SaveGridImageToDisk(string path, string grid_url)
        {
            var file = new FileInfo(Path.Combine(path, Path.GetFileName(grid_url)));
            file.Directory.Create();

            using (var stream = await _steamGridDbClient.GetStreamAsync(grid_url))
            using (var fileStream = new FileStream(file.FullName, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }


    
}
