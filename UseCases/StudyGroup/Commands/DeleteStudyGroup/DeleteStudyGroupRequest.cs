using MediatR;

namespace UseCases.StudyGroup.Commands.DeleteStudyGroup
{
    public record DeleteStudyGroupRequest(int groupId) :IRequest<int>;
}
