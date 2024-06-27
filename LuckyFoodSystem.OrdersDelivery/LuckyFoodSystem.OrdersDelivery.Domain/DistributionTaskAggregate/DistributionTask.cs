namespace LuckyFoodSystem.Delivery.Domain.DistributionTaskAggregate;

public class DistributionTask
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid CourierId { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public DistributionTask(Guid orderId, Guid courierId)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        CourierId = courierId;
        AssignedAt = DateTime.UtcNow;
    }

    //public void Start()
    //{
    //    if (Status != DistributionStatus.Assigned)
    //        throw new InvalidOperationException("Task can only be started if it is assigned.");

    //    Status = DistributionStatus.InProgress;
    //}

    //public void Complete()
    //{
    //    if (Status != DistributionStatus.InProgress)
    //        throw new InvalidOperationException("Task can only be completed if it is in progress.");

    //    Status = DistributionStatus.Completed;
    //    CompletedAt = DateTime.UtcNow;
    //}
}
