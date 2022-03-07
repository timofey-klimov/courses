using Entities.Base;
using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Test : TrackableEntity<Guid>
    {
        public string Title { get; private set; }

        public User CreatedBy { get; private set; }

        private List<Question> _questions;

        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

        private Test() { }

        public Test(User createdBy, string title, ICollection<Question> questions)
        {
            _questions = questions.ToList();
            CreatedBy = createdBy;
            Title = title;
        }

        public Test CreateQuestionWithAnswerOptions(QuestionWithAnswerOptions question)
        {
            if (_questions.Any(x => x.Title == question.Title || x.Content == question.Content))
                throw new QuestionAlreadyExistException();

            _questions.Add(question);

            return this;
        }

        public Test CreateQuestionWithFileAnswer(QuestionWithFileAnswer question)
        {
            if (_questions.Any(x => x.Title == question.Title || x.Content == question.Content))
                throw new QuestionAlreadyExistException();

            _questions.Add(question);

            return this;
        }

        public Test CreateQuestionWithTextAnswer(QuestionWithTextAnswer question)
        {
            if (_questions.Any(x => x.Title == question.Title || x.Content == question.Content))
                throw new QuestionAlreadyExistException();

            _questions.Add(question);

            return this;
        }
    }
}
