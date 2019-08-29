namespace Curator.Data
{
    class SteamUser
    {
        public string AccountName { get; set; }
        public string PersonaName { get; set; }
        public int RememberPassword { get; set; }
        public int MostRecent { get; set; }
        public int Timestamp { get; set; }
        public int WantsOfflineMode { get; set; }
    }
}
