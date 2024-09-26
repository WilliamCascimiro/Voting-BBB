using Voting.API.Domain.Entities.Base;

namespace Voting.API.Domain.Entities
{
    public class Result : BaseDomain
    {
        public Result()
        {
        }

        public Result(Guid participantId, Guid paredaoId, int count)
        {
            ParticipantId = participantId;
            ParedaoId = paredaoId;
            Count = count;
        }

        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
        public Guid ParedaoId { get; set; }
        public Paredao Paredao { get; set; }
        public int Count { get; set; }
    }
}
