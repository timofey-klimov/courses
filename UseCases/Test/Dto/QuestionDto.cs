using System.Collections.Generic;

namespace UseCases.Test.Dto
{
    public class QuestionDto
    {
        public long Id { get; set; }
        public int Position { get; set; }

        public string Content { get; set; }

        public QuestionTypeDto Type { get; set; }

        public ICollection<AsnwerOptionDto> AnswerOptions { get; set; }
    }
}
