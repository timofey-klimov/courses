using System.Collections.Generic;

namespace UseCases.Test.Dto.Request
{
    public class CreateQuestionDto
    {
        public int Position { get; set; }

        public string Content { get; set; }

        public QuestionTypeDto Type { get; set; }

        public IEnumerable<CreateAnswerOptionDto> AnswerOptions { get; set; }
    }
}
