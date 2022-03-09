namespace Entities
{
    public class QuestionWithTextAnswer : Question
    {
        protected QuestionWithTextAnswer() { }
        public QuestionWithTextAnswer(string content, int position)
            : base(content, position)
        {

        }
    }
}
