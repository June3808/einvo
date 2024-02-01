using AutoMapper.Internal.Mappers;
//using EasyAbp.LoggingManagement.Permissions;
//using EasyAbp.LoggingManagement.AuditLogs;
//using EasyAbp.LoggingManagement.AuditLogs.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Volo.Abp.Linq;
using System.Linq.Dynamic.Core;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Json;
using System.Net;
using Sys.AuditLogs;
using Sys;
using Sys.Permissions;
using System.Threading;
using Volo.Abp.Identity;

namespace Sys.AuditLogs
{
    [Authorize(SysPermissions.AuditLog.Default)]
    public class AuditLogAppService : ApplicationService, IAuditLogAppService
    {
        //private IRepository<AuditLog> repository;

        //public AuditLogAppService(IRepository<AuditLog> repository)
        //{
        //    this.repository = repository;
        //}

        protected IAuditLogRepository AuditLogRepository { get; }

        protected IJsonSerializer JsonSerializer { get; }

        protected IPermissionChecker PermissionChecker { get; }

        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }

        private readonly IIdentityUserAppService userAppService;

        public AuditLogAppService(
            IAuditLogRepository auditLogRepository
            , IJsonSerializer jsonSerializer, IPermissionChecker permissionChecker, IPermissionDefinitionManager permissionDefinitionManager
            , IIdentityUserAppService _userAppService
            )
        {
            this.AuditLogRepository = auditLogRepository;
            this.JsonSerializer = jsonSerializer;
            this.PermissionChecker = permissionChecker;
            this.PermissionDefinitionManager = permissionDefinitionManager;
            this.userAppService = _userAppService;
        }

        public virtual async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogListDto input)
        {
            try
            {
                IAuditLogRepository auditLogRepository = this.AuditLogRepository;
                string sorting = input.Sorting;
                int maxResultCount = input.MaxResultCount;
                int skipCount = input.SkipCount;
                DateTime? dateTime = input.StartTime;
                DateTime? dateTime2 = input.EndTime;
                string httpMethod = input.HttpMethod;
                string test = input.ApplicationName;    
                HttpStatusCode? httpStatusCode = input.HttpStatusCode;
                List<AuditLog> list = await auditLogRepository.GetListAsync(null, maxResultCount, skipCount, dateTime, dateTime2, httpMethod, input.Url, null, input.UserName, input.ApplicationName, null, input.CorrelationId, input.MaxExecutionDuration, input.MinExecutionDuration, input.HasException, httpStatusCode, false);
                //var query = auditLogRepository.Where(x=> x.);
                //List<AuditLog> list = await auditLogRepository.GetListAsync();
                List<AuditLog> auditLogs = list;
                IAuditLogRepository auditLogRepository2 = this.AuditLogRepository;
                string httpMethod2 = input.HttpMethod;
                httpStatusCode = input.HttpStatusCode;

                long num = await auditLogRepository2.GetCountAsync(dateTime, dateTime2, httpMethod2, httpMethod, null, input.UserName, input.ApplicationName, null, input.CorrelationId, input.MaxExecutionDuration, input.MinExecutionDuration, false, httpStatusCode);
                List<AuditLogDto> list2 = base.ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(auditLogs);
                return new PagedResultDto<AuditLogDto>(num, list2);
            }
            catch(Exception e) {
                throw e;
            }
        }

        public virtual async Task<AuditLogDto> GetAsync(Guid id)
        {
            AuditLog auditLog = await this.AuditLogRepository.GetAsync(id);
            var auditLogDto = base.ObjectMapper.Map<AuditLog, AuditLogDto>(auditLog);
            foreach (var auditLogActionDto in auditLogDto.Actions)
            {
                object obj = this.JsonSerializer.Deserialize<object>(auditLogActionDto.Parameters, true);
                auditLogActionDto.Parameters = this.JsonSerializer.Serialize(obj, true, true);
            }
            return auditLogDto;
        }

        public virtual async Task<GetErrorRateOutput> GetErrorRateAsync(GetErrorRateFilter filter)
        {
            long successfulLogCount = await this.AuditLogRepository.GetCountAsync(new DateTime?(filter.StartDate), new DateTime?(filter.EndDate.AddDays(1.0)),null,null, null, null, null, null, null, null, null, new bool?(false));
            long value = await this.AuditLogRepository.GetCountAsync(new DateTime?(filter.StartDate), new DateTime?(filter.EndDate.AddDays(1.0)), null, null, null,null,null,  null, null, null, null, new bool?(true));
            return new GetErrorRateOutput
            {
                Data = new Dictionary<string, long>
                {
                    {
                        base.L["Fault"],
                        value
                    },
                    {
                        base.L["Success"],
                        successfulLogCount
                    }
                }
            };
        }

        public virtual async Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDayAsync(GetAverageExecutionDurationPerDayInput filter)
        {
            var source = await this.AuditLogRepository.GetAverageExecutionDurationPerDayAsync(filter.StartDate, filter.EndDate);
            var getAverageExecutionDurationPerDayOutput = new GetAverageExecutionDurationPerDayOutput();
            getAverageExecutionDurationPerDayOutput.Data = source.ToDictionary((KeyValuePair<DateTime, double> x) => x.Key.ToString("d"), (KeyValuePair<DateTime, double> x) => x.Value);
            return getAverageExecutionDurationPerDayOutput;
        }

        public virtual async Task<PagedResultDto<EntityChangeDto>> GetEntityChangesAsync(GetEntityChangesDto input)
        {
            List<EntityChange> list = await this.AuditLogRepository.GetEntityChangeListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.AuditLogId, input.StartDate, input.EndDate, input.EntityChangeType, input.EntityId, input.EntityTypeFullName, true);
            List<EntityChange> entityChanges = list;
            long num = await this.AuditLogRepository.GetEntityChangeCountAsync(input.AuditLogId, input.StartDate, input.EndDate, input.EntityChangeType, input.EntityId, input.EntityTypeFullName);
            List<EntityChangeDto> list2 = base.ObjectMapper.Map<List<EntityChange>, List<EntityChangeDto>>(entityChanges);
            return new PagedResultDto<EntityChangeDto>(num, list2);
        }

        [AllowAnonymous]
        public virtual async Task<List<EntityChangeWithUsernameDto>> GetEntityChangesWithUsernameAsync(EntityChangeFilter input)
        {
            await this.CheckPermissionForEntity(input.EntityTypeFullName);
            var list = await this.AuditLogRepository.GetEntityChangesWithUsernameAsync(input.EntityId, input.EntityTypeFullName);
            return base.ObjectMapper.Map<List<EntityChangeWithUsername>, List<EntityChangeWithUsernameDto>>(list);
        }

        public virtual async Task<EntityChangeWithUsernameDto> GetEntityChangeWithUsernameAsync(Guid entityChangeId)
        {
            var entityChangeWithUsername = await this.AuditLogRepository.GetEntityChangeWithUsernameAsync(entityChangeId);
            return base.ObjectMapper.Map<EntityChangeWithUsername, EntityChangeWithUsernameDto>(entityChangeWithUsername);
        }

        public virtual async Task<EntityChangeDto> GetEntityChangeAsync(Guid entityChangeId)
        {
            var entityChange = await this.AuditLogRepository.GetEntityChange(entityChangeId);
            return base.ObjectMapper.Map<EntityChange, EntityChangeDto>(entityChange);
        }

        protected virtual async Task CheckPermissionForEntity(string entityFullName)
        {
            string text = "AuditLogging.ViewChangeHistory:" + entityFullName;
            if (await this.PermissionDefinitionManager.GetOrNullAsync(text) == null)
            {
                await base.AuthorizationService.CheckAsync("AuditLogging.AuditLogs");
            }
            else
            {
                var flag = await this.PermissionChecker.IsGrantedAsync(text);
                if (!flag)
                {
                    await base.AuthorizationService.CheckAsync("AuditLogging.AuditLogs");
                }
            }
        }
        //public async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogListInput input)
        //{
        //    //var list = await repository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, "");

        //    var query = await CreateAuditLogAndUsersQuery(input);
        //    var resultCount = query.LongCount();

        //    query = query.PageBy(input);
        //    var result = query.Select(s => new AuditLogDto()
        //    {
        //        LogName = s.ApplicationName,
        //        LogValue = s.Exceptions.IsNullOrEmpty() ? "" : s.Exceptions,
        //        Time = s.ExecutionTime,
        //        Level = "unknown"//dict.ContainsKey("Level") ? dict["Level"] : "unknown"
        //    }).ToList();

        //    return new PagedResultDto<AuditLogDto>() { 
        //        Items = result,
        //        TotalCount = resultCount

        //    };
        //}

        //private async Task<IQueryable<AuditLog>> CreateAuditLogAndUsersQuery(GetAuditLogListInput input)
        //{
        //    //var query = from auditLog in repository
        //    //            join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
        //    //            from joinedUser in userJoin.DefaultIfEmpty()
        //    //            where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime <= input.EndDate
        //    //            select new AuditLogAndUser { AuditLog = auditLog, User = joinedUser };
        //    var query = await repository.GetQueryableAsync();

        //    query = query
        //        .Where(a => a.ExecutionTime >= input.StartTime && a.ExecutionTime <= input.EndTime)
        //        .WhereIf(!input.QueryString.IsNullOrWhiteSpace(), item => item.UserName.Contains(input.QueryString))
        //        .WhereIf(!input.QueryString.IsNullOrWhiteSpace(), item => item.Exceptions.Contains(input.QueryString));
        //    //.WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
        //    //.WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), item => item.AuditLog.ServiceName.Contains(input.ServiceName))
        //    //.WhereIf(!input.MethodName.IsNullOrWhiteSpace(), item => item.AuditLog.MethodName.Contains(input.MethodName))
        //    //.WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), item => item.AuditLog.BrowserInfo.Contains(input.BrowserInfo))
        //    //.WhereIf(input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, item => item.AuditLog.ExecutionDuration >= input.MinExecutionDuration.Value)
        //    //.WhereIf(input.MaxExecutionDuration.HasValue && input.MaxExecutionDuration < int.MaxValue, item => item.AuditLog.ExecutionDuration <= input.MaxExecutionDuration.Value)
        //    //.WhereIf(input.HasException == true, item => item.AuditLog.Exception != null && item.AuditLog.Exception != "")
        //    //.WhereIf(input.HasException == false, item => item.AuditLog.Exception == null || item.AuditLog.Exception == "");
        //    return query;
        //}

        public virtual async Task<ListResultDto<IdentityUserDto>> GetActiveUsersAsync(GetErrorRateFilter filter)
        {
            var query = await this.AuditLogRepository.GetQueryableAsync();
            var userloglist = query
                .Where(a => a.ExecutionTime < filter.EndDate.AddDays(1) && a.ExecutionTime > filter.StartDate)
                .GroupBy(t => new { t.UserId })
                .Select(g => new { Key = g.Key, Count = g.Count() })
                .ToList();

            var data = new List<IdentityUserDto>();
            foreach (var userlog in userloglist) {
                if (userlog.Key.UserId == null) continue;
                var user = await userAppService.GetAsync(userlog.Key.UserId.GetValueOrDefault());
                if (user == null) continue;
                data.Add(user);
            }
            var result = new ListResultDto<IdentityUserDto>(data);
            return result;
        }
    }
}
