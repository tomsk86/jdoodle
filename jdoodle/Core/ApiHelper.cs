using jdoodle.Core.ApiClient;
using System.Net.Http.Headers;

namespace jdoodle.Core
{
    public static class ApiHelper
    {
        public static JDoodleClient JDoodleClient { get; set; }

        public static void InitializeClient()
        {
            JDoodleClient = new JDoodleClient();
            JDoodleClient.DefaultRequestHeaders.Accept.Clear();
            JDoodleClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}