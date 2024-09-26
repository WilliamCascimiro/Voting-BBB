using System.Drawing;
using System.Xml.Linq;
using Voting.API.Domain.Entities.Base;

namespace Voting.API.Domain.Entities
{
    public class User : BaseDomain
    {
        public User()
        {
        }

        public User(string name, string email, string senha)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = senha;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}
