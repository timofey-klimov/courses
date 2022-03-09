namespace Entities
{
    public class QuestionWithFileAnswer : Question
    {
        protected QuestionWithFileAnswer() { }

        public QuestionWithFileAnswer(string content, int position)
            : base(content, position)
        {

        }
    }
}
