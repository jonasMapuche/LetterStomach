namespace LetterStomach.Models
{
    public class DecisionTree
    {
        public int level { get; set; }
        public int order { get; set; }
        public string node { get; set; }
        public DecisionTree host { get; set; }
        public List<DecisionTree> children { get; set; }
    }
}
