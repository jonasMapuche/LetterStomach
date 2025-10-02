using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace LetterStomach.Services
{
    public class HttpService : IHttpService
    {
        #region ERROR
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        //private string URL = "http://192.168.0.3:8885/";
        private string URL = "http://api.stomach.com.br:8885/";

        private HttpClient _client;
        #endregion

        #region CONSTRUCTOR
        public HttpService()
        {
            try 
            {
                _client = new HttpClient();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region POST
        public async Task<string> HttpPost(Grammar message)
        {
            try
            {
                string json = JsonConvert.SerializeObject(message);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await _client.PostAsync(URL, data);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<string> HttpPost(StreamContent message, string file_name)
        {
            try
            {
                string path = "File";
                string uri = URL + path;
                using var content = new MultipartFormDataContent();
                content.Add(message, "\"fileUpload\"", $"{file_name}");
                using HttpResponseMessage response = await _client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<Locution> HttpGo(GoMessage message)
        {
            try
            {
                string path = "Go";
                string uri = URL + path;
                string json = JsonConvert.SerializeObject(message);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await _client.PostAsync(URL, data);
                string result = await response.Content.ReadAsStringAsync();
                Locution request = new Locution();
                request = JsonConvert.DeserializeObject<Locution>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region GET
        private async Task<string> HttpGet(string path)
        {
            try 
            { 
                string uri = URL + path;
                using HttpResponseMessage response = await _client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Adverbios>> HttpAdverb()
        {
            try 
            { 
                string path = "Adverb";
                string result = await HttpGet(path);
                List<Adverbios> request = new List<Adverbios>();
                request = JsonConvert.DeserializeObject<List<Adverbios>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Pronomes>> HttpPronoun()
        {
            try 
            { 
                string path = "Pronoun";
                string result = await HttpGet(path);
                List<Pronomes> request = new List<Pronomes>();
                request = JsonConvert.DeserializeObject<List<Pronomes>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Artigos>> HttpArticle()
        {
            try 
            { 
                string path = "Article";
                string result = await HttpGet(path);
                List<Artigos> request = new List<Artigos>();
                request = JsonConvert.DeserializeObject<List<Artigos>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Numerais>> HttpNumeral()
        {
            try
            {
                string path = "Numeral";
                string result = await HttpGet(path);
                List<Numerais> request = new List<Numerais>();
                request = JsonConvert.DeserializeObject<List<Numerais>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Preposicoes>> HttpPreposition()
        {
            try
            {
                string path = "Preposition";
                string result = await HttpGet(path);
                List<Preposicoes> request = new List<Preposicoes>();
                request = JsonConvert.DeserializeObject<List<Preposicoes>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Substantivo>> HttpNoun()
        {
            try
            {
                string path = "Noun";
                string result = await HttpGet(path);
                List<Substantivo> request = new List<Substantivo>();
                request = JsonConvert.DeserializeObject<List<Substantivo>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Adjetivo>> HttpAdjective()
        {
            try
            {
                string path = "Adjective";
                string result = await HttpGet(path);
                List<Adjetivo> request = new List<Adjetivo>();
                request = JsonConvert.DeserializeObject<List<Adjetivo>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Verbos>> HttpVerb()
        {
            try
            {
                string path = "Verb";
                string result = await HttpGet(path);
                List<Verbos> request = new List<Verbos>();
                request = JsonConvert.DeserializeObject<List<Verbos>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Sentencas>> HttpSentence()
        {
            try
            {
                string path = "Sentence";
                string result = await HttpGet(path);
                List<Sentencas> request = new List<Sentencas>();
                request = JsonConvert.DeserializeObject<List<Sentencas>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Conjuncoes>> HttpConjunction()
        {
            try
            {
                string path = "Conjunction";
                string result = await HttpGet(path);
                List<Conjuncoes> request = new List<Conjuncoes>();
                request = JsonConvert.DeserializeObject<List<Conjuncoes>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public async Task<List<Auxiliares>> HttpAuxiliary()
        {
            try
            {
                string path = "Auxiliary";
                string result = await HttpGet(path);
                List<Auxiliares> request = new List<Auxiliares>();
                request = JsonConvert.DeserializeObject<List<Auxiliares>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion
    }
}
