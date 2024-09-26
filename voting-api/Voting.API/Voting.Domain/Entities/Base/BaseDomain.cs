namespace Voting.API.Domain.Entities.Base
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; } = new Guid();
    }
}
