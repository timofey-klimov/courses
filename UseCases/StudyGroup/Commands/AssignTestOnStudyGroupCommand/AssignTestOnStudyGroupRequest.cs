using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand
{
    public record AssignTestOnStudyGroupRequest(int GroupId, int TestId, DateTime Deadline) : IRequest;
  
}
