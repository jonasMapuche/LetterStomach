using LetterStomach.Data;
using LetterStomach.Models;
using MongoDB.Driver;

namespace LetterStomach.Repositories.MongoDBs
{
    public class MateriaRepository : IMateriaRepository
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

        public event EventHandler<string>? OnError;
        #endregion

        #region VARIABLE
        private readonly IMongoCollection<Materia> _collection;
        #endregion

        #region CONSTRUCTOR
        public MateriaRepository()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Material\" repository failed!");
                else this._error_message = string.Empty;

                string connection = "mongodb://berthazatz:freedown@ac-dgizj8w-shard-00-00.88ewwu7.mongodb.net:27017,ac-dgizj8w-shard-00-01.88ewwu7.mongodb.net:27017,ac-dgizj8w-shard-00-02.88ewwu7.mongodb.net:27017/?ssl=true&replicaSet=atlas-4bl81c-shard-0&authSource=admin&appName=letter";
                string database = "stomach";
                string collection = "letter";
                var mongoClient = new MongoClient(connection);
                var mongoDatabase = mongoClient.GetDatabase(database);
                this._collection = mongoDatabase.GetCollection<Materia>(collection);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public MateriaRepository(MaterialContext materialContext)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Material\" repository failed!");
                else this._error_message = string.Empty;

                this._collection = materialContext.GetCollection();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region GET
        public List<Materia> GetLessonSimple(bool lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson simples \"Material\" repository failed!");

                return this._collection.Find(index => index.linguagem == language && index.licao == lesson).ToList<Materia>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Materia>> GetLessonSimpleAsync(bool lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson simples async \"Material\" repository failed!");

                return this._collection.Find(index => index.linguagem == language && index.licao == lesson).ToList<Materia>();
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
