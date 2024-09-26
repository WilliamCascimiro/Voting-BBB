using Voting.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Voting.API.Voting.Infra.DataBase.Context;
using Voting.API.Voting.Infra.DataBase.Repositories.Base;
using Voting.API.Domain.Entities;

namespace Voting.API.Voting.Infra.DataBase.Repositories
{
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        readonly BBBDbContext _context;

        public VoteRepository(BBBDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
