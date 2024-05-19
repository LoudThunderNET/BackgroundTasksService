using AutoMapper;
using BackgroundTasksService.AppServices.StateMachines;
using BackgroundTasksService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTaskService.Infrastructure.Mapping
{
    public class TaskSagaProfile : Profile
    {
        public TaskSagaProfile() 
        {
            CreateMap<EnqueueTaskCommand, TaskSaga>()
                .ForMember(d => d.CurrentState, o => o.Ignore())
                .ForMember(d => d.CreationDateTime, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.CompletedDateTime, o => o.Ignore())
                .ForMember(d => d.ResultPayload, o => o.Ignore());
        }
    }
}
