namespace Core.Entities.Concrete
{
    public class SystemUserOperationClaim:IEntity
    {
        public int Id { get; set; }
        public int SystemUserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
