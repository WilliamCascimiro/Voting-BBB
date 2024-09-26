namespace Voting.Application.DTOs.Booking
{
    public class CreateVoteRequest
    {
        public Guid userId { get; set; }
        public Guid participantId { get; set; }
        public Guid paredaoId { get; set; }

    }
}
