namespace Sandbox
{
    using Newtonsoft.Json;

    internal class CountryTownsDto
    {
        [JsonRequired]
        public string country { get; set; }

        public string[] towns { get; set; }
    }
}
