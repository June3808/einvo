using System;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Threading;
using TaskScheduler.TaskSchedulers.Dtos;
using Quartz;

namespace TaskScheduler.TaskSchedulers
{
    public interface ITaskSchedulersAppService :
    ICrudAppService<
        ScheduleJobDto,
        Guid,
        ScheduleJobGetListInput,
        CreateUpdateScheduleJobDto,
        CreateUpdateScheduleJobDto>
    {

        Task<PagedResultDto<ScheduleJobDto>> GetAll(ScheduleJobGetListInput input, CancellationToken token);

        Task<CreateUpdateScheduleJobDto> GetScheduleJobForEdit(Guid? Id);

        Task CreateOrEditAdvanced(CreateUpdateScheduleJobDto job, bool mustStartNow);

        Task StopScheduleJob(Guid id);

        Task ScheduleAllJobs();

        Task StartScheduleJob(Guid id);

        Task UpdateJobStatus(JobKey key, JobRunStatus runStatus, string log);

        //Task BuildAndRunJob(IScheduler schedulerContext, Type type);

        Task<IJobDetail> BuildJobDetail(Type type);

        Task<ITrigger> BuildTrigger(Type type);
    }
}
