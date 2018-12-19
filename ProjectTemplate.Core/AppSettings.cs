using Microsoft.Extensions.Configuration;

namespace ProjectTemplate.Core
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
        public const string STORAGE_PATH = "Storage:Path";

    }
}