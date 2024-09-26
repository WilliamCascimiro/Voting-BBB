namespace Voting.FraudPreventionWorker.DTOs.VoteRequest
{
    public class VoteRequest
    {
        public Guid userId { get; set; }
        public Guid participantId { get; set; }
        public Guid paredaoId { get; set; }
    }
}
