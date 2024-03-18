using TaskScheduler.Localization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler.TaskSchedulers;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using TaskScheduler.TaskSchedulers.Dtos;
using System.Threading;
using System;

namespace TaskScheduler;

[Area(TaskSchedulerRemoteServiceConsts.ModuleName)]
[RemoteService(Name = TaskSchedulerRemoteServiceConsts.RemoteServiceName)]
[Route("/api/TaskScheduler/[action]")]
public class TaskSchedulerController : AbpControllerBase
{

    private readonly ITaskSchedulersAppService _service;

    public TaskSchedulerController(ITaskSchedulersAppService service)
    {
        _service = service;
        LocalizationResource = typeof(TaskSchedulerResource);
    }

    [HttpGet]
    public async Task<PagedResultDto<ScheduleJobDto>> GetAll(ScheduleJobGetListInput input, CancellationToken token)
    {
        return await _service.GetAll(input, token);
    }

    [HttpPost]
    public async Task Delete(Guid id)
    {
        await _service.DeleteAsync(id);
    }

    [HttpPost]
    public async Task Stop(Guid id)
    {
        await _service.StopScheduleJob(id);
    }

    [HttpPost]
    public async Task Start(Guid id)
    {
        await _service.StartScheduleJob(id);
    }
}


