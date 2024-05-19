using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BackgroundTasksService.AppServices.StateMachines;

namespace BackgroundTasksService.DataAccess.Configuration
{
    internal class TaskSagaStateConfiguration : SagaClassMap<TaskSaga>
    {
        protected override void Configure(EntityTypeBuilder<TaskSaga> entity, ModelBuilder model)
        {
            entity.Property(ts => ts.TaskType)
                .HasMaxLength(60)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(ts => ts.CreationDateTime)
                .IsRequired()
                .HasColumnType("DATETIME2(3)");

            entity.Property(ts => ts.CompletedDateTime)
                .IsRequired(false)
                .HasColumnType("DATETIME2(3)");

            entity.Property(ts => ts.Payload)
                .IsRequired(false)
                .IsUnicode(false);

            entity.Property(ts => ts.ResultPayload)
                .IsRequired(false)
                .IsUnicode(false);

            entity.Property(ts => ts.Url)
                .IsRequired(true)
                .IsUnicode(false);

            entity.Property(ts => ts.HttpMethod)
                .IsRequired(false)
                .IsUnicode(false);
        }
    }
}
