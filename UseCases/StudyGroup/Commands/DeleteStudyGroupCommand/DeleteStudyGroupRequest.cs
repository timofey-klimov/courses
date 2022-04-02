using MediatR;

namespace UseCases.StudyGroup.Commands.DeleteStudyGroupCommand
{
    public record DeleteStudyGroupRequest(int groupId) : IRequest<int>;
}
