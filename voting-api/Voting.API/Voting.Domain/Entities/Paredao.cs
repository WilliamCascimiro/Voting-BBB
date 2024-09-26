using Voting.API.Domain.Entities.Base;

namespace Voting.API.Domain.Entities
{
    public class Paredao : BaseDomain
    {
        public Paredao() { }
        public Paredao(DateTime startTime, DateTime endTime, bool isOpen)
        {
            Id = Guid.NewGuid();
            StartTime = startTime;
            EndTime = endTime;
            IsOpen = isOpen;
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOpen { get; set; }
    }

}
