using Voting.API.Domain.Entities.Base;

namespace Voting.API.Domain.Entities
{
    public class Vote : BaseDomain
    {
        public Vote()
        {
        }

        public Vote(Guid userId, Guid participantId, Guid paredaoId)
        {
            UserId = userId;
            ParticipantId = participantId;
            ParedaoId = paredaoId;
            CreateDate = DateTime.Now;
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
        public Guid ParedaoId { get; set; }
        public Paredao Paredao { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
