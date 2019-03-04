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
        public static string ImageLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Curator", "Images");

        private static HttpClient _steamGridDbClient = new HttpClient { BaseAddress = new Uri("http://www.steamgriddb.com/api/") };

        private static List<string> _gamesList = new List<string>();

        private static async Task PopulateGamesList()
        {
            if (_gamesList.Count > 0)
                return;

            var response = await _steamGridDbClient.GetAsync("games").Result.Content.ReadAsStringAsync();

            _gamesList = JsonConvert.DeserializeObject<GamesResponse>(response).Games;
        }

        public static async Task FetchGamePictures(CuratorDataSet.ROMRow rom)
        {
            await PopulateGamesList();

            var filePath = Path.Combine(ImageLocation, rom.Name);

            if (!_gamesList.Where(x => x == rom.Name).Any())
                return;

            var response = await _steamGridDbClient.GetAsync($"grids?game={rom.Name}&fields=grid_url").Result.Content.ReadAsStringAsync();
            var gridUrls = JsonConvert.DeserializeObject<GridUrlsResponse>(response).Data;

            foreach (var gridUrl in gridUrls)
            {
                await SaveGridImageToDisk(filePath, gridUrl.Grid_url);
            }

            rom.GridPicture = Directory.GetFiles(filePath)[0];
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
