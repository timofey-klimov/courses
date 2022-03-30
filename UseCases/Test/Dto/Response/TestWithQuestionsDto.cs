using System;
using System.Collections.Generic;

namespace UseCases.Test.Dto
{
    public class TestWithQuestionsDto : TestDto
    {
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
