using Entities.Base;
using Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Test : AuditableEntity<int>
    {
        public string Title { get; private set; }

        private List<Question> _questions;

        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

        private Test() { }

        public Test(string title, ICollection<Question> questions)
        {
            _questions = questions.ToList();
            Title = title;
        }

        public Test CreateQuestionWithAnswerOptions(QuestionWithAnswerOptions question)
        {
            if (_questions.Any(x => x.Content == question.Content))
                throw new QuestionAlreadyExistException();

            _questions.Add(question);

            return this;
        }

        public Test CreateQuestionWithFileAnswer(QuestionWithFileAnswer question)
        {
            if (_questions.Any(x => x.Content == question.Content))
                throw new QuestionAlreadyExistException();

            _questions.Add(question);

            return this;
        }

        public Test CreateQuestionWithTextAnswer(QuestionWithTextAnswer question)
        {
            if (_questions.Any(x => x.Content == question.Content))
                throw new QuestionAlreadyExistException();

            _questions.Add(question);

            return this;
        }
    }
}
