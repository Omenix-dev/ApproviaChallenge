using ApproviaChallenge.TaskManager.Core.DTOs;
using ApproviaChallenge.TaskManager.Core.Models;
using AutoMapper;

namespace ApproviaChallenge.TaskManager.Core.Utilities.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskList, TaskListDto>().ReverseMap();
            CreateMap<TaskList, CreateTaskDto>().ReverseMap();
        }
    }
}
