using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace LetterStomach.Services
{
    public class HttpService : IHttpService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        private string _url = "http://192.168.0.3:8885/";
        //private string _url = "http://api.stomach.com.br:8885/";

        private HttpClient _httpClient;
        #endregion

        #region CONSTRUCTOR
        public HttpService()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Http\" service failed!");
                else this.error_message = string.Empty;

                this._httpClient = new HttpClient();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region HTTP
        public async Task<string> HttpPost(Grammar message)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http post \"Http\" service failed!");

                string json = JsonConvert.SerializeObject(message);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await this._httpClient.PostAsync(this._url, data);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<string> HttpPost(StreamContent message, string file_name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http post \"Http\" service failed!");

                string path = "File";
                string uri = this._url + path;
                using var content = new MultipartFormDataContent();
                content.Add(message, "\"fileUpload\"", $"{file_name}");
                using HttpResponseMessage response = await this._httpClient.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Locution>> HttpGo(GoMessage message)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http go \"Http\" service failed!");

                string path = "Message";
                string uri = this._url + path;
                string json = JsonConvert.SerializeObject(message);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await this._httpClient.PostAsync(uri, data);
                string result = await response.Content.ReadAsStringAsync();
                List<Locution> request = new List<Locution>();
                request = JsonConvert.DeserializeObject<List<Locution>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> HttpGet(string path)
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation http get \"Http\" service failed!");

                string uri = this._url + path;
                using HttpResponseMessage response = await this._httpClient.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region LETTER
        public async Task<List<Adverbios>> HttpAdverb()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation http adverb \"Http\" service failed!");

                string path = "Adverb";
                string result = await HttpGet(path);
                List<Adverbios> request = new List<Adverbios>();
                request = JsonConvert.DeserializeObject<List<Adverbios>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Pronomes>> HttpPronoun()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation http pronoun \"Http\" service failed!");

                string path = "Pronoun";
                string result = await HttpGet(path);
                List<Pronomes> request = new List<Pronomes>();
                request = JsonConvert.DeserializeObject<List<Pronomes>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Artigos>> HttpArticle()
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation http article \"Http\" service failed!");

                string path = "Article";
                string result = await HttpGet(path);
                List<Artigos> request = new List<Artigos>();
                request = JsonConvert.DeserializeObject<List<Artigos>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Numerais>> HttpNumeral()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http numeral \"Http\" service failed!");

                string path = "Numeral";
                string result = await HttpGet(path);
                List<Numerais> request = new List<Numerais>();
                request = JsonConvert.DeserializeObject<List<Numerais>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Preposicoes>> HttpPreposition()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http preposition \"Http\" service failed!");

                string path = "Preposition";
                string result = await HttpGet(path);
                List<Preposicoes> request = new List<Preposicoes>();
                request = JsonConvert.DeserializeObject<List<Preposicoes>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Substantivo>> HttpNoun()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http noun \"Http\" service failed!");

                string path = "Noun";
                string result = await HttpGet(path);
                List<Substantivo> request = new List<Substantivo>();
                request = JsonConvert.DeserializeObject<List<Substantivo>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Model>> HttpModel()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http model \"Http\" service failed!");

                string path = "Model";
                string result = await HttpGet(path);
                List<Model> request = new List<Model>();
                request = JsonConvert.DeserializeObject<List<Model>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Adjetivo>> HttpAdjective()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http adjective \"Http\" service failed!");

                string path = "Adjective";
                string result = await HttpGet(path);
                List<Adjetivo> request = new List<Adjetivo>();
                request = JsonConvert.DeserializeObject<List<Adjetivo>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Verbos>> HttpVerb()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http verb \"Http\" service failed!");

                string path = "Verb";
                string result = await HttpGet(path);
                List<Verbos> request = new List<Verbos>();
                request = JsonConvert.DeserializeObject<List<Verbos>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Sentencas>> HttpSentence()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http sentence \"Http\" service failed!");

                string path = "Sentence";
                string result = await HttpGet(path);
                List<Sentencas> request = new List<Sentencas>();
                request = JsonConvert.DeserializeObject<List<Sentencas>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Conjuncoes>> HttpConjunction()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http conjunction \"Http\" service failed!");

                string path = "Conjunction";
                string result = await HttpGet(path);
                List<Conjuncoes> request = new List<Conjuncoes>();
                request = JsonConvert.DeserializeObject<List<Conjuncoes>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Auxiliares>> HttpAuxiliary()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation http auxiliary \"Http\" service failed!");

                string path = "Auxiliary";
                string result = await HttpGet(path);
                List<Auxiliares> request = new List<Auxiliares>();
                request = JsonConvert.DeserializeObject<List<Auxiliares>>(result);
                return request;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion
    }
}
