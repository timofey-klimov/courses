using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Test.CreateTest;
using Wep.App.Dto.Request.Test;

namespace Wep.App.Profiles
{
    public class TestProfiles : Profile
    {
        public TestProfiles()
        {
            CreateMap<CreateAnswerOptionDto, UseCases.Test.Dto.Request.CreateAnswerOptionDto>();

            CreateMap<CreateQuestionDto, UseCases.Test.Dto.Request.CreateQuestionDto>();

            CreateMap<CreateTestDto, CreateTestRequest>()
                .ForMember(x => x.Title, u => u.MapFrom(x => x.Title))
                .ForMember(x => x.Questions, u => u.MapFrom(x => x.Questions));
        }
    }
}
