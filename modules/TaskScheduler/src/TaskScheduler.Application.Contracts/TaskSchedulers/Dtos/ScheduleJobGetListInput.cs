using System;
using Volo.Abp.Application.Dtos;

namespace TaskScheduler.TaskSchedulers.Dtos
{
    [Serializable]
    public class ScheduleJobGetListInput : PagedAndSortedResultRequestDto
    {
        public virtual string? Filter { get; set; }
    }
}
