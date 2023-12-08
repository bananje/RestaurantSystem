using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LuckyFoodSystem.Infrastructure.Interceptors
{
    public class PublishDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IPublisher _mediator;
        public PublishDomainEventsInterceptor(IPublisher mediator) => _mediator = mediator;
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                              InterceptionResult<int> result,
                                                                              CancellationToken cancellationToken = default)
        {
            await PublishDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishDomainEvents(DbContext? dbContext)
        {
            //if (dbContext is null) return;

            //var entities = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            //                        .Where(entry => entry.Entity.DomainEvents.Any())
            //                        .Select(entry => entry.Entity)
            //                        .ToList();

            //var domainEvents = entities.SelectMany(entry => entry.DomainEvents).ToList();

            //entities.ForEach(entity => entity.ClearDomainEvents());

            //foreach (var domainEvent in domainEvents)
            //{
            //    await _mediator.Publish(domainEvent);
            //}

        }
    }
}
