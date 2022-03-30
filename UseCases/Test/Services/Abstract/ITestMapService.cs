using UseCases.Test.Dto;

namespace UseCases.Test.Services.Abstract
{
    public interface ITestMapService
    {
        TestWithQuestionsDto MapFromTestToTestWithQuestionDto(Entities.Test test);
    }
}
