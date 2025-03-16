using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Physics.Core.Configuration
{
    [JsonSerializable(typeof(AppConfig))]
    public class AppConfig
    {
        public int Width { get; set; } = 1280;
        public int Height { get; set; } = 720;
        public bool Fullscreen { get; set; } = false;

        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appconfig.json");

        public static AppConfig Load()
        {
            if (!File.Exists(ConfigPath))
            {
                var defaultConfig = new AppConfig();
                defaultConfig.Save();
                return defaultConfig;
            }

            var jsonString = File.ReadAllText(ConfigPath);
            return JsonSerializer.Deserialize<AppConfig>(jsonString) ?? new AppConfig();
        }

        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
            };
            var jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(ConfigPath, jsonString);
        }
    }
}