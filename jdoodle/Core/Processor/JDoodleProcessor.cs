using jdoodle.Const;
using jdoodle.Core.ApiClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jdoodle.Core.Processor
{
    public class JDoodleProcessor
    {
        private static JDoodleClient _client;

        public JDoodleProcessor()
        {
            ApiHelper.InitializeClient();
            _client = ApiHelper.JDoodleClient;
        }

        public async Task<JDoodleResponse> PostAsync(JDoodleRequest request)
        {
            MapRequest(ref request);

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(request, serializerSettings);

            HttpContent content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("execute", content);
            result.EnsureSuccessStatusCode();

            string resultContentString = await result.Content.ReadAsStringAsync();
            var resultContent = JsonConvert.DeserializeObject<JDoodleResponse>(resultContentString);

            return resultContent;
        }

        private void MapRequest(ref JDoodleRequest contentValue)
        {
            contentValue.ClientId = _client.ClientID;
            contentValue.ClientSecret = _client.ClientSecret;
            contentValue.Language = Language.CSharp;
            contentValue.VersionIndex = 0;
        }
    }

    [Serializable]
    public class JDoodleRequest
    {
        /// <summary>
        /// Client Secret for your subscription
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret for your subscription
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Program to compile and execute
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// StdIn
        /// </summary>
        public string Stdin { get; set; }

        /// <summary>
        /// Language of the script(refer the supported language list below)
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Version of the language to be used(refere the supportoed languages and versions in list below)
        /// </summary>
        public int VersionIndex { get; set; }
    }

    [Serializable]
    public class JDoodleResponse
    {
        /// <summary>
        /// Output of the program
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Status Code of the result
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Memory used by the program
        /// </summary>
        public string Memory { get; set; }

        /// <summary>
        /// CPU Time used by the program
        /// </summary>
        public string CpuTime { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }
    }
}