using System.Drawing;
using System.Xml.Linq;
using Voting.API.Domain.Entities.Base;

namespace Voting.API.Domain.Entities
{
    public class Participant : BaseDomain
    {
        public Participant()
        {
        }

        public Participant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; private set; }
    }

}
