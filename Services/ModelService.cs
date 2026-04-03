using LetterStomach.Models;
using LetterStomach.Services.Interfaces;

namespace LetterStomach.Services
{
    public class ModelService : IModelService
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

        #region MOUNT OBJECT
        private Circunstancia InsertCircunstancia(string language, string name, List<string> types)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert circustancia \"Model\" service failed!");

                Circunstancia circunstancia = new Circunstancia();
                circunstancia.nome = name;
                circunstancia.linguagem = language;
                circunstancia.tipo = types;
                return circunstancia;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Preceito InsertPreceito(string language, string name, List<string> types, List<string> numbers, List<string> genders)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert preceito \"Model\" service failed!");

                Preceito preceito = new Preceito();
                preceito.nome = name;
                preceito.linguagem = language;
                preceito.tipo = types;
                preceito.numero = numbers;
                preceito.genero = genders;
                return preceito;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Contento InsertContento(List<int> persons, List<string> numbers, List<string> genders, List<string> contexts)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert contento \"Model\" service failed!");

                Contento contento = new Contento();
                contento.pessoa = persons;
                contento.numero = numbers;
                contento.genero = genders;
                contento.contexto = contexts;
                return contento;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Estoutro InsertEstoutro(string language, string name, List<string> types, List<Contento> contentos)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert estoutro \"Model\" service failed!");

                Estoutro estoutro = new Estoutro();
                estoutro.nome = name;
                estoutro.linguagem = language;
                estoutro.tipo = types;
                estoutro.contento = contentos;
                return estoutro;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Algarismo InsertAlgarismo(string language, string name, int inital, List<string> types)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert algarismo \"Model\" service failed!");

                Algarismo algarismo = new Algarismo();
                algarismo.nome = name;
                algarismo.linguagem = language;
                algarismo.sigla = inital;
                algarismo.tipo = types;
                return algarismo;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Juncao InsertJuncao(string language, string name, List<string> types)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert juncao \"Model\" service failed!");

                Juncao juncao = new Juncao();
                juncao.nome = name;
                juncao.linguagem = language;
                juncao.tipo = types;
                return juncao;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Conteudo InsertSubstantivo(List<string> substantivos)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert substantivo \"Model\" service failed!");

                Conteudo conteudo = new Conteudo();
                conteudo.substantivo = substantivos;
                return conteudo;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Conteudo InsertAdjetivo(List<string> adjetivos)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert adjetivo \"Model\" service failed!");

                Conteudo conteudo = new Conteudo();
                conteudo.adjetivo = adjetivos;
                return conteudo;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Conteudo InsertModel(List<string> models)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert model \"Model\" service failed!");

                Conteudo conteudo = new Conteudo();
                conteudo.verbo = models;
                return conteudo;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Materia InsertMateria(string language, string lesson, Conteudo conteudo)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert materia \"Model\" service failed!");

                Materia materia = new Materia();
                materia.nome = lesson;
                materia.linguagem = language;
                materia.conteudo = conteudo;
                materia.titulo = lesson.Split(" ")[0];
                materia.ordem = Int16.Parse(lesson.Split(" ")[1]);
                materia.licao = true;
                return materia;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Sentenca InsertSentenca(string language, string impulse, List<string> rest)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert sentenca \"Model\" service failed!");

                Sentenca sentenca = new Sentenca();
                sentenca.linguagem = language;
                sentenca.impulso = impulse;
                sentenca.repouso = rest;
                return sentenca;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Ligacao InsertLigacao(string language, string name, List<string> type)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert ligacao \"Model\" service failed!");

                Ligacao ligacao = new Ligacao();
                ligacao.linguagem = language;
                ligacao.nome = name;
                ligacao.tipo = type;
                return ligacao;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Teor InsertTeor(List<string> mode, List<string> pronoun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert teor \"Model\" service failed!");

                Teor teor = new Teor();
                teor.modo = mode;
                teor.pronome = pronoun;
                return teor;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Elocucao InsertElocucao(string language, string name, string model, List<Teor> teors)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert elocucao \"Model\" service failed!");

                Elocucao elocucao = new Elocucao();
                elocucao.nome = name;
                elocucao.linguagem = language;
                elocucao.modelo = model;
                elocucao.teor = teors;
                return elocucao;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
                   
        private Tematica InsertTematica(List<string> mode, List<string> prefix, List<string> preverb, List<string> premode)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert tematica \"Model\" service failed!");

                Tematica tematica = new Tematica();
                tematica.modo = mode;
                tematica.prefixo = prefix;
                tematica.preverbo = preverb;
                tematica.premodo = premode;
                return tematica;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private Assistente InsertAssistente(string language, string name, List<Tematica> tematicas)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation insert assistente \"Model\" service failed!");

                Assistente assistente = new Assistente();
                assistente.nome = name;
                assistente.linguagem = language;
                assistente.tematica = tematicas;
                return assistente;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region MOUNT LETTER
        private async Task<List<Materia>> MateriaNoun(List<Substantivo> noun, List<Materia> matter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount noun \"Model\" service failed!");

                List<Substantivo> nouns = new List<Substantivo>();
                nouns = noun.OrderBy(index => index.language).ThenBy(index => index.lesson).ThenBy(index => index.name).ToList();
                List<Materia> materias = new List<Materia>();
                materias = matter;
                string before_language = "";
                string before_lesson = "";
                string substantivo = "";
                string language = "";
                string lesson = "";
                List<string> substantivos = new List<string>();
                Conteudo conteudo = new Conteudo();
                Materia? materia = new Materia();
                foreach (Substantivo item in nouns)
                {
                    language = item.language;
                    lesson = item.lesson;
                    substantivo = item.name;
                    if (lesson.Split(" ").Count() == 1) continue;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_lesson = lesson;
                        substantivos = new List<string>();
                        substantivos.Add(substantivo);
                        continue;
                    }
                    if ((language != before_language) || ((lesson != before_lesson) && (language == before_language)))
                    {
                        materia = materias.FirstOrDefault(index => index.linguagem == before_language && index.nome == before_lesson);
                        if (materia != null)
                        {
                            conteudo = new Conteudo();
                            conteudo = materia.conteudo;
                            conteudo.substantivo = substantivos;
                            int index = materias.IndexOf(materia);
                            materia.conteudo = conteudo;
                            materias[index] = materia;
                            substantivos = new List<string>();
                        }
                        else
                        {
                            conteudo = new Conteudo();
                            conteudo = InsertSubstantivo(substantivos);
                            materia = new Materia();
                            materia = InsertMateria(before_language, before_lesson, conteudo);
                            materias.Add(materia);
                            substantivos = new List<string>();
                        }
                    }
                    before_language = language;
                    before_lesson = lesson;
                    if ((!substantivos.Contains(substantivo)) || (substantivos.Count == 0)) substantivos.Add(substantivo);
                }
                materia = materias.FirstOrDefault(index => index.linguagem == before_language && index.nome == before_lesson);
                if (before_lesson.Split(" ").Count() > 1)
                {
                    if (materia != null)
                    {
                        conteudo = new Conteudo();
                        conteudo = materia.conteudo;
                        conteudo.substantivo = substantivos;
                        int index = materias.IndexOf(materia);
                        materia.conteudo = conteudo;
                        materias[index] = materia;
                        substantivos = new List<string>();
                    }
                    else
                    {
                        conteudo = new Conteudo();
                        conteudo = InsertSubstantivo(substantivos);
                        materia = new Materia();
                        materia = InsertMateria(before_language, before_lesson, conteudo);
                        materias.Add(materia);
                    }
                }
                return materias;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Materia>> MateriaAdjective(List<Adjetivo> adjective, List<Materia> matter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount adjective \"Model\" service failed!");

                List<Adjetivo> adjectives = new List<Adjetivo>();
                adjectives = adjective.OrderBy(index => index.language).ThenBy(index => index.lesson).ThenBy(index => index.name).ToList();
                List<Materia> materias = new List<Materia>();
                materias = matter;
                string before_language = "";
                string before_lesson = "";
                string adjetivo = "";
                string language = "";
                string lesson = "";
                List<string> adjetivos = new List<string>();
                Conteudo conteudo = new Conteudo();
                Materia? materia = new Materia();
                foreach (Adjetivo item in adjectives)
                {
                    language = item.language;
                    lesson = item.lesson;
                    adjetivo = item.name;
                    if (lesson.Split(" ").Count() == 1) continue;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_lesson = lesson;
                        adjetivos = new List<string>();
                        adjetivos.Add(adjetivo);
                        continue;
                    }
                    if ((language != before_language) || ((lesson != before_lesson) && (language == before_language)))
                    {
                        materia = materias.FirstOrDefault(index => index.linguagem == before_language && index.nome == before_lesson);
                        if (materia != null)
                        {
                            conteudo = new Conteudo();
                            conteudo = materia.conteudo;
                            conteudo.adjetivo = adjetivos;
                            int index = materias.IndexOf(materia);
                            materia.conteudo = conteudo;
                            materias[index] = materia;
                            adjetivos = new List<string>();
                        }
                        else
                        {
                            conteudo = new Conteudo();
                            conteudo = InsertAdjetivo(adjetivos);
                            materia = new Materia();
                            materia = InsertMateria(before_language, before_lesson, conteudo);
                            materias.Add(materia);
                            adjetivos = new List<string>();
                        }
                    }
                    before_language = language;
                    before_lesson = lesson;
                    if ((!adjetivos.Contains(adjetivo)) || (adjetivos.Count == 0)) adjetivos.Add(adjetivo);
                }
                materia = materias.FirstOrDefault(index => index.linguagem == before_language && index.nome == before_lesson);
                if (before_lesson.Split(" ").Count() > 1)
                {
                    if (materia != null)
                    {
                        conteudo = new Conteudo();
                        conteudo = materia.conteudo;
                        conteudo.adjetivo = adjetivos;
                        int index = materias.IndexOf(materia);
                        materia.conteudo = conteudo;
                        materias[index] = materia;
                        adjetivos = new List<string>();
                    }
                    else
                    {
                        conteudo = new Conteudo();
                        conteudo = InsertAdjetivo(adjetivos);
                        materia = new Materia();
                        materia = InsertMateria(before_language, before_lesson, conteudo);
                        materias.Add(materia);
                    }
                }
                return materias;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Materia>> MateriaModel(List<Model> model, List<Materia> matter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount model \"Model\" service failed!");

                List<Model> models = new List<Model>();
                models = model.OrderBy(index => index.language).ThenBy(index => index.lesson).ThenBy(index => index.name).ToList();
                List<Materia> materias = new List<Materia>();
                materias = matter;
                string before_language = "";
                string before_lesson = "";
                string verbo = "";
                string language = "";
                string lesson = "";
                List<string> verbos = new List<string>();
                Conteudo conteudo = new Conteudo();
                Materia? materia = new Materia();
                foreach (Model item in models)
                {
                    language = item.language;
                    lesson = item.lesson;
                    verbo = item.name;
                    if (lesson.Split(" ").Count() == 1) continue;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_lesson = lesson;
                        verbos = new List<string>();
                        verbos.Add(verbo);
                        continue;
                    }
                    if ((language != before_language) || ((lesson != before_lesson) && (language == before_language)))
                    {
                        materia = materias.FirstOrDefault(index => index.linguagem == before_language && index.nome == before_lesson);
                        if (materia != null)
                        {
                            conteudo = new Conteudo();
                            conteudo = materia.conteudo;
                            conteudo.verbo = verbos;
                            int index = materias.IndexOf(materia);
                            materia.conteudo = conteudo;
                            materias[index] = materia;
                            verbos = new List<string>();
                        }
                        else
                        {
                            conteudo = new Conteudo();
                            conteudo = InsertModel(verbos);
                            materia = new Materia();
                            materia = InsertMateria(before_language, before_lesson, conteudo);
                            materias.Add(materia);
                            verbos = new List<string>();
                        }
                    }
                    before_language = language;
                    before_lesson = lesson;
                    if ((!verbos.Contains(verbo)) || (verbos.Count == 0)) verbos.Add(verbo);
                }
                materia = materias.FirstOrDefault(index => index.linguagem == before_language && index.nome == before_lesson);
                if (before_lesson.Split(" ").Count() > 1)
                {
                    if (materia != null)
                    {
                        conteudo = new Conteudo();
                        conteudo = materia.conteudo;
                        conteudo.verbo = verbos;
                        int index = materias.IndexOf(materia);
                        materia.conteudo = conteudo;
                        materias[index] = materia;
                        verbos = new List<string>();
                    }
                    else
                    {
                        conteudo = new Conteudo();
                        conteudo = InsertModel(verbos);
                        materia = new Materia();
                        materia = InsertMateria(before_language, before_lesson, conteudo);
                        materias.Add(materia);
                    }
                }
                return materias;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region MOUNT MODEL
        public async Task<List<Circunstancia>> LoadAdverb(List<Adverbios> adverb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load adverb \"Model\" service failed!");

                List<Adverbios> adverbs = new List<Adverbios>();
                adverbs = adverb.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.type).ToList();
                string before_name = "";
                string before_language = "";
                string name = "";
                string language = "";
                string type = "";
                List<string> types = new List<string>();
                Circunstancia circunstancia = new Circunstancia();
                List<Circunstancia> circunstancias = new List<Circunstancia>();
                foreach (Adverbios item in adverbs)
                {
                    language = item.language;
                    name = item.name;
                    type = item.type;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_name = name;
                        types = new List<string>();
                        types.Add(type);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        circunstancia = new Circunstancia();
                        circunstancia = InsertCircunstancia(before_language, before_name, types);
                        circunstancias.Add(circunstancia);
                        types = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    if ((!types.Contains(type)) || (types.Count == 0)) types.Add(type);
                }
                circunstancia = new Circunstancia();
                circunstancia = InsertCircunstancia(before_language, before_name, types);
                circunstancias.Add(circunstancia);
                return circunstancias;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Preceito>> LoadArticle(List<Artigos> article)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load article \"Model\" service failed!");

                List<Artigos> articles = new List<Artigos>();
                articles = article.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.type).ThenBy(index => index.number).ThenBy(index => index.gender).ToList();
                string before_name = "";
                string before_language = "";
                string name = "";
                string language = "";
                string type = "";
                string number = "";
                string gender = "";
                List<string> types = new List<string>();
                List<string> numbers = new List<string>();
                List<string> genders = new List<string>();
                Preceito preceito = new Preceito();
                List<Preceito> preceitos = new List<Preceito>();
                foreach (Artigos item in articles)
                {
                    language = item.language;
                    name = item.name;
                    type = item.type;
                    number = item.number;
                    gender = item.gender;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_name = name;
                        types = new List<string>();
                        types.Add(type);
                        numbers = new List<string>();
                        numbers.Add(number);
                        genders = new List<string>();
                        genders.Add(gender);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        preceito = new Preceito();
                        preceito = InsertPreceito(before_language, before_name, types, numbers, genders);
                        preceitos.Add(preceito);
                        types = new List<string>();
                        numbers = new List<string>();
                        genders = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    if ((!types.Contains(type)) || (types.Count == 0)) types.Add(type);
                    if ((!numbers.Contains(number)) || (numbers.Count == 0)) numbers.Add(number);
                    if (!(genders.Contains(gender)) || (genders.Count == 0)) genders.Add(gender);
                }
                preceito = new Preceito();
                preceito = InsertPreceito(before_language, before_name, types, numbers, genders);
                preceitos.Add(preceito);
                return preceitos;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Estoutro>> LoadPronoun(List<Pronomes> pronoun)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load pronoun \"Model\" service failed!");

                List<Pronomes> pronouns = new List<Pronomes>();
                pronouns = pronoun.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.type).ThenBy(index => index.person).ThenBy(index => index.number).ThenBy(index => index.gender).ThenBy(index => index.context).ToList();
                string before_name = "";
                string before_language = "";
                string name = "";
                string language = "";
                string type = "";
                int person = 0;
                string number = "";
                string gender = "";
                string context = "";
                List<string> types = new List<string>();
                List<int> persons = new List<int>();
                List<string> numbers = new List<string>();
                List<string> genders = new List<string>();
                List<string> contexts = new List<string>();
                Estoutro estoutro = new Estoutro();
                Contento contento = new Contento();
                List<Contento> contentos = new List<Contento>();
                List<Estoutro> estoutros = new List<Estoutro>();
                foreach (Pronomes item in pronouns)
                {
                    language = item.language;
                    name = item.name;
                    type = item.type;
                    person = item.person;
                    number = item.number;
                    gender = item.gender;
                    context = item.context;
                    if ((before_language == ""))
                    {
                        before_language = language;
                        before_name = name;
                        types = new List<string>();
                        types.Add(type);
                        persons = new List<int>();
                        persons.Add(person);
                        numbers = new List<string>();
                        numbers.Add(number);
                        genders = new List<string>();
                        genders.Add(gender);
                        contexts = new List<string>();
                        contexts.Add(context);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language))
                        || ((!types.Contains(type)) && (name == before_name) && (language == before_language)))
                    {
                        contento = new Contento();
                        contento = InsertContento(persons, numbers, genders, contexts);
                        contentos.Add(contento);
                        estoutro = new Estoutro();
                        estoutro = InsertEstoutro(before_language, before_name, types, contentos);
                        estoutros.Add(estoutro);
                        types = new List<string>();
                        persons = new List<int>();
                        numbers = new List<string>();
                        genders = new List<string>();
                        contexts = new List<string>();
                        contentos = new List<Contento>();
                    }
                    if ((!persons.Contains(person)) && (persons.Count > 0))
                    {
                        contento = new Contento();
                        contento = InsertContento(persons, numbers, genders, contexts);
                        contentos.Add(contento);
                        persons = new List<int>();
                        numbers = new List<string>();
                        genders = new List<string>();
                        contexts = new List<string>();
                    }
                    else if ((!numbers.Contains(number)) && (numbers.Count > 0))
                    {
                        contento = new Contento();
                        contento = InsertContento(persons, numbers, genders, contexts);
                        contentos.Add(contento);
                        persons = new List<int>();
                        numbers = new List<string>();
                        genders = new List<string>();
                        contexts = new List<string>();
                    }
                    else if ((!genders.Contains(gender)) && (genders.Count > 0))
                    {
                        contento = new Contento();
                        contento = InsertContento(persons, numbers, genders, contexts);
                        contentos.Add(contento);
                        persons = new List<int>();
                        numbers = new List<string>();
                        genders = new List<string>();
                        contexts = new List<string>();
                    }
                    else if ((!contexts.Contains(context)) && (contexts.Count > 0))
                    {
                        contento = new Contento();
                        contento = InsertContento(persons, numbers, genders, contexts);
                        contentos.Add(contento);
                        persons = new List<int>();
                        numbers = new List<string>();
                        genders = new List<string>();
                        contexts = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    if ((!types.Contains(type)) || (types.Count == 0)) types.Add(type);
                    if ((!persons.Contains(person)) || (persons.Count == 0)) persons.Add(person);
                    if ((!numbers.Contains(number)) || (numbers.Count == 0)) numbers.Add(number);
                    if ((!genders.Contains(gender)) || (genders.Count == 0)) genders.Add(gender);
                    if ((!contexts.Contains(context)) || (contexts.Count == 0)) contexts.Add(context);
                }
                contento = new Contento();
                contento = InsertContento(persons, numbers, genders, contexts);
                contentos.Add(contento);
                estoutro = new Estoutro();
                estoutro = InsertEstoutro(before_language, before_name, types, contentos);
                estoutros.Add(estoutro);
                return estoutros;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Algarismo>> LoadNumeral(List<Numerais> numeral)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load numeral \"Model\" service failed!");

                List<Numerais> numerais = new List<Numerais>();
                numerais = numeral.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.initial).ThenBy(index => index.type).ToList();
                string before_name = "";
                string before_language = "";
                int before_initial = 0;
                string name = "";
                string language = "";
                int initial = 0;
                string type = "";
                List<string> types = new List<string>();
                Algarismo algarismo = new Algarismo();
                List<Algarismo> algarismos = new List<Algarismo>();
                foreach (Numerais item in numerais)
                {
                    language = item.language;
                    name = item.name;
                    initial = item.initial;
                    type = item.type;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_name = name;
                        before_initial = initial;
                        types = new List<string>();
                        types.Add(type);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        algarismo = new Algarismo();
                        algarismo = InsertAlgarismo(before_language, before_name, before_initial, types);
                        algarismos.Add(algarismo);
                        types = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    before_initial = initial;
                    if ((!types.Contains(type)) || (types.Count == 0)) types.Add(type);
                }
                algarismo = new Algarismo();
                algarismo = InsertAlgarismo(before_language, before_name, before_initial, types);
                algarismos.Add(algarismo);
                return algarismos;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Juncao>> LoadPreposition(List<Preposicoes> preposition)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load preposition \"Model\" service failed!");

                List<Preposicoes> prepositions = new List<Preposicoes>();
                prepositions = preposition.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.type).ToList();
                string before_language = "";
                string before_name = "";
                string name = "";
                string language = "";
                string type = "";
                List<string> types = new List<string>();
                Juncao juncao = new Juncao();
                List<Juncao> juncoes = new List<Juncao>();
                foreach (Preposicoes item in prepositions)
                {
                    language = item.language;
                    name = item.name;
                    type = item.type;
                    if ((before_language == ""))
                    {
                        before_language = language;
                        before_name = name;
                        types = new List<string>();
                        types.Add(type);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        juncao = new Juncao();
                        juncao = InsertJuncao(before_language, before_name, types);
                        juncoes.Add(juncao);
                        types = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    if ((!types.Contains(type)) || (types.Count == 0)) types.Add(type);
                }
                juncao = new Juncao();
                juncao = InsertJuncao(before_language, before_name, types);
                juncoes.Add(juncao);
                return juncoes;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Materia>> LoadMateria(List<Substantivo> noun, List<Adjetivo> adjective, List<Model> model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load materia \"Model\" service failed!");

                List<Materia> materias = new List<Materia>();
                materias = await MateriaAdjective(adjective, materias);
                materias = await MateriaNoun(noun, materias);
                materias = await MateriaModel(model, materias);
                return materias;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Sentenca>> LoadSentenca(List<Sentencas> sentence)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load sentenca \"Model\" service failed!");

                List<Sentencas> sentances = new List<Sentencas>();
                sentances = sentence.OrderBy(index => index.language).ThenBy(index => index.impulse).ThenBy(index => index.rest).ToList();
                string before_impulse = "";
                string before_language = "";
                string impulse = "";
                string language = "";
                string rest = "";
                List<string> rests = new List<string>();
                Sentenca sentenca = new Sentenca();
                List<Sentenca> sentencas = new List<Sentenca>();
                foreach (Sentencas item in sentances)
                {
                    language = item.language;
                    impulse = item.impulse;
                    rest = item.rest;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_impulse = impulse;
                        rests = new List<string>();
                        rests.Add(rest);
                        continue;
                    }
                    if ((language != before_language) || ((impulse != before_impulse) && (language == before_language)))
                    {
                        sentenca = new Sentenca();
                        sentenca = InsertSentenca(before_language, before_impulse, rests);
                        sentencas.Add(sentenca);
                        rests = new List<string>();
                    }
                    before_language = language;
                    before_impulse = impulse;
                    if ((!rests.Contains(rest)) || (rests.Count == 0)) rests.Add(rest);
                }
                sentenca = new Sentenca();
                sentenca = InsertSentenca(before_language, before_impulse, rests);
                sentencas.Add(sentenca);
                return sentencas;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Ligacao>> LoadLigacao(List<Conjuncoes> conjunction)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load ligacao \"Model\" service failed!");

                List<Conjuncoes> conjuncoes = new List<Conjuncoes>();
                conjuncoes = conjunction.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.type).ToList();
                string before_name = "";
                string before_language = "";
                string name = "";
                string language = "";
                string type = "";
                List<string> types = new List<string>();
                Ligacao ligacao = new Ligacao();
                List<Ligacao> ligacoes = new List<Ligacao>();
                foreach (Conjuncoes item in conjuncoes)
                {
                    language = item.language;
                    name = item.name;
                    type = item.type;
                    if (before_language == "")
                    {
                        before_language = language;
                        before_name = name;
                        types = new List<string>();
                        types.Add(type);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        ligacao = new Ligacao();
                        ligacao = InsertLigacao(before_language, before_name, types);
                        ligacoes.Add(ligacao);
                        types = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    if ((!types.Contains(type)) || (types.Count == 0)) types.Add(type);
                }
                ligacao = new Ligacao();
                ligacao = InsertLigacao(before_language, before_name, types);
                ligacoes.Add(ligacao);
                return ligacoes;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public async Task<List<Elocucao>> LoadElocucao(List<Verbos> verb)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load locucao \"Model\" service failed!");

                List<Verbos> verbs = new List<Verbos>();
                verbs = verb.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.model).ThenBy(index => index.mode).ThenBy(index => index.pronoun).ToList();
                string before_name = "";
                string before_language = "";
                string before_model = "";
                string name = "";
                string language = "";
                string model = "";
                string mode = "";
                string pronoun = "";
                List<string> modes = new List<string>();
                List<string> pronouns = new List<string>();
                Elocucao elocucao = new Elocucao();
                Teor teor = new Teor();
                List<Teor> teors = new List<Teor>();
                List<Elocucao> elocucoes = new List<Elocucao>();
                foreach (Verbos item in verbs)
                {
                    language = item.language;
                    name = item.name;
                    model = item.model;
                    mode = item.mode;
                    pronoun = item.pronoun;
                    if ((before_language == ""))
                    {
                        before_language = language;
                        before_name = name;
                        before_model = model;
                        modes = new List<string>();
                        modes.Add(mode);
                        pronouns = new List<string>();
                        pronouns.Add(pronoun);
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        teor = new Teor();
                        teor = InsertTeor(modes, pronouns);
                        teors.Add(teor);
                        elocucao = new Elocucao();
                        elocucao = InsertElocucao(before_language, before_name, before_model, teors);
                        elocucoes.Add(elocucao);
                        modes = new List<string>();
                        pronouns = new List<string>();
                        teors = new List<Teor>();
                    }
                    if ((!modes.Contains(mode)) && (modes.Count > 0))
                    {
                        teor = new Teor();
                        teor = InsertTeor(modes, pronouns);
                        teors.Add(teor);
                        modes = new List<string>();
                        pronouns = new List<string>();
                    } 
                    else if ((!pronouns.Contains(pronoun)) && (pronouns.Count > 0))
                    {
                        teor = new Teor();
                        teor = InsertTeor(modes, pronouns);
                        teors.Add(teor);
                        modes = new List<string>();
                        pronouns = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    before_model = model;
                    if ((!modes.Contains(mode)) || (modes.Count == 0)) modes.Add(mode);
                    if ((!pronouns.Contains(pronoun)) || (pronouns.Count == 0)) pronouns.Add(pronoun);
                }
                teor = new Teor();
                teor = InsertTeor(modes, pronouns);
                teors.Add(teor);
                elocucao = new Elocucao();
                elocucao = InsertElocucao(before_language, before_name, before_model, teors);
                elocucoes.Add(elocucao);
                return elocucoes;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        
        public async Task<List<Assistente>> LoadAssistente(List<Auxiliares> auxiliary)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load assistente \"Model\" service failed!");

                List<Auxiliares> auxiliaries = new List<Auxiliares>();
                auxiliaries = auxiliary.OrderBy(index => index.language).ThenBy(index => index.name).ThenBy(index => index.mode).ThenBy(index => index.prefix).ThenBy(index => index.preverb).ThenBy(index => index.premode).ToList();
                string before_name = "";
                string before_language = "";
                string name = "";
                string language = "";
                string mode = "";
                string prefix = "";
                string preverb = "";
                string premode = "";
                List<string> modes = new List<string>();
                List<string> prefixs = new List<string>();
                List<string> preverbs = new List<string>();
                List<string> premodes = new List<string>();
                Assistente assistente = new Assistente();
                Tematica tematica = new Tematica();
                List<Assistente> assistentes = new List<Assistente>();
                List<Tematica> tematicas = new List<Tematica>();
                foreach (Auxiliares item in auxiliaries)
                {
                    language = item.language;
                    name = item.name;
                    mode = item.mode;
                    prefix = item.prefix;
                    preverb = item.preverb;
                    premode = item.premode;
                    if ((before_language == ""))
                    {
                        before_language = language;
                        before_name = name;
                        modes = new List<string>();
                        modes.Add(mode);
                        prefixs = new List<string>();
                        prefixs.Add(prefix);
                        preverbs = new List<string>();
                        preverbs.Add(preverb);
                        premodes = new List<string>();
                        premodes.Add(premode);  
                        continue;
                    }
                    if ((language != before_language) || ((name != before_name) && (language == before_language)))
                    {
                        tematica = new Tematica();
                        tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                        tematicas.Add(tematica);
                        assistente = new Assistente();
                        assistente = InsertAssistente(before_language, before_name, tematicas);
                        assistentes.Add(assistente);
                        modes = new List<string>();
                        prefixs = new List<string>();
                        preverbs = new List<string>();
                        premodes = new List<string>();
                        tematicas = new List<Tematica>();
                    }
                    if ((!modes.Contains(mode)) && (modes.Count > 0))
                    {
                        tematica = new Tematica();
                        tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                        tematicas.Add(tematica);
                        modes = new List<string>();
                        prefixs = new List<string>();
                        preverbs = new List<string>();
                        premodes = new List<string>();
                    }
                    else if ((!prefixs.Contains(prefix)) && (prefixs.Count > 0))
                    {
                        tematica = new Tematica();
                        tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                        tematicas.Add(tematica);
                        modes = new List<string>();
                        prefixs = new List<string>();
                        preverbs = new List<string>();
                        premodes = new List<string>();
                    }
                    else if ((!preverbs.Contains(preverb)) && (preverbs.Count > 0))
                    {
                        tematica = new Tematica();
                        tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                        tematicas.Add(tematica);
                        modes = new List<string>();
                        prefixs = new List<string>();
                        preverbs = new List<string>();
                        premodes = new List<string>();
                    }
                    else if ((!preverbs.Contains(preverb)) && (preverbs.Count > 0))
                    {
                        tematica = new Tematica();
                        tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                        tematicas.Add(tematica);
                        modes = new List<string>();
                        prefixs = new List<string>();
                        preverbs = new List<string>();
                        premodes = new List<string>();
                    }
                    else if ((!premodes.Contains(premode)) && (premodes.Count > 0))
                    {
                        tematica = new Tematica();
                        tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                        tematicas.Add(tematica);
                        modes = new List<string>();
                        prefixs = new List<string>();
                        preverbs = new List<string>();
                        premodes = new List<string>();
                    }
                    before_language = language;
                    before_name = name;
                    if ((!modes.Contains(mode)) || (modes.Count == 0)) modes.Add(mode);
                    if ((!prefixs.Contains(prefix)) || (prefixs.Count == 0)) prefixs.Add(prefix);
                    if ((!preverbs.Contains(preverb)) || (preverbs.Count == 0)) preverbs.Add(preverb);
                    if ((!premodes.Contains(premode)) || (premodes.Count == 0)) premodes.Add(premode);
                }
                tematica = new Tematica();
                tematica = InsertTematica(modes, prefixs, preverbs, premodes);
                tematicas.Add(tematica);
                assistente = new Assistente();
                assistente = InsertAssistente(before_language, before_name, tematicas);
                assistentes.Add(assistente);
                return assistentes;
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