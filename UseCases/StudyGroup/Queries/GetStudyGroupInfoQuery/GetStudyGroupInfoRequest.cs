using MediatR;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetStudyGroupInfoQuery
{
    public record GetStudyGroupInfoRequest(int GroupId, int TeacherId) : IRequest<StudyGroupInfoDto>;
    
}
