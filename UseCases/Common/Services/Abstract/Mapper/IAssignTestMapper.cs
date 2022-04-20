using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Common.Dto;

namespace UseCases.Common.Services.Abstract.Mapper
{
    public interface IAssignTestMapper
    {
        AssignedTestDto ToAssignTestDto(AssignedTest assignedTest);
    }
}
