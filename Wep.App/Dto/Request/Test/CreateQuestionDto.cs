using System.Collections.Generic;
using UseCases.Test.Dto;

namespace Wep.App.Dto.Request.Test
{
    public record CreateQuestionDto(int Position, string Content, QuestionTypeDto Type, IEnumerable<CreateAnswerOptionDto> AnswerOptions);
}
