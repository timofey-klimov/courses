using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Test.Dto;

namespace UseCases.Test.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Entities.Question, QuestionDto>()
                .ForMember(x=>x.Type, u => u.MapFrom(s => 
                {
                    var type = s.GetType();
                    if (type == typeof(Entities.QuestionWithAnswerOptions))
                        return QuestionTypeDto.WithAnswerOptions;
                    if (type == typeof(Entities.QuestionWithFileAnswer))
                        return QuestionTypeDto.WithFileInput;
                    if (type == typeof(Entities.QuestionWithTextAnswer))
                        return QuestionTypeDto.WithTextInput;                   
                })
        }
    }
}
