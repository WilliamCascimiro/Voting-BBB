namespace Voting_Consumer.DTOs
{
    public class VoteRequest
    {
        public Guid userId { get; set; }
        public Guid participantId { get; set; }
        public Guid paredaoId { get; set; }
    }
}
