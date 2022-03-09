using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class QuestionWithAnswerOptions : Question
    {
        protected QuestionWithAnswerOptions() { }
        public QuestionWithAnswerOptions(string content, int position, IEnumerable<AnswerOption> answers) 
            : base(content, position)
        {
            _ansewers = answers.ToList();
        }

        private List<AnswerOption> _ansewers;

        public IReadOnlyCollection<AnswerOption> Answers => _ansewers.AsReadOnly();
    }
}
