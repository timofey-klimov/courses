using MediatR;
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetStudyGroupInfoQuery
{
    public record GetStudyGroupInfoRequest(int Id) 
        : IRequest<StudyGroupFullInfoDto>;
    
}
