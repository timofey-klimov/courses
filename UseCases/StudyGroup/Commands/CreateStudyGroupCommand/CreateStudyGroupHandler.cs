using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;
using UseCases.StudyGroup.Dto;
using UseCases.StudyGroup.Exceptions;

namespace UseCases.StudyGroup.Commands.CreateStudyGroupCommand
{
    public class CreateStudyGroupHandler : IRequestHandler<CreateStudyGroupRequest, CreateStudyGroupDto>
    {
        private readonly IDbContext _dbContext;
        public CreateStudyGroupHandler(IDbContext dbContext)
        {
            _dbContext =  dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<CreateStudyGroupDto> Handle(CreateStudyGroupRequest request, CancellationToken cancellationToken)
        {
            if (await _dbContext.StudyGroups.AnyAsync(x => x.Title == request.Title))
                throw new GroupAlreadyExistException();

            var teacher = await _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Include(x => x.StudyGroups)
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId);

            if (teacher == null)
                throw new ParticipantNotFoundException();

            var students = _dbContext.Participants
                .OfType<Entities.Participants.Student>()
                .Where(x => request.StudentsIds.Any(y => y == x.Id))
                .AsEnumerable();

            var studyGroup = new Entities.StudyGroup(request.Title, students);

            teacher.AssignGroup(studyGroup);

            await _dbContext.SaveChangesAsync();

            return new CreateStudyGroupDto(
                new StudyGroupDto(studyGroup.Id, studyGroup.Title), new Common.Dto.TeacherDto(teacher.Id, teacher.Name, teacher.Surname, teacher.Login));
        }
    }
}
