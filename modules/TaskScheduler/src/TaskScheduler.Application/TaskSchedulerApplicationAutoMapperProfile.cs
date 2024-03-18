using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using TaskScheduler.TaskSchedulers.Dtos;

namespace TaskScheduler;

public class TaskSchedulerApplicationAutoMapperProfile : Profile
{
    public TaskSchedulerApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<ScheduleJobs, CreateUpdateScheduleJobDto>()
            .ForMember(dto => dto.JobGroupNames , opt => opt.Ignore())
            .ForMember(dto => dto.PriorityList, opt => opt.Ignore())
            .ForMember(dto => dto.Trigger, opt => opt.Ignore());

        CreateMap<CreateUpdateScheduleJobDto, ScheduleJobs>().ReverseMap();


        CreateMap<ScheduleJobs, ScheduleJobDto>().ReverseMap();
    }
}
