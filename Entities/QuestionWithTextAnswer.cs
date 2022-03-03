namespace Entities
{
    public class QuestionWithTextAnswer : Question
    {
        protected QuestionWithTextAnswer() { }
        public QuestionWithTextAnswer(string title, string content)
            : base(title, content)
        {

        }
    }
}
