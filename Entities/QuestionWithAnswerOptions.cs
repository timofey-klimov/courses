using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class QuestionWithAnswerOptions : Question
    {
        protected QuestionWithAnswerOptions() { }
        public QuestionWithAnswerOptions(string title, string content, ICollection<AnswerOption> answers) 
            : base(title, content)
        {
            _ansewers = answers.ToList();
        }

        private List<AnswerOption> _ansewers;

        public IReadOnlyCollection<AnswerOption> Answers => _ansewers.AsReadOnly();
    }
}
