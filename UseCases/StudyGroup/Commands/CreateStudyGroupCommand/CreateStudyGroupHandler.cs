using Application.Interfaces.StudyGroups;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Common.Participant;
using UseCases.StudyGroup.Dto;
using UseCases.StudyGroup.Exceptions;

namespace UseCases.StudyGroup.Commands.CreateStudyGroupCommand
{
    public class CreateStudyGroupHandler : IRequestHandler<CreateStudyGroupRequest, StudyGroupDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IStudyGroupService _studyGroupService;

        public CreateStudyGroupHandler(IDbContext dbContext, IStudyGroupService studyGroupService)
        {
            _dbContext =  dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _studyGroupService = studyGroupService ?? throw new ArgumentNullException(nameof(studyGroupService));
        }

        public async Task<StudyGroupDto> Handle(CreateStudyGroupRequest request, CancellationToken cancellationToken)
        {
            if (await _dbContext.StudyGroups.AnyAsync(x => x.Title == request.Title))
                throw new GroupAlreadyExistException();

            var teacher = await _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Include(x => x.StudyGroups)
                .Include(x => x.StudentTeachers)
                    .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId);

            if (teacher == null)
                throw new ParticipantNotFoundException();

            var students = _dbContext.Participants
                .OfType<Entities.Participants.Student>()
                .Where(x => request.StudentsIds.Any(y => y == x.Id))
                .AsEnumerable();

            var studyGroup = _studyGroupService.CreateStudyGroup(students, teacher, request.Title);

            await _dbContext.SaveChangesAsync();

            return new StudyGroupDto(
                studyGroup.Id, studyGroup.Title, 
                new TeacherDto(teacher.Id, teacher.Name, teacher.Surname, teacher.Login), studyGroup.CreateDate);
        }
    }
}
