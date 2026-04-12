using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    internal interface ISyntaxService
    {
        List<Lesson> SampleSubjectVerb(List<Sentenca> sentences, List<Lesson> matters);
        List<Tutorial> SampleSubjectVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec);
        List<Lesson> CompoundSubjectVerb(List<Sentenca> sentences, List<Lesson> matters);
        List<Tutorial> CompoundSubjectVerb(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec);
        List<Lesson> PredicateDirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Tutorial> PredicateDirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_init);
        List<Lesson> PredicatePredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Tutorial> PredicatePredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_init);
        List<Lesson> PredicateIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int init_order);
        List<Tutorial> PredicateIndirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_init);
        List<Lesson> PredicateDirectObjectIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Tutorial> PredicateDirectObjectIndirectObject(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_sample, int order_predicative);
        List<Lesson> PredicateDirectObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Tutorial> PredicateDirectObjectPredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_sample, int order_predicative);
        List<Lesson> PredicateIndirectObjectPredicative(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
        List<Tutorial> PredicateIndirectObjectPredicative(List<Tutorial> tutorials, Dictionary<(byte[], byte[]), int> word_2_vec, List<Tutorial> sources, int order_sample, int order_predicative);
        List<Lesson> PredicatePredicativeIndirectObject(List<Sentenca> sentences, List<Lesson> matters, List<Lesson> sources, int order_init);
     }
}
