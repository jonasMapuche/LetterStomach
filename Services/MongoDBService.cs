using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services
{
    public class MongoDBService : IMongoDBService
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
        public IMongoCollection<Circunstancia> Circunstancia { get; set; }
        public IMongoCollection<Estoutro> Estoutro { get; set; }
        public IMongoCollection<Preceito> Preceito { get; set; }
        public IMongoCollection<Algarismo> Algarismo { get; set; }
        public IMongoCollection<Elocucao> Elocucao { get; set; }
        public IMongoCollection<Juncao> Juncao { get; set; }
        public IMongoCollection<Materia> Materia { get; set; }
        public IMongoCollection<Sentenca> Sentenca { get; set; }
        public IMongoCollection<Ligacao> Ligacao { get; set; }
        public IMongoCollection<Assistente> Assistente { get; set; }
        #endregion

        #region CONNECT
        public void Connect()
        {
            try 
            { 
                LoadCircustancia();
                LoadEstoutro();
                LoadPreceito();
                LoadAlgarismo();
                LoadJuncao();
                LoadElocucao();
                LoadMateria();
                LoadSentenca();
                LoadLigacao();
                LoadAssistente();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region LOAD
        public void LoadCircustancia() 
        {
            try 
            { 
                string connection = "mongodb://labrouste:freedown@ac-qs3nere-shard-00-00.twimpt2.mongodb.net:27017,ac-qs3nere-shard-00-01.twimpt2.mongodb.net:27017,ac-qs3nere-shard-00-02.twimpt2.mongodb.net:27017/?ssl=true&replicaSet=atlas-a5rh82-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusteradverb";
                string database = "stomach";
                string collection = "adverb";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Circunstancia> ConfigurationValue = mongoDatabase.GetCollection<Circunstancia>(collection);
                this.Circunstancia = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadEstoutro()
        {
            try
            { 
                string connection = "mongodb://labrouste:freedown@ac-4droh61-shard-00-00.trwpuy0.mongodb.net:27017,ac-4droh61-shard-00-01.trwpuy0.mongodb.net:27017,ac-4droh61-shard-00-02.trwpuy0.mongodb.net:27017/?ssl=true&replicaSet=atlas-xrl9qi-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterpronoun";
                string database = "stomach";
                string collection = "pronoun";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Estoutro> ConfigurationValue = mongoDatabase.GetCollection<Estoutro>(collection);
                this.Estoutro = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadPreceito()
        {
            try 
            { 
                string connection = "mongodb://labrouste:freedown@ac-dzsg50m-shard-00-00.brox8ik.mongodb.net:27017,ac-dzsg50m-shard-00-01.brox8ik.mongodb.net:27017,ac-dzsg50m-shard-00-02.brox8ik.mongodb.net:27017/?ssl=true&replicaSet=atlas-143za4-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterarticle";
                string database = "stomach";
                string collection = "article";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Preceito> ConfigurationValue = mongoDatabase.GetCollection<Preceito>(collection);
                this.Preceito = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadAlgarismo()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@clusternumeral-shard-00-00.m6ca3.mongodb.net:27017,clusternumeral-shard-00-01.m6ca3.mongodb.net:27017,clusternumeral-shard-00-02.m6ca3.mongodb.net:27017/?ssl=true&replicaSet=atlas-zqs044-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusternumeral";
                string database = "stomach";
                string collection = "numeral";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Algarismo> ConfigurationValue = mongoDatabase.GetCollection<Algarismo>(collection);
                this.Algarismo = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadElocucao()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@clusterverb-shard-00-00.yhx9b.mongodb.net:27017,clusterverb-shard-00-01.yhx9b.mongodb.net:27017,clusterverb-shard-00-02.yhx9b.mongodb.net:27017/?ssl=true&replicaSet=atlas-bgtfxu-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterverb";
                string database = "stomach";
                string collection = "verb";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Elocucao> ConfigurationValue = mongoDatabase.GetCollection<Elocucao>(collection);
                this.Elocucao = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadJuncao()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@ac-qbpfxxr-shard-00-00.hjeuzew.mongodb.net:27017,ac-qbpfxxr-shard-00-01.hjeuzew.mongodb.net:27017,ac-qbpfxxr-shard-00-02.hjeuzew.mongodb.net:27017/?ssl=true&replicaSet=atlas-ket9j8-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterpreposition";
                string database = "stomach";
                string collection = "preposition";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Juncao> ConfigurationValue = mongoDatabase.GetCollection<Juncao>(collection);
                this.Juncao = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadMateria()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@ac-jiagffd-shard-00-00.hh85dxs.mongodb.net:27017,ac-jiagffd-shard-00-01.hh85dxs.mongodb.net:27017,ac-jiagffd-shard-00-02.hh85dxs.mongodb.net:27017/?ssl=true&replicaSet=atlas-ryd5gy-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterletter";
                string database = "stomach";
                string collection = "letter";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Materia> ConfigurationValue = mongoDatabase.GetCollection<Materia>(collection);
                this.Materia = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadSentenca()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@ac-3y1axe2-shard-00-00.n5y9bze.mongodb.net:27017,ac-3y1axe2-shard-00-01.n5y9bze.mongodb.net:27017,ac-3y1axe2-shard-00-02.n5y9bze.mongodb.net:27017/?ssl=true&replicaSet=atlas-ifgens-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clustersentence";
                string database = "stomach";
                string collection = "sentence";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Sentenca> ConfigurationValue = mongoDatabase.GetCollection<Sentenca>(collection);
                this.Sentenca = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadLigacao()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@ac-mtuqyju-shard-00-00.nqdittm.mongodb.net:27017,ac-mtuqyju-shard-00-01.nqdittm.mongodb.net:27017,ac-mtuqyju-shard-00-02.nqdittm.mongodb.net:27017/?ssl=true&replicaSet=atlas-cffrmz-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterconjunction";
                string database = "stomach";
                string collection = "conjunction";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Ligacao> ConfigurationValue = mongoDatabase.GetCollection<Ligacao>(collection);
                this.Ligacao = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void LoadAssistente()
        {
            try
            {
                string connection = "mongodb://labrouste:freedown@clusterauxiliary-shard-00-00.kwsaj.mongodb.net:27017,clusterauxiliary-shard-00-01.kwsaj.mongodb.net:27017,clusterauxiliary-shard-00-02.kwsaj.mongodb.net:27017/?ssl=true&replicaSet=atlas-3mfvr2-shard-0&authSource=admin&retryWrites=true&w=majority&appName=clusterauxiliary";
                string database = "stomach";
                string collection = "auxiliary";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                IMongoCollection<Assistente> ConfigurationValue = mongoDatabase.GetCollection<Assistente>(collection);
                this.Assistente = ConfigurationValue;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion
    }
}
