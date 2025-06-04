using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kAI_webAPI.Models.Subjects;
using kAI_webAPI.Dtos.Subjects;
namespace kAI_webAPI.Mappers
{
    public static class SubjectsMapper
    {   public static SubjectsDTOs ToSubjectsDto(this Subjects subjectsModel)
        {
            return new SubjectsDTOs
            {
                Subject = subjectsModel.Subject,
                Type = subjectsModel.Type ?? string.Empty
            };
        }
        public static SubjectsGroupDTOs ToSubjectsGroupDto(this Subjects_group subjectsGroupModel)
        {
            var dto = new SubjectsGroupDTOs
            {
                Subjects = new List<SubjectsDTOs>()
            };
            if (subjectsGroupModel.Subjects1 != null)
            {
                dto.Subjects.Add(subjectsGroupModel.Subjects1.ToSubjectsDto());
            }
            if (subjectsGroupModel.Subjects2 != null)
            {
                dto.Subjects.Add(subjectsGroupModel.Subjects2.ToSubjectsDto());
            }
            if (subjectsGroupModel.Subjects3 != null)
            {
                dto.Subjects.Add(subjectsGroupModel.Subjects3.ToSubjectsDto());
            }
            if (subjectsGroupModel.Subjects4 != null)
            {
                dto.Subjects.Add(subjectsGroupModel.Subjects4.ToSubjectsDto());
            }
            if (subjectsGroupModel.Subjects5 != null)
            {
                dto.Subjects.Add(subjectsGroupModel.Subjects5.ToSubjectsDto());
            }
            return dto;
        }
    }
}
