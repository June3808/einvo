using EasyAbp.LoggingManagement;
using System.Threading.Tasks;
using EasyAbp.LoggingManagement.SystemLogs.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using TaskScheduler.TaskSchedulers;
using TaskScheduler.TaskSchedulers.Dtos;
using System.Threading;
using TaskScheduler;
using System;

namespace Sys
{
    //[RemoteService(Name = LoggingManagementRemoteServiceConsts.RemoteServiceName)]
    //[Route("/api/task-scheduler/[action]")]
    //public class TaskSchedulersController : TaskSchedulerController
    //{
    //    private readonly ITaskSchedulersAppService _service;

    //    public TaskSchedulersController(ITaskSchedulersAppService service)
    //    {
    //        _service = service;
    //    }

    //    [HttpGet]
    //    public async Task<PagedResultDto<ScheduleJobDto>> GetAll(ScheduleJobGetListInput input,CancellationToken token)
    //    {
    //        return await _service.GetAll(input, token);
    //    }

    //    [HttpPost]
    //    public async Task Delete(Guid id)
    //    {
    //        await _service.DeleteAsync(id);
    //    }

    //    [HttpPost]
    //    public async Task Stop(Guid id)
    //    {
    //        await _service.StopScheduleJob(id);
    //    }

    //    [HttpPost]
    //    public async Task Start(Guid id)
    //    {
    //        await _service.StartScheduleJob(id);
    //    }
    //}
}
