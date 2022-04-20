using MediatR;
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetTeacherStudentInfoQuery
{
    public record GetTeacherStudentInfoRequest(int StudentId) 
        : IRequest<StudentInfoDto>;
   
}
