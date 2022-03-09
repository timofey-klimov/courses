using Entities.Base;
using Entities.Exceptions;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Test : TrackableEntity<int>
    {
        public string Title { get; private set; }

        public Manager CreatedBy { get; private set; }

        private List<Question> _questions;

        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

        private Test() { }

        public Test(Manager createdBy, string title, ICollection<Question> questions)
        {
            _questions = questions.ToList();
            CreatedBy = createdBy;
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
