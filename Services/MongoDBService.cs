using LetterStomach.Data;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services 
{
    public class MongoDBService : IMongoDBService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string? _error_message;

        public string? error_message
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
        private AlgarismoContext _algarismoContext;
        private AssistenteContext _assistenteContext;
        private CircunstanciaContext _circunstanciaContex;
        private ElocucaoContext _elocucaoContex;
        private EstoutroContext _estoutroContex;
        private JuncaoContext _juncaoContex;
        private LigacaoContext _ligacaoContex;
        private MaterialContext _materialContex;
        private PreceitoContext _preceitoContex;
        private SentencaContext _sentencaContex;

        public AlgarismoContext AlgarismoContext { get => _algarismoContext; }
        public AssistenteContext AssistenteContext { get => _assistenteContext; }
        public CircunstanciaContext CircunstanciaContex { get => _circunstanciaContex; }
        public ElocucaoContext ElocucaoContext { get => _elocucaoContex; }
        public EstoutroContext EstoutroContext { get => _estoutroContex; }
        public JuncaoContext JuncaoContext { get => _juncaoContex; }
        public LigacaoContext LigacaoContext { get => _ligacaoContex; }
        public MaterialContext MaterialContext { get => _materialContex; }
        public PreceitoContext PreceitoContext { get => _preceitoContex; }
        public SentencaContext SentencaContext { get => _sentencaContex; }
        #endregion

        #region CONSTRUCTOR
        public MongoDBService()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"MongoDB\" service failed!");
                else this.error_message = string.Empty;

                this._algarismoContext = new AlgarismoContext();
                this._assistenteContext = new AssistenteContext();
                this._circunstanciaContex = new CircunstanciaContext();
                this._elocucaoContex = new ElocucaoContext();
                this._estoutroContex = new EstoutroContext();
                this._juncaoContex = new JuncaoContext();
                this._ligacaoContex = new LigacaoContext();
                this._materialContex = new MaterialContext();
                this._preceitoContex = new PreceitoContext();
                this._sentencaContex = new SentencaContext();
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
