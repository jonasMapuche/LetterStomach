using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    internal interface ISyntaxService
    {
        List<Lesson> SampleSubjectVerb(List<Sentenca> sentences, List<Lesson> matters);
        List<Tutorial> SampleSubjectVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec);
        List<Lesson> CompoundSubjectVerb(List<Sentenca> sentences, List<Lesson> matters);
        List<Lesson> PredicateDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicatePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicateIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int init_order);
        List<Lesson> PredicateDirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicateDirectObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicateIndirectObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Lesson> PredicatePredicativeIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
 
    }
}
