using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    internal interface ISyntaxViewModel
    {
        string GetOration(List<Word> words);

        List<Lesson> SampleSubjectVerb(List<Sentenca> sentences, List<Lesson> matters);
        List<Lesson> CompoundSubjectVerb(List<Sentenca> sentences, List<Lesson> matters);
        List<Lesson> PredicateDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicatePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicateIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int init_order);
        List<Lesson> PredicateDirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicateDirectObjectPredicativo(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicateIndirectObjectPredicativo(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicatePredicativoIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
    }
}
