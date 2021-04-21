using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ArangoDBNetStandard.Transport.Http
{
    public class HttpClientConfig
    {
        public Uri BaseAddress {
            set {
                _config += client => client.BaseAddress = value;
                _key["baseaddress"] = value.ToString();
            }
        }
        public MediaTypeHeaderValue ContentType { 
            get {
                return new MediaTypeHeaderValue(_contentTypeMap[_contentType]);
            }
        }

        public void UseContentType(HttpContentType contentType)
        {
            _contentType = contentType;
            _config += client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(_contentTypeMap[_contentType]));
            };
            _key["type"] = contentType.ToString();
        }

        private Dictionary<string, string> _key = new Dictionary<string, string>();
        private Action<HttpClient> _config = _ => { };
        private Action<HttpClient> _auth = _ => { };

        private HttpClient Create() 
        {
            var cli = new HttpClient();
            _config(cli);
            _auth(cli);
            return cli;
        }

        private HttpContentType _contentType;

        private static readonly Dictionary<HttpContentType, string> _contentTypeMap =
            new Dictionary<HttpContentType, string>
            {
                [HttpContentType.Json] = "application/json",
                [HttpContentType.VPack] = "application/x-velocypack"
            };

        public void SetBasicAuth(string username, string password)
        {
            _auth = client =>
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes($"{username}:{password}")));
            _key["auth"] = $"basic,{username}:{password}";
        }

        public void SetJwtToken(string jwt)
        {
            _auth = client =>
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "bearer",
                   jwt);
            };
            _key["auth"] = $"jwt,{jwt}";
        }

        static Dictionary<string, HttpClient> _pool = new Dictionary<string, HttpClient>();

        public HttpClient Build()
        {
            HttpClient client;
            var key = string.Join(";", _key);
            lock (_pool) {
                if (!_pool.TryGetValue(key, out client)) {
                    client = Create();
                    _pool[key] = client;
                }
            }
            return client;
        }
    }
}