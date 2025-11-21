namespace LetterStomach.Models
{
    public class DecisionTree
    {
        public int level { get; set; }
        public int order { get; set; }
        public string node { get; set; }
        public DecisionTree host { get; set; }
        public DecisionTree tree_true { get; set; }
        public DecisionTree tree_false { get; set; }
    }
}
